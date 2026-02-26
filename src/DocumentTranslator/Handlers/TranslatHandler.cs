using DocumentTranslator.Tools;
using DocumentTranslator.Translators;
using System.Collections.Concurrent;
using XiHan.Framework.Utils.Logging;

namespace DocumentTranslator.Handlers;

/// <summary>
/// 翻译处理器
/// </summary>
public class TranslatHandler
{
    private readonly string _chineseFilePath;
    private readonly GoogleTranslator _translator = new();

    /// <summary>
    /// 构造函数
    /// </summary>
    public TranslatHandler(string chineseFilePath)
    {
        _chineseFilePath = chineseFilePath;
    }

    /// <summary>
    /// 翻译中文行
    /// </summary>
    /// <returns></returns>
    public async Task<Dictionary<int, string>> TranslateChineseLines(int? maxDegreeOfParallelism = null)
    {
        var chineseLines = File.ReadAllLines(_chineseFilePath);

        // 中文行文件为行号和行内容各占一行
        var linesByContent = new Dictionary<string, List<int>>(StringComparer.Ordinal);
        for (var i = 0; i + 1 < chineseLines.Length; i += 2)
        {
            if (!int.TryParse(chineseLines[i], out var lineNo))
            {
                continue;
            }
            var lineContent = chineseLines[i + 1];
            if (!linesByContent.TryGetValue(lineContent, out var lineNos))
            {
                lineNos = [];
                linesByContent[lineContent] = lineNos;
            }
            lineNos.Add(lineNo);
        }

        var totalLines = linesByContent.Sum(item => item.Value.Count);
        if (totalLines == 0)
        {
            LogHelper.Info("未发现需要翻译的中文行。");
            return new Dictionary<int, string>();
        }
        var maxDegree = Math.Clamp(maxDegreeOfParallelism ?? Environment.ProcessorCount, 2, 8);
        LogHelper.Info($"需要翻译{totalLines}行中文行，使用并发数：{maxDegree}");

        var translatedLineInfos = new ConcurrentDictionary<int, string>();
        using var progressBar = new ConsoleProgressBar(totalLines);

        var options = new ParallelOptions { MaxDegreeOfParallelism = maxDegree };
        await Parallel.ForEachAsync(linesByContent, options, async (item, _) =>
        {
            var lineContent = item.Key;
            var lineNos = item.Value;
            var translatedLine = string.IsNullOrWhiteSpace(lineContent)
                ? string.Empty
                : await Translate(lineNos[0], lineContent);

            foreach (var lineNo in lineNos)
            {
                translatedLineInfos[lineNo] = translatedLine;
            }
            progressBar.Increment(lineNos.Count);
        });

        return translatedLineInfos.ToDictionary(item => item.Key, item => item.Value);
    }

    /// <summary>
    /// 翻译
    /// </summary>
    /// <param name="lineContent"></param>
    /// <returns></returns>
    private async Task<string> Translate(int lineNo, string lineContent)
    {
        // 翻译中文行
        var translatedLine = string.Empty;
        try
        {
            translatedLine = await _translator.Translate(lineContent, "zh-CN", "en");
        }
        catch (Exception ex)
        {
            LogHelper.Warn($"翻译中文行失败，源文件行号：{lineNo}，源文件行内容：{lineContent}，异常：{ex.Message}");
        }
        return translatedLine;
    }
}