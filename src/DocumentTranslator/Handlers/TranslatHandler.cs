#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// FileName:TranslatHandler
// Guid:05971664-af29-406d-8a53-0e72efae09df
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/10/29 3:42:19
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using DocumentTranslator.Translators;
using XiHan.Framework.Utils.Logging;
using XiHan.Framework.Utils.ConsoleTools;
using System.Text;

namespace DocumentTranslator.Handlers;

/// <summary>
/// 翻译处理器
/// </summary>
public class TranslatHandler
{
    private readonly string _chineseFilePath;
    private readonly string _sourceLanguage;
    private readonly string _targetLanguage;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="chineseFilePath">源文本文件路径</param>
    /// <param name="sourceLanguage">源语言代码</param>
    /// <param name="targetLanguage">目标语言代码</param>
    public TranslatHandler(string chineseFilePath, string sourceLanguage = "zh-CN", string targetLanguage = "en")
    {
        _chineseFilePath = chineseFilePath;
        _sourceLanguage = sourceLanguage;
        _targetLanguage = targetLanguage;
    }

    /// <summary>
    /// 翻译文本段落
    /// </summary>
    /// <returns>翻译结果字典（key为起始行号，value为翻译后的段落内容）</returns>
    public async Task<Dictionary<int, string>> TranslateLines()
    {
        var translatedParagraphs = new Dictionary<int, string>();
        var sourceLines = File.ReadAllLines(_chineseFilePath);

        // 按段落解析（第一行为行号，后续行为段落内容）
        var paragraphs = new List<(int StartLine, string Content)>();
        var currentParagraphBuilder = new StringBuilder();
        int? currentStartLine = null;

        for (int i = 0; i < sourceLines.Length; i++)
        {
            var line = sourceLines[i];

            // 如果遇到行号行
            if (int.TryParse(line.Trim(), out var lineNumber))
            {
                // 保存上一个段落
                if (currentStartLine.HasValue && currentParagraphBuilder.Length > 0)
                {
                    var paragraphText = currentParagraphBuilder.ToString().TrimEnd(Environment.NewLine.ToCharArray());
                    paragraphs.Add((currentStartLine.Value, paragraphText));
                    currentParagraphBuilder.Clear();
                }

                // 开始新段落
                currentStartLine = lineNumber;
            }
            else
            {
                // 累积段落内容
                if (currentStartLine.HasValue)
                {
                    currentParagraphBuilder.AppendLine(line);
                }
            }
        }

        // 保存最后一个段落
        if (currentStartLine.HasValue && currentParagraphBuilder.Length > 0)
        {
            var paragraphText = currentParagraphBuilder.ToString().TrimEnd(Environment.NewLine.ToCharArray());
            paragraphs.Add((currentStartLine.Value, paragraphText));
        }

        if (paragraphs.Count == 0)
        {
            LogHelper.Warn("源文件中没有找到需要翻译的文本");
            return translatedParagraphs;
        }

        LogHelper.Info($"需要翻译 {paragraphs.Count} 个段落");

        // 初始化多任务进度条
        using var multiProgressBar = new ConsoleMultiProgressBar();
        multiProgressBar.AddTask("translation", paragraphs.Count, "翻译进度", 50);

        // 按段落翻译
        var processedCount = 0;
        foreach (var (startLine, content) in paragraphs)
        {
            // 更新进度条显示当前处理的段落
            multiProgressBar.UpdateTask("translation", processedCount, $"正在翻译段落 {startLine}...");

            // 翻译段落
            var translatedParagraph = await Translate(startLine, content);
            translatedParagraphs.Add(startLine, translatedParagraph);

            // 更新进度
            processedCount++;
            multiProgressBar.IncrementTask("translation", 1, $"已完成段落 {startLine}");

            // 等待片刻，避免请求过于频繁
            await Task.Delay(100);
        }

        multiProgressBar.CompleteTask("translation", "翻译完成！");

        return translatedParagraphs;
    }

    /// <summary>
    /// 翻译
    /// </summary>
    /// <param name="lineNo">行号</param>
    /// <param name="lineContent">行内容</param>
    /// <returns>翻译结果</returns>
    private async Task<string> Translate(int lineNo, string lineContent)
    {
        var translatedLine = string.Empty;
        try
        {
            var translator = new GoogleTranslator();
            translatedLine = await translator.Translate(lineContent, _sourceLanguage, _targetLanguage);

            // 如果翻译为空，使用原文本作为备用
            if (string.IsNullOrWhiteSpace(translatedLine))
            {
                LogHelper.Warn($"第 {lineNo} 行翻译结果为空，使用原文本：{lineContent}");
                translatedLine = lineContent;
            }
        }
        catch (Exception ex)
        {
            LogHelper.Warn($"翻译第 {lineNo} 行失败，将使用原文本。错误信息：{ex.Message}");
            translatedLine = lineContent; // 使用原文本作为备用
        }
        return translatedLine;
    }
}