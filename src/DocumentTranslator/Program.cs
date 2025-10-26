using DocumentTranslator.Handlers;
using DocumentTranslator.Translators;
using XiHan.Framework.Utils.Logging;
using XiHan.Framework.Utils.ConsoleTools;

// 选择源语言
var sourceLanguageResult = SelectLanguage("选择源语言（要翻译的内容是什么语言？）");
if (sourceLanguageResult == null) return;
var (sourceCode, sourceName) = sourceLanguageResult.Value;

// 选择目标语言
var targetLanguageResult = SelectLanguage("选择目标语言（翻译成什么语言？）");
if (targetLanguageResult == null) return;
var (targetCode, targetName) = targetLanguageResult.Value;

if (sourceCode == targetCode)
{
    LogHelper.Error("源语言和目标语言不能相同！");
    return;
}

// 获取文件路径输入
string? sourceFilePath = GetSourceFilePath();
if (sourceFilePath == null) return;

// 读取源文件
LogHelper.Handle("正在提取需要翻译的文本...");
var fileHandler = new FileHandler(sourceFilePath, sourceCode, targetCode);
var textFilePath = fileHandler.ReadSourceFile();
LogHelper.Success($"已提取需要翻译的文本，保存到：【{textFilePath}】");

// 选择翻译方式
var translatHandler = new TranslatHandler(textFilePath, sourceCode, targetCode);
var translatedLineInfos = await SelectTranslationMethod(fileHandler, translatHandler, textFilePath);

// 保存翻译结果
LogHelper.Handle("正在保存翻译结果...");
var translatedFilePath = fileHandler.RewriteSourceFileForTranslated(translatedLineInfos);
LogHelper.Success($"已将翻译结果保存到新文件：【{translatedFilePath}】");
LogHelper.Success("翻译完成！");
LogHelper.Info("按任意键退出。");
Console.ReadKey();

/// <summary>
/// 选择语言
/// </summary>
/// <param name="title">提示标题</param>
/// <returns>选中的语言，如果取消则返回null</returns>
static (string Code, string Name)? SelectLanguage(string title)
{
    var languageMenu = new ConsoleMenu
    {
        Title = title
    };

    var languages = LanguageHelper.GetLanguageList();
    foreach (var (code, name) in languages)
    {
        languageMenu.AddItem($"{name} ({code})");
    }

    var choice = languageMenu.Show();

    if (choice == -1)
        return null;

    return languages[choice];
}

/// <summary>
/// 获取源文件路径
/// </summary>
/// <returns>文件路径，如果取消则返回null</returns>
static string? GetSourceFilePath()
{
    while (true)
    {
        LogHelper.Handle("请输入需要翻译的文件路径(包含全路径和文件扩展名)：");
        string? sourceFilePath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(sourceFilePath))
        {
            var retryMenu = new ConsoleMenu
            {
                Title = "文件路径不能为空！",
                ShowExitOption = false
            };
            retryMenu.AddItem("重新输入");
            retryMenu.AddItem("退出程序");

            var choice = retryMenu.Show();
            if (choice == 1) // 退出程序
                return null;
            continue; // 重新输入
        }

        if (!File.Exists(sourceFilePath))
        {
            var fileNotExistMenu = new ConsoleMenu
            {
                Title = "文件不存在！",
                ShowExitOption = false
            };
            fileNotExistMenu.AddItem("重新输入路径");
            fileNotExistMenu.AddItem("退出程序");

            var choice = fileNotExistMenu.Show();
            if (choice == 1) // 退出程序
                return null;
            continue; // 重新输入
        }

        return sourceFilePath;
    }
}

/// <summary>
/// 选择翻译方式
/// </summary>
/// <param name="fileHandler">文件处理器</param>
/// <param name="translatHandler">翻译处理器</param>
/// <param name="textFilePath">文本文件路径</param>
/// <returns>翻译结果</returns>
static async Task<Dictionary<int, string>> SelectTranslationMethod(FileHandler fileHandler, TranslatHandler translatHandler, string textFilePath)
{
    var translationMenu = new ConsoleMenu
    {
        Title = "请选择翻译方式"
    };
    translationMenu.AddItem("自动翻译（使用Google翻译）");
    translationMenu.AddItem("手动翻译（生成模板文件供手动翻译）");

    var choice = translationMenu.Show();

    if (choice == -1) // 用户取消
    {
        LogHelper.Info("用户取消操作");
        Environment.Exit(0);
    }

    if (choice == 1) // 手动翻译
    {
        return await HandleManualTranslation(fileHandler, textFilePath);
    }
    else // 自动翻译
    {
        LogHelper.Handle("正在翻译文本...");
        var result = await translatHandler.TranslateLines();
        LogHelper.Info("文本翻译完成！");
        return result;
    }
}

/// <summary>
/// 处理手动翻译
/// </summary>
/// <param name="fileHandler">文件处理器</param>
/// <param name="textFilePath">文本文件路径</param>
/// <returns>翻译结果</returns>
static async Task<Dictionary<int, string>> HandleManualTranslation(FileHandler fileHandler, string textFilePath)
{
    LogHelper.Info("请手动翻译文本，完成后继续。");
    string translatedTemplateFilePath = fileHandler.GenerateTranslatedFileTemplate();
    LogHelper.Info($"已生成翻译文件模板：【{translatedTemplateFilePath}】");
    LogHelper.Info($"请将文本信息文件的内容自行翻译：【{textFilePath}】");
    LogHelper.Info($"翻译后复制至：【{translatedTemplateFilePath}】");

    while (true)
    {
        var finishMenu = new ConsoleMenu
        {
            Title = "手动翻译状态检查",
            ShowExitOption = false
        };
        finishMenu.AddItem("已完成翻译");
        finishMenu.AddItem("尚未完成");
        finishMenu.AddItem("取消操作");

        var choice = finishMenu.Show();

        switch (choice)
        {
            case 0: // 已完成翻译
                return fileHandler.ReadTranslatedFile();

            case 1: // 尚未完成
                LogHelper.Info("请继续完成翻译...");
                continue;
            case 2: // 取消操作
            default:
                LogHelper.Info("用户取消操作");
                Environment.Exit(0);
                return [];
        }
    }
}