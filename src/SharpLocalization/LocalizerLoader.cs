﻿using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;

namespace SharpLocalization;

public class LocalizerLoader
{
    public static LocalizerLoader Instance => _instance.Value;

    private static Lazy<LocalizerLoader> _instance =
        new Lazy<LocalizerLoader>(() => new LocalizerLoader());

    private const string LANG_PATH_NAME = "language";
    private readonly string LANG_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LANG_PATH_NAME);
    public ConcurrentDictionary<string, Dictionary<string, string>> Loader { get; private set; } = new();

    private LocalizerLoader()
    {
        ReadLanguageFile();
    }

    private void ReadLanguageFile()
    {
        if (Loader.Count > 0) Loader.Clear();

        var files = Directory.GetFiles(LANG_PATH);
        foreach (var file in files)
        {
            var jsonData = File.ReadAllText(file, Encoding.UTF8);
            var map = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonData);

            Loader.TryAdd(Path.GetFileNameWithoutExtension(file), map);
        }
    }
}