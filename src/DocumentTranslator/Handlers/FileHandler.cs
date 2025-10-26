#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// FileName:FileHandler
// Guid:4886d858-7551-49c3-9b02-42984b881115
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/10/29 3:00:32
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using DocumentTranslator.Tools;
using System.Text;
using System.Text.RegularExpressions;
using XiHan.Framework.Utils.IO;

namespace DocumentTranslator.Handlers;

/// <summary>
/// 文件处理器
/// </summary>
public class FileHandler
{
    private readonly string _sourceFilePath;
    private readonly string _extractedTextFilePath;
    private readonly string _translatedFileTemplate;
    private readonly string _translatedFilePath;
    private readonly string _sourceLanguage;
    private readonly string _targetLanguage;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sourceFilePath">源文件路径</param>
    /// <param name="sourceLanguage">源语言</param>
    /// <param name="targetLanguage">目标语言</param>
    public FileHandler(string sourceFilePath, string sourceLanguage = "zh-CN", string targetLanguage = "en")
    {
        _sourceFilePath = sourceFilePath;
        _sourceLanguage = sourceLanguage;
        _targetLanguage = targetLanguage;

        // 获取源文件所在目录
        var sourceDir = Path.GetDirectoryName(_sourceFilePath) ?? "";
        var fileNameWithoutExt = Path.GetFileNameWithoutExtension(_sourceFilePath);
        var fileExtension = Path.GetExtension(_sourceFilePath);

        // 生成提取的文本文件路径（保存到源文件所在目录）
        _extractedTextFilePath = Path.Combine(sourceDir, $"{fileNameWithoutExt}_source_{_sourceLanguage}.txt");

        // 生成翻译模板文件路径
        _translatedFileTemplate = Path.Combine(sourceDir, $"{fileNameWithoutExt}_translated_template.txt");

        // 生成翻译后的文件路径
        _translatedFilePath = Path.Combine(sourceDir, $"{fileNameWithoutExt}_{_sourceLanguage}_to_{_targetLanguage}{fileExtension}");
    }

    /// <summary>
    /// 读取源文件，并返回包含可翻译文本的文件路径（按段落提取）
    /// </summary>
    /// <returns>提取出的文本文件路径</returns>
    public string ReadSourceFile()
    {
        string text = File.ReadAllText(_sourceFilePath);
        string[] sourceLines = GetFileSplitLines(text);

        // 按段落提取文本
        var paragraphsToTranslate = ExtractParagraphs(sourceLines);

        var sb = new StringBuilder();
        foreach (var (startLine, endLine, paragraphText) in paragraphsToTranslate)
        {
            // 使用起始行号作为标识
            sb.AppendLine($"{startLine}{Environment.NewLine}{paragraphText}");
        }
        var fileStr = sb.ToString();

        // 按源文件路径和名称，保存到新文件
        File.WriteAllText(_extractedTextFilePath, fileStr);
        return _extractedTextFilePath;
    }

    /// <summary>
    /// 按段落提取可翻译文本
    /// </summary>
    /// <param name="sourceLines">源文件行数组</param>
    /// <returns>段落列表（起始行号、结束行号、段落内容）</returns>
    private List<(int StartLine, int EndLine, string Content)> ExtractParagraphs(string[] sourceLines)
    {
        var paragraphs = new List<(int StartLine, int EndLine, string Content)>();
        var currentParagraph = new List<string>();
        int currentStartLine = -1;
        int currentEndLine = -1;
        bool inParagraph = false;

        for (int i = 0; i < sourceLines.Length; i++)
        {
            var line = sourceLines[i];
            var trimmedLine = line.Trim();

            // 检查是否是段落开始标记
            if (IsParagraphStart(line) && !inParagraph)
            {
                // 开始新段落
                currentStartLine = i;
                currentEndLine = i;
                currentParagraph.Clear();
                currentParagraph.Add(line);
                inParagraph = true;
            }
            // 检查是否是段落结束标记
            else if (IsParagraphEnd(line) && inParagraph)
            {
                // 结束当前段落并保存
                currentParagraph.Add(line);
                currentEndLine = i;
                var paragraphText = string.Join(Environment.NewLine, currentParagraph);

                if (ShouldExtractParagraph(paragraphText))
                {
                    // 处理段落文本中的特殊字符
                    var processedText = ReplaceHelper.ReplaceSpecialChar(paragraphText);
                    paragraphs.Add((currentStartLine, currentEndLine, processedText));
                }

                currentParagraph.Clear();
                inParagraph = false;
            }
            // 在段落中，继续添加行
            else if (inParagraph)
            {
                currentParagraph.Add(line);
                currentEndLine = i;
            }
        }

        // 处理文件末尾可能未结束的段落
        if (inParagraph && currentParagraph.Count > 0)
        {
            var paragraphText = string.Join(Environment.NewLine, currentParagraph);

            if (ShouldExtractParagraph(paragraphText))
            {
                var processedText = ReplaceHelper.ReplaceSpecialChar(paragraphText);
                paragraphs.Add((currentStartLine, currentEndLine, processedText));
            }
        }

        return paragraphs;
    }

