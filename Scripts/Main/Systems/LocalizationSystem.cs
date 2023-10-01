using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sm_application.Extension;
using sm_application.Localizations;
using sm_application.Scripts.Main.Events;
using sm_application.Service;
using sm_application.Wrappers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace sm_application.Systems
{
    public class LocalizationSystem: BaseSystem
    {
        private LocalizationService _localizationService;
        
        public override async void Init()
        {
            _localizationService = Services.Get<LocalizationService>();
            var localizations = new Dictionary<Locales, Localization>();
            Localization currentLocalization;
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
                localizations.Add(localization.Locale, localization);
            }

            if (!localizations.ContainsKey(_localizationService.CurrentLocale))
                throw new Exception("Current localization not found.");

            
            currentLocalization = localizations[_localizationService.CurrentLocale];
            _localizationService.SetLoadedLocalizations(currentLocalization, localizations);
            
        }

        public override void RemoveEventHandlers()
        {
            RemoveListener<RequireLocalizationChangeEvent>();
            base.RemoveEventHandlers();
        }

        public override void AddEventHandlers()
        {
            AddListener<RequireLocalizationChangeEvent>(RequiredChangeLocalization);
            base.AddEventHandlers();
        }
        
        private void RequiredChangeLocalization(RequireLocalizationChangeEvent currEvent)
        {
            if (_localizationService.Localizations.TryGetValue(currEvent.CurrentLocale, out var localization))
            {
                _localizationService.SetCurrentLocalization(localization);
                return;
            }
            
            Log.Error($"Localization '{currEvent.CurrentLocale.ToString()}' not found.");
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

    }
}