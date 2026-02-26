#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// FileName:LanguageHelper
// Guid:a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/10/29 10:00:00
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace DocumentTranslator.Translators;

/// <summary>
/// 语言辅助类
/// </summary>
public static class LanguageHelper
{
    /// <summary>
    /// 支持的语言列表
    /// </summary>
    public static readonly Dictionary<string, string> SupportedLanguages = new()
    {
        { "zh-CN", "简体中文" },
        { "zh-TW", "繁体中文" },
        { "en", "英语" },
        { "ja", "日语" },
        { "ko", "韩语" },
        { "fr", "法语" },
        { "de", "德语" },
        { "es", "西班牙语" },
        { "pt", "葡萄牙语" },
        { "ru", "俄语" },
        { "it", "意大利语" },
        { "ar", "阿拉伯语" },
        { "nl", "荷兰语" },
        { "pl", "波兰语" },
        { "th", "泰语" },
        { "vi", "越南语" },
        { "tr", "土耳其语" },
        { "cs", "捷克语" },
        { "sv", "瑞典语" },
        { "no", "挪威语" }
    };

    /// <summary>
    /// 获取语言显示名称
    /// </summary>
    /// <param name="langCode">语言代码</param>
    /// <returns>语言显示名称</returns>
    public static string GetLanguageDisplayName(string langCode)
    {
        return SupportedLanguages.TryGetValue(langCode, out var name) 
            ? $"{name} ({langCode})" 
            : langCode;
    }

    /// <summary>
    /// 获取语言列表（用于菜单显示）
    /// </summary>
    /// <returns>语言列表</returns>
    public static List<(string Code, string Name)> GetLanguageList()
    {
        return SupportedLanguages.Select(kvp => (kvp.Key, kvp.Value)).ToList();
    }

    /// <summary>
    /// 获取文件扩展名对应的语言代码（智能检测）
    /// </summary>
    /// <param name="filename">文件名</param>
    /// <returns>可能的语言代码</returns>
    public static string? DetectLanguageFromFilename(string filename)
    {
        var lowerName = filename.ToLowerInvariant();
        
        if (lowerName.Contains("_zh") || lowerName.Contains("中文"))
            return "zh-CN";
        if (lowerName.Contains("_en") || lowerName.Contains("english"))
            return "en";
        if (lowerName.Contains("_ja") || lowerName.Contains("日语"))
            return "ja";
        if (lowerName.Contains("_ko") || lowerName.Contains("韩语"))
            return "ko";
        
        return null; // 无法从文件名判断
    }
}

