using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using smApplication.Scripts.Extension;
using smApplication.Scripts.Main.Localizations;
using smApplication.Scripts.Main.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace smApplication.Scripts.Main.Services
{
    public class LocalizationService : IService, IConstruct
    {
        private Locales _currentLocale;
        private Dictionary<Locales, Localization> _localizations;
        private Localization _currentLocalization;
        private bool _isLoaded;

        public Dictionary<Locales, Localization> Localizations => _localizations;
        public bool IsLoaded => _isLoaded;
        
        public async void Construct()
        {
            _currentLocale = Services.Get<SettingsService>().GameSettings.CurrentLocale;
            _localizations = new Dictionary<Locales, Localization>();
            var resourceLocations = await Addressables.LoadResourceLocationsAsync("locale").Task;
            var tasks = resourceLocations
                .Where(x => x.ToString().Contains(".csv"))
                .Select(x => Addressables.LoadAssetAsync<TextAsset>(x).Task).ToList();

            await Task.WhenAll(tasks);

            for (var i = 0; i < tasks.Count; i++)
            {
                var localeText = tasks[i].Result;
                var filePath = resourceLocations[i].ToString();
                var localization = LoadLocaleFile(localeText, filePath);
                _localizations.Add(localization.Locale, localization);
            }

            if (!_localizations.ContainsKey(_currentLocale))
                throw new Exception("Current localization not found.");

            _currentLocalization = _localizations[_currentLocale];
            _isLoaded = true;
        }

        public async UniTask<Dictionary<Locales, Localization>> GetLocalizationsAsync()
        {
            while (!_isLoaded)
            {
                await UniTask.NextFrame();
            }

            return _localizations;
        }

        private Localization LoadLocaleFile(TextAsset textAsset, string filePath)
        {
            var lines = textAsset.text.SplitLines();
            var locale = lines[0];
            var formatInfoMaybeJson = lines[1];
            var hint = lines[2];

            var itemList = new List<string>();
            for (var i = 3; i < lines.Length; i++)
            {
                itemList.Add(lines[i]);
            }

            return new Localization(locale, hint, formatInfoMaybeJson, itemList.ToArray(), filePath);
        }
        
        public string GetLocalizedText(string key)
        {
            if (_currentLocalization.LocalizedItems.ContainsKey(key) == false)
            {
                AddNewLocaleKeyToFiles(key);
            }
            
            return _currentLocalization.LocalizedItems[key].Text;
        }

        private void AddNewLocaleKeyToFiles(string newKey)
        {
            if (Application.isEditor)
            {
                Debug.LogError($"Key '{newKey}' not in current locale '{_currentLocale.ToString()}'.");
                
                foreach (var localization in _localizations.Values)
                {
                    if (localization.LocalizedItems.ContainsKey(newKey) == false)
                    {
                        Debug.LogWarning($"Key '{newKey}' is not in locale '{localization.Locale.ToString()}'. Adding new key..");
                        var fullPath = Path.Combine(Application.dataPath, @"..\") + localization.FilePathInEditor;
                        using var streamWriter = File.AppendText(fullPath);
                        streamWriter.WriteLine($"{newKey};;;key.{newKey};");
                        var newLocalizedItem = new LocalizedItem { Key = newKey, Text = $"key^{newKey}" };
                        localization.LocalizedItems.Add(newKey, newLocalizedItem);
                    }
                }
            }
            else
            {
                Debug.LogError($"Key '{newKey}' not in current locale '{_currentLocale.ToString()}'");
            }
        }
    }
}