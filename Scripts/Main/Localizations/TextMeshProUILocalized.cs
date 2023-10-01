using System;
using sm_application.Service;
using sm_application.Wrappers;
using TMPro;
using UnityEngine;

namespace sm_application.Localizations
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshProUILocalized : LocalizedTextComponent
    {
        [SerializeField] private string _localizedTextKey;
        [SerializeField] private string _prefix;
        [SerializeField] private string _postfix;

        private TextMeshPro _textMesh;
        private TextMeshProUGUI _textMeshUI;
        
        protected override void Awake()
        {
            base.Awake();
            _textMesh = GetComponent<TextMeshPro>();
            _textMeshUI = GetComponent<TextMeshProUGUI>();
            _localizationService.LocalizationChanged += OnLocalizationService;
        }

        private void OnLocalizationService()
        {
            SetText();
        }

        public override void SetText()
        {
            if (string.IsNullOrEmpty(_localizedTextKey))
            {
                SetTextToComponents("---NO KEY---");
                return;
            }

            if (!_localizationService.IsLoaded)
            {
                Log.Exception(new Exception("LocalizationService is not loaded"));
                return;
            }
            
            SetTextToComponents(_prefix + Services.Get<LocalizationService>().GetLocalizedText(_localizedTextKey) + _postfix);
        }

        private void SetTextToComponents(string text)
        {
            if (_textMesh != null)
            {
                _textMesh.text = text;
            }

            if (_textMeshUI != null)
            {
                _textMeshUI.text = text;
            }
        }
    }
}