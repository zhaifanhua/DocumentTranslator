using Newtonsoft.Json.Linq;
using XiHan.Framework.Utils.Http;

namespace DocumentTranslator.Translators;

/// <summary>
/// 谷歌翻译提供者
/// </summary>
public class GoogleTranslator
{
    /// <summary>
    /// 翻译
    /// </summary>
    /// <param name="sourceText">源文本</param>
    /// <param name="sourceLanguage">源语言</param>
    /// <param name="targetLanguage">目标语言</param>
    /// <returns>翻译结果</returns>
    public async Task<string> Translate(string sourceText, string sourceLanguage = "zh-CN", string targetLanguage = "en")
    {
        //  curl 'https://translate.google.com/translate_a/single?client=gtx&sl=zh-CN&tl=en&dt=t&q=%E7%90%86%E6%83%B3'
        string url = $"https://translate.google.com/translate_a/single?" +
                     $"client=gtx&sl={sourceLanguage}&tl={targetLanguage}&dt=t" +
                     $"&q={sourceText}";
        try
        {
            // 读取响应内容
            var rawData = await HttpHelper.GetAsync<string>(url, null);
            if (string.IsNullOrWhiteSpace(rawData))
            {
                throw new Exception("翻译失败！");
            }
            var result = JArray.Parse(rawData)[0][0]?[0]?.ToString();
            return result ?? string.Empty;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"出现错误: {ex.Message}");
        }

        return string.Empty;
    }
}