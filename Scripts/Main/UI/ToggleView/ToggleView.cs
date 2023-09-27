using System;
using sm_application.Extension;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace sm_application.UI.ToggleView
{
    [RequireComponent(typeof(UnityEngine.UI.Toggle))]
    public class ToggleView : MonoBehaviour
    {
        [SerializeField] private Image _handleImage;
        [SerializeField] private Image _handleBack;
        [SerializeField] private Color _activeHandleColor;
        [SerializeField] private Color _activeBackColor;

        private UnityEngine.UI.Toggle _toggle;
        private RectTransform _handleRect;
        private Vector3 _handleInactivePosition;
        private Vector3 _handleActivePosition;
        private Color _inactiveHandleColor;
        private Color _inactiveBackColor;

        private void Awake()
        {
            _toggle = GetComponent<UnityEngine.UI.Toggle>();
            _handleRect = _handleImage.GetComponent<RectTransform>();
            _handleInactivePosition = _handleRect.anchoredPosition;
            _handleActivePosition.Set(x: -_handleRect.rect.width / 2);
            _inactiveHandleColor = _handleImage.color;
            _inactiveBackColor = _handleBack.color;
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(OnSwitch);
            OnSwitch(_toggle.isOn);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnSwitch);
        }

        private void OnSwitch(bool newState)
        {
            var handlePosition = newState ? _handleActivePosition : _handleInactivePosition;
            var handleColor = newState ? _activeBackColor : _inactiveBackColor;
            var backColor = newState ? _activeHandleColor : _inactiveHandleColor; 
            _handleRect.DOAnchorPos(handlePosition, 0.4f).SetEase(Ease.InOutBack).SetUpdate(true);
            _handleBack.DOColor(handleColor, 0.6f).SetUpdate(true);
            _handleImage.DOColor(backColor, 0.4f).SetUpdate(true);
        }
    }
}