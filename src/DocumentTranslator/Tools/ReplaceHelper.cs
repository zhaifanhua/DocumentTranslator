namespace DocumentTranslator.Tools;

public static class ReplaceHelper
{
    /// <summary>
    /// 替换特殊字符
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    public static string ReplaceSpecialChar(string line)
    {
        return line
            .Replace(@"\n", "`n")
            .Replace(@"\t", "`t")
            .Replace(@"\r", "`r")
            .Replace(@"#", "`f")
            .Replace("\t", "`k");
    }

    /// <summary>
    /// 回滚替换特殊字符
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    public static string ReplaceSpecialCharBack(string line)
    {
        return line
            .Replace("`n", @"\n")
            .Replace("`t", @"\t")
            .Replace("`r", @"\r")
            .Replace("`f", "#")
            .Replace("`k", "\t");
    }
}