using DocumentTranslator;
using XiHan.Framework.Utils.System;

ConsoleHelper.Handle("请输入需要翻译文件（中文=>英文）的路径：");
string? sourceFilePath = Console.ReadLine();
if (string.IsNullOrWhiteSpace(sourceFilePath))
{
    ConsoleHelper.Danger("文件路径不能为空！");
    return;
}
if (!File.Exists(sourceFilePath))
{
    ConsoleHelper.Danger("文件不存在！");
    return;
}

// 读取源文件
ConsoleHelper.Handle("正在读取源文件...");
var fileHandler = new FileHandler(sourceFilePath);
var chineseFilePath = fileHandler.ReadSourceFile();
ConsoleHelper.Info($"已读取源文件中的中文行：【{sourceFilePath}】");
ConsoleHelper.Info($"中文行信息保存到新文件：【{chineseFilePath}】");

// 是否需要手动翻译
var translatHandler = new TranslatHandler(chineseFilePath);
var translatedLineInfos = new Dictionary<int, string>();
ConsoleHelper.Handle("是否需要根据中文行信息手动翻译？【Y/N】");
string? needManualTranslate = Console.ReadLine();
if (string.IsNullOrWhiteSpace(needManualTranslate))
{
    ConsoleHelper.Danger("请输入 【Y/N】！");
    return;
}
if (needManualTranslate.ToUpper().Equals("Y"))
{
    ConsoleHelper.Info("请手动翻译中文行，完成后继续。");
    string translatedTemplateFilePath = fileHandler.GenerateTranslatedFileTemplate();
    ConsoleHelper.Info($"已生成翻译文件模板：【{translatedTemplateFilePath}】");
    ConsoleHelper.Info($"请将中文行信息文件的内容自行翻译：【{chineseFilePath}】");
    ConsoleHelper.Info($"翻译后复制至：【{translatedTemplateFilePath}】");
    ConsoleHelper.Handle("正在等待完成手动翻译...");

    while (true)
    {
        ConsoleHelper.Handle("是否已完成手动翻译？【Y/N】");
        string? isFinished = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(isFinished))
        {
            ConsoleHelper.Danger("请输入 【Y/N】！");
            continue;
        }
        if (isFinished.ToUpper().Equals("Y"))
        {
            break;
        }
    }
    translatedLineInfos = fileHandler.ReadTranslatedFile();
}
else
{
    // 翻译中文行
    ConsoleHelper.Handle("正在翻译中文行...");
    translatedLineInfos = translatHandler.TranslateChineseLines();
    ConsoleHelper.Info("中文行翻译完成！");
}
// 保存翻译结果
ConsoleHelper.Handle("正在保存翻译结果...");
var translatedFilePath = fileHandler.RewriteSourceFileForTranslated(translatedLineInfos);
ConsoleHelper.Info($"已将翻译结果保存到新文件：【{translatedFilePath}】");
ConsoleHelper.Success("翻译完成！");
ConsoleHelper.Info("按任意键退出。");
Console.ReadKey();