    /// <summary>
    /// 判断是否是段落开始标记
    /// </summary>
    private bool IsParagraphStart(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            return false;

        var trimmed = line.Trim();

        // Markdown 标题
        if (trimmed.StartsWith("#"))
            return true;

        // 列表项（有序或无序列）
        if (Regex.IsMatch(trimmed, @"^[-*+]\s") || Regex.IsMatch(trimmed, @"^\d+[\.\)]\s"))
            return true;

        // HTML 标签
        if (Regex.IsMatch(trimmed, @"^<[^/][^>]*>"))
            return true;

        // 表格行
        if (trimmed.StartsWith("|") && trimmed.EndsWith("|"))
            return true;

        // 代码块标记
        if (trimmed.StartsWith("```") || trimmed.StartsWith("~~~"))
            return true;

        return false;
    }

    /// <summary>
    /// 判断是否是段落结束标记
    /// </summary>
    private bool IsParagraphEnd(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            return true;

        var trimmed = line.Trim();

        // 空行或多行空行表示段落结束
        if (string.IsNullOrWhiteSpace(trimmed))
            return true;

        // 新的标题或列表项
        if (IsParagraphStart(line))
            return true;

        return false;
    }

    /// <summary>
    /// 判断是否应该提取该段落
    /// </summary>
    private bool ShouldExtractParagraph(string paragraphText)
    {
        if (string.IsNullOrWhiteSpace(paragraphText))
            return false;

        // 根据源语言判断是否提取该段落
        switch (_sourceLanguage)
        {
            case "zh-CN":
            case "zh-TW":
                // 中文：检测是否包含中文字符
                return Regex.IsMatch(paragraphText, @"[\u4e00-\u9fa5]");

            case "ja":
                // 日语：检测是否包含日文字符
                return Regex.IsMatch(paragraphText, @"[\u3040-\u309F\u30A0-\u30FF\u4E00-\u9FAF]");

            case "ko":
                // 韩语：检测是否包含韩文字符
                return Regex.IsMatch(paragraphText, @"[\uAC00-\uD7AF]");

            case "ar":
                // 阿拉伯语：检测是否包含阿拉伯字符
                return Regex.IsMatch(paragraphText, @"[\u0600-\u06FF]");

            case "th":
                // 泰语：检测是否包含泰文字符
                return Regex.IsMatch(paragraphText, @"[\u0E00-\u0E7F]");

            default:
                // 其他语言（英文等）：检测是否包含可翻译内容
                // 排除纯代码和格式化内容
                if (paragraphText.Contains("```") || paragraphText.Contains("~~~"))
                {
                    // 代码块，检查是否包含注释
                    var lines = paragraphText.Split([Environment.NewLine, "\n", "\r"], StringSplitOptions.None);
                    foreach (var line in lines)
                    {
                        var trimmed = line.Trim();
                        if (!string.IsNullOrWhiteSpace(trimmed) &&
                            !trimmed.StartsWith("```") &&
                            !trimmed.StartsWith("~~~") &&
                            Regex.IsMatch(trimmed, @"[a-zA-Z]") &&
                            trimmed.Length > 3)
                        {
                            return true;
                        }
                    }
                    return false;
                }

                // 检查是否包含英文文本（至少包含一个字母）
                return Regex.IsMatch(paragraphText, @"[a-zA-Z]") && paragraphText.Trim().Length > 10;
        }
    }

    /// <summary>
    /// 写入空翻译文件模板
    /// 用于根据生成的中文行文件手动翻译
    /// </summary>
    public string GenerateTranslatedFileTemplate()
    {
        File.WriteAllText(_translatedFileTemplate, string.Empty);
        return _translatedFileTemplate;
    }

