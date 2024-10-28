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

using XiHan.Framework.Utils.System;

namespace DocumentTranslator;

/// <summary>
/// 翻译处理器
/// </summary>
public class TranslatHandler
{
    private readonly string _chineseFilePath;

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
    public Dictionary<int, string> TranslateChineseLines()
    {
        var translatedLineInfos = new Dictionary<int, string>();
        var chineseLines = File.ReadAllLines(_chineseFilePath);
        ConsoleHelper.Info($"需要翻译{chineseLines.Length / 2}行中文行，进度：");
        // 中文行文件为行号和行内容各占一行
        for (var i = 0; i < chineseLines.Length; i += 2)
        {
            var lineNo = int.Parse(chineseLines[i]);
            var lineContent = chineseLines[i + 1];
            // 翻译中文行
            var translatedLine = Translate(lineNo, lineContent);
            translatedLineInfos.Add(lineNo, translatedLine);

            // 计算进度百分比
            double progress = (i + 1) / (double)chineseLines.Length * 100;
            ConsoleHelper.Handle($"{progress:F2}%");
        }
        return translatedLineInfos;
    }

    /// <summary>
    /// 翻译
    /// </summary>
    /// <param name="lineContent"></param>
    /// <returns></returns>
    private string Translate(int lineNo, string lineContent)
    {
        // 翻译中文行
        var translatedLine = string.Empty;
        try
        {
            // TODO: 对接翻译
            //var translator = new GoogleTranslator();
            //translatedLine = translator.Translate(lineContent, "zh", "en");
        }
        catch (Exception ex)
        {
            ConsoleHelper.Warning($"翻译中文行失败，源文件行号：{lineNo}，源文件行内容：{lineContent}，异常：{ex.Message}");
        }
        return translatedLine;
    }
}