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
    private readonly string _chineseFilePath;
    private readonly string _translatedFileTemplate;
    private readonly string _translatedFilePath;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sourceFilePath"></param>
    public FileHandler(string sourceFilePath)
    {
        _sourceFilePath = sourceFilePath;
        _chineseFilePath = $"{_sourceFilePath.Replace(FileHelper.GetExtension(_sourceFilePath), "")}_chinese.txt";
        _translatedFileTemplate = $"{_sourceFilePath.Replace(FileHelper.GetExtension(_sourceFilePath), "")}_translated_template.txt";
        _translatedFilePath = $"{_sourceFilePath.Replace(FileHelper.GetExtension(_sourceFilePath), "")}_translated{FileHelper.GetExtension(_sourceFilePath)}";
    }

    /// <summary>
    /// 读取源文件，并返回包含中文文件的路径
    /// </summary>
    /// <param name="sourceFilePath"></param>
    /// <returns></returns>
    public string ReadSourceFile()
    {
        string text = File.ReadAllText(_sourceFilePath);
        string[] sourceLines = GetFileSplitLines(text);
        // 记录行号和行内容
        var sourceChineseLines = new Dictionary<int, string>();
        for (int i = 0; i < sourceLines.Length; i++)
        {
            if (Regex.IsMatch(sourceLines[i], @"[\u4e00-\u9fa5]"))
            {
                // 替换特殊字符
                sourceLines[i] = ReplaceHelper.ReplaceSpecialChar(sourceLines[i]);
                sourceChineseLines.Add(i, @$"{sourceLines[i]}");
            }
        }

        var sb = new StringBuilder();
        foreach (var (lineNumber, lineContent) in sourceChineseLines)
        {
            sb.AppendLine($"{lineNumber}{Environment.NewLine}{lineContent}");
        }
        var fileStr = sb.ToString();

        // 按源文件路径和名称，保存到新文件，文件名后缀为.chinese.txt，返回新文件路径
        File.WriteAllText(_chineseFilePath, fileStr);
        return _chineseFilePath;
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
    /// 读取翻译后文件，并包装为行号和行内容的字典
    /// 用于根据生成的中文行文件手动翻译
    /// </summary>
    /// <param name="sourceFilePath"></param>
    /// <param name="translatedLineInfos"></param>
    /// <returns></returns>
    public Dictionary<int, string> ReadTranslatedFile()
    {
        string text = File.ReadAllText(_translatedFileTemplate);
        string[] translatedLines = GetFileSplitLines(text);

        // 记录行号和行内容，每两行为一组，第一行为行号，第二行为行内容
        var translatedLineInfos = new Dictionary<int, string>();
        for (int i = 0; i < translatedLines.Length; i += 2)
        {
            translatedLineInfos.Add(int.Parse(translatedLines[i]), translatedLines[i + 1]);
        }

        return translatedLineInfos;
    }

    /// <summary>
    /// 将翻译后的文本原位替换后，重写为新文件，并返回翻译后文件的路径
    /// </summary>
    /// <param name="sourceFilePath"></param>
    /// <param name="translatedLineInfos"></param>
    /// <returns></returns>
    public string RewriteSourceFileForTranslated(Dictionary<int, string> translatedLineInfos)
    {
        string text = File.ReadAllText(_sourceFilePath);
        string[] sourceLines = GetFileSplitLines(text);

        // 按照翻译行信息找到源文件中文行，替换为翻译后的文本
        translatedLineInfos.ToList().ForEach(translatedLineInfo =>
        {
            // 替换特殊字符
            var line = ReplaceHelper.ReplaceSpecialCharBack(translatedLineInfo.Value);
            sourceLines[translatedLineInfo.Key] = line;
        });

        // 重写为新文件
        File.WriteAllText(_translatedFilePath, string.Join(Environment.NewLine, sourceLines));
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