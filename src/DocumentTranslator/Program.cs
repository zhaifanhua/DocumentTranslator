using DocumentTranslator.Handlers;
using XiHan.Framework.Utils.Logging;

LogHelper.Handle("请输入需要翻译文件（中文=>英文）的路径(包含全路径和文件扩展名)：");
string? sourceFilePath = Console.ReadLine();
if (string.IsNullOrWhiteSpace(sourceFilePath))
{
    LogHelper.Error("文件路径不能为空！");
    return;
}
if (!File.Exists(sourceFilePath))
{
    LogHelper.Error("文件不存在！");
    return;
}

// 读取源文件
LogHelper.Handle("正在读取源文件...");
var fileHandler = new FileHandler(sourceFilePath);
var chineseFilePath = fileHandler.ReadSourceFile();
LogHelper.Info($"已读取源文件中的中文行：【{sourceFilePath}】");
LogHelper.Info($"中文行信息保存到新文件：【{chineseFilePath}】");

// 是否需要手动翻译
var translatHandler = new TranslatHandler(chineseFilePath);
var translatedLineInfos = new Dictionary<int, string>();
LogHelper.Handle("是否需要根据中文行信息手动翻译？【手动Y/自动N】");
string? needManualTranslate = Console.ReadLine();
if (string.IsNullOrWhiteSpace(needManualTranslate))
{
    LogHelper.Error("请输入 【Y/N】！");
    return;
}
if (needManualTranslate.ToUpper().Equals("Y"))
{
    LogHelper.Info("请手动翻译中文行，完成后继续。");
    string translatedTemplateFilePath = fileHandler.GenerateTranslatedFileTemplate();
    LogHelper.Info($"已生成翻译文件模板：【{translatedTemplateFilePath}】");
    LogHelper.Info($"请将中文行信息文件的内容自行翻译：【{chineseFilePath}】");
    LogHelper.Info($"翻译后复制至：【{translatedTemplateFilePath}】");
    LogHelper.Handle("正在等待完成手动翻译...");

    while (true)
    {
        LogHelper.Handle("是否已完成手动翻译？【完成Y/未完成N】");
        string? isFinished = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(isFinished))
        {
            LogHelper.Error("请输入 【Y/N】！");
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
    LogHelper.Handle("正在翻译中文行...");
    translatedLineInfos = await translatHandler.TranslateChineseLines();
    LogHelper.Info("中文行翻译完成！");
}
// 保存翻译结果
LogHelper.Handle("正在保存翻译结果...");
var translatedFilePath = fileHandler.RewriteSourceFileForTranslated(translatedLineInfos);
LogHelper.Info($"已将翻译结果保存到新文件：【{translatedFilePath}】");
LogHelper.Success("翻译完成！");
LogHelper.Info("按任意键退出。");
Console.ReadKey();