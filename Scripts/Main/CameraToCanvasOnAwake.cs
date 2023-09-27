using JetBrains.Annotations;
using sm_application.Service;
using UnityEngine;

namespace sm_application
{
    [RequireComponent(typeof(Canvas))]
    public class CameraToCanvasOnAwake : MonoBehaviour
    {
        [SerializeField][UsedImplicitly] private ScreenService.CameraType _cameraType = ScreenService.CameraType.MainCamera;
        [SerializeField] private float _planeDistance = 0.1f;
        
        private void OnEnable()
        {
            var canvas = GetComponent<Canvas>();
            var screenService = Services.Get<ScreenService>();
            screenService.SetCameraToCanvas(_cameraType == ScreenService.CameraType.MainCamera ? canvas : null);
            canvas.planeDistance = _planeDistance;
            enabled = false;
        }
    }
}