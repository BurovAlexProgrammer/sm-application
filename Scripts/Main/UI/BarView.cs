using DG.Tweening;
using sm_application.Extension;
using UnityEngine;
using UnityEngine.UI;

namespace sm_application.UI
{
    public class BarView : MonoBehaviour
    {
        [SerializeField] private Image _filler;
        [SerializeField, ReadOnlyField] private float _currentValue;
        [SerializeField, ReadOnlyField] private float _maxValue;
        [SerializeField, ReadOnlyField] private float _fillAmount;

        public void Init(float currentValue, float maxValue)
        {
            _maxValue = maxValue;
            SetValue(currentValue, true);
        }

        public void SetValue(float value, bool fast = false)
        {
            _currentValue = value;
            _fillAmount = _currentValue / _maxValue;
            Fill();
        }

        private void Fill(bool fast = false)
        {
            if (fast)
            {
                _filler.fillAmount = _fillAmount;
            }
            else
            {
                _filler.DOComplete();
                _filler.DOFillAmount(_fillAmount, 0.2f);
            }
        }
    }
}
