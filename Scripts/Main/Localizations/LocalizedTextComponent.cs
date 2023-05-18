using Cysharp.Threading.Tasks;
using UnityEngine;

namespace smApplication.Scripts.Main.Localizations
{
    public abstract class LocalizedTextComponent : MonoBehaviour
    {
        // protected LocalizationService _localization;

        private async void Start()
        {
            // while (!_localization.IsLoaded)
            {
                await UniTask.NextFrame();
            }
            
            SetText();
        }

        protected abstract void SetText();
    }
}