    /// <summary>
    /// 读取翻译后文件，并包装为段落字典（支持按段落翻译）
    /// </summary>
    /// <returns>段落字典，key为起始行号，value为翻译后的段落内容</returns>
    public Dictionary<int, string> ReadTranslatedFile()
    {
        if (!File.Exists(_translatedFileTemplate))
        {
            return [];
        }

        string text = File.ReadAllText(_translatedFileTemplate);
        string[] translatedLines = GetFileSplitLines(text);

        // 记录段落信息：每两行为一组，第一行为起始行号，第二行为段落内容
        var translatedParagraphs = new Dictionary<int, string>();
        var currentParagraphBuilder = new StringBuilder();
        int? currentStartLine = null;

        for (int i = 0; i < translatedLines.Length; i++)
        {
            var line = translatedLines[i];

            // 如果遇到行号行
            if (int.TryParse(line.Trim(), out var lineNumber))
            {
                // 保存上一个段落
                if (currentStartLine.HasValue && currentParagraphBuilder.Length > 0)
                {
                    var paragraphText = currentParagraphBuilder.ToString().TrimEnd(Environment.NewLine.ToCharArray());
                    translatedParagraphs[currentStartLine.Value] = paragraphText;
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
            translatedParagraphs[currentStartLine.Value] = paragraphText;
        }

        return translatedParagraphs;
    }

    /// <summary>
    /// 将翻译后的文本按段落替换，重写为新文件，并返回翻译后文件的路径
    /// </summary>
    /// <param name="translatedParagraphs">翻译后的段落字典（key为起始行号，value为翻译后的段落内容）</param>
    /// <returns>翻译后文件的路径</returns>
    public string RewriteSourceFileForTranslated(Dictionary<int, string> translatedParagraphs)
    {
        string text = File.ReadAllText(_sourceFilePath);
        string[] sourceLines = GetFileSplitLines(text);

        // 重建段落替换逻辑
        var updatedLines = new string[sourceLines.Length];
        Array.Copy(sourceLines, updatedLines, sourceLines.Length);

        // 按段落进行替换
        foreach (var (startLine, translatedParagraph) in translatedParagraphs.OrderByDescending(kvp => kvp.Key))
        {
            // 从保存的段落中恢复原始行号范围
            var originalParagraphs = ExtractParagraphs(sourceLines);
            var matchingParagraph = originalParagraphs.FirstOrDefault(p => p.StartLine == startLine);

            if (matchingParagraph != default)
            {
                // 替换整个段落
                var translatedText = ReplaceHelper.ReplaceSpecialCharBack(translatedParagraph);
                var translatedLines = GetFileSplitLines(translatedText);

                // 计算需要替换的行范围
                var linesToReplace = matchingParagraph.EndLine - matchingParagraph.StartLine + 1;
                var translatedLinesCount = translatedLines.Length;

                // 如果翻译后的行数相同或更少，直接替换
                if (translatedLinesCount <= linesToReplace)
                {
                    for (int i = 0; i < translatedLinesCount; i++)
                    {
                        if (matchingParagraph.StartLine + i < updatedLines.Length)
                        {
                            updatedLines[matchingParagraph.StartLine + i] = translatedLines[i];
                        }
                    }

                    // 清除多余的行（如果翻译后的行数少于原段落）
                    for (int i = translatedLinesCount; i < linesToReplace; i++)
                    {
                        if (matchingParagraph.StartLine + i < updatedLines.Length)
                        {
                            updatedLines[matchingParagraph.StartLine + i] = string.Empty;
                        }
                    }
                }
                else
                {
                    // 如果翻译后的行数更多，需要插入新行
                    // 这种情况比较复杂，这里简化处理：只替换前 N 行
                    for (int i = 0; i < linesToReplace && i < translatedLinesCount; i++)
                    {
                        if (matchingParagraph.StartLine + i < updatedLines.Length)
                        {
                            updatedLines[matchingParagraph.StartLine + i] = translatedLines[i];
                        }
                    }
                }
            }
        }

        // 重写为新文件
        File.WriteAllText(_translatedFilePath, string.Join(Environment.NewLine, updatedLines));
        return _translatedFilePath;
    }

    /// <summary>
    /// 获取文件的行内容数组
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private static string[] GetFileSplitLines(string text)
    {
        string[] sourceLines1 = text.Split(Environment.NewLine);
        string[] sourceLines2 = text.Split("\n");
        // 比较哪种分隔符分割的行数最多，选择最多的那种
        string[] sourceLines = sourceLines1.Length > sourceLines2.Length ? sourceLines1 : sourceLines2;
        return sourceLines;
    }
}