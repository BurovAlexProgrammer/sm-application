using System;
using System.Collections.Generic;
using System.IO;
using sm_application.Localizations;
using UnityEngine;

namespace sm_application.Service
{
    public class LocalizationService : IService
    {
        private Locales _currentLocale;
        private Dictionary<Locales, Localization> _localizations;
        private Localization _currentLocalization;
        private bool _isLoaded;

        public event Action LocalizationChanged;

        public Dictionary<Locales, Localization> Localizations => _localizations;
        public bool IsLoaded => _isLoaded;
        public Locales CurrentLocale => _currentLocale;

        public void Construct()
        {
            _currentLocale = Services.Get<SettingsService>().GameSettings.CurrentLocale;
            _localizations = new Dictionary<Locales, Localization>();
        }

        public string GetLocalizedText(string key)
        {
            if (!_isLoaded) return null;

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
                Debug.LogError($"Key '{newKey}' not in current locale '{CurrentLocale.ToString()}'.");
                
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
                Debug.LogError($"Key '{newKey}' not in current locale '{CurrentLocale.ToString()}'");
            }
        }

        public void SetLoadedLocalizations(Localization currentLocalization, Dictionary<Locales, Localization> localizations)
        {
            _currentLocalization = currentLocalization;
            _localizations = localizations;
            _isLoaded = true;
            LocalizationChanged?.Invoke();
        }
        
        public void SetCurrentLocalization(Localization currentLocalization)
        {
            if (_currentLocalization == currentLocalization) return;
            
            _currentLocalization = currentLocalization;
            LocalizationChanged?.Invoke();
        }
    }
}