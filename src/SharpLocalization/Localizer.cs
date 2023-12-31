﻿using System.Globalization;
using SharpLocalization.Abstraction;

namespace SharpLocalization;

public class Localizer : ILocalizer
{
    private readonly string[] _supportLanguage = new[]
    {
        "en-US", //미국 
        "ko-KR", //한국
        "zh-CN", //중국
        "ja-JP"  //일본
    };

    public string this[string resCode]
    {
        get
        {
            var resName = resCode.Substring(0, 3).ToLower();
            var supportLang = this._supportLanguage.FirstOrDefault(m => m.Contains(CultureInfo.CurrentCulture.Name));
            if (!string.IsNullOrWhiteSpace(supportLang))
            {
                if (LocalizerLoader.Instance.Loader.TryGetValue($"{resName}.{supportLang}",
                        out Dictionary<string, string> val))
                {
                    if (val.TryGetValue(resCode, out string result))
                    {
                        return result;
                    }
                }
            }

            return string.Empty;
        }
    }
}