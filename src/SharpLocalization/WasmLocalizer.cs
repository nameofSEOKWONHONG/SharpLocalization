﻿using System.Globalization;
using System.Net.Http.Json;
using eXtensionSharp;
using SharpLocalization.Abstract;

namespace SharpLocalization;

public class WasmLocalizer : ILocalizer
{
    private static readonly Dictionary<string, Dictionary<string, string>>
        _languages = new();

    public string this[string rescode]
    {
        get
        {
            if(rescode.xIsEmpty()) return String.Empty;
            
            var resGroup = rescode.Substring(0, 3).ToLower();
            if(_languages.TryGetValue($"{resGroup}.{CultureInfo.CurrentCulture.Name}", out Dictionary<string, string> val))
            {
                if (val.TryGetValue(rescode, out string result))
                {
                    return result;
                }
            }

            return string.Empty;
        }
    }

    private readonly HttpClient _client;

    public WasmLocalizer(HttpClient client)
    {
        _client = client;
    }

    public async Task InitializeAsync(string versionString)
    {
        if (_languages.xIsNotEmpty()) _languages.Clear();
        var langJsonItems = new Dictionary<string, Func<Dictionary<string, string>>>()
        {
            {
                "en-US", () =>
                {
                    return new Dictionary<string, string>()
                    {
                        { "lbl.en-US", "language/lbl.en-US.json" },
                        { "msg.en-US", "language/msg.en-US.json" },
                        { "btn.en-US", "language/btn.en-US.json" },
                    };
                }
            },
            {
                "ko-KR", () =>
                {
                    return new Dictionary<string, string>()
                    {
                        { "lbl.ko-KR", "language/lbl.ko-KR.json" },
                        { "msg.ko-KR", "language/msg.ko-KR.json" },
                        { "btn.ko-KR", "language/btn.ko-KR.json" },
                    };
                }
            }
        };

        #region [support .net8]

        //Parallel.ForEach(langJsonItems, async (item) =>
        //{
        //    var resp = await _client.GetFromJsonAsync<Dictionary<string, string>>(item.Value);
        //    if (resp != null)
        //    {
        //        _languages.Add(item.Key, resp);
        //    }
        //});

        #endregion [support .net8]

        var langItems = langJsonItems[CultureInfo.CurrentCulture.Name]();
        foreach (var item in langItems)
        {
            var req = new HttpRequestMessage(HttpMethod.Get, $"{item.Value}?{versionString}");
            var resp = await _client.SendAsync(req);
            var res = await resp.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            if (res.xIsNotEmpty())
            {
                _languages.Add(item.Key, res);
            }
        }
    }
}