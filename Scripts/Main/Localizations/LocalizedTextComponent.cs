using sm_application.Service;
using UnityEngine;

namespace sm_application.Localizations
{
    public abstract class LocalizedTextComponent : MonoBehaviour
    {
        protected LocalizationService _localizationService;

        protected virtual void Awake()
        {
            _localizationService = Services.Get<LocalizationService>();
            _localizationService.LocalizationChanged += OnLocalizationChanged;
        }

        private void OnLocalizationChanged()
        {
            SetText();
        }

        private void Start()
        {
            if (!_localizationService.IsLoaded) return;

            SetText();
        }

        public abstract void SetText();
    }
}