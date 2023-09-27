using sm_application.Service;
using UnityEngine;

namespace sm_application
{
    public class CameraHolder : MonoBehaviour
    {
        private ScreenService _screenService;

        private void Awake()
        {
            _screenService = Services.Get<ScreenService>();
        }

        private void OnDestroy()
        {
            ReturnCameraToService();
        }

        public void SetCamera()
        {
            _screenService.SetCameraPlace(transform);
        }

        public void ReturnCameraToService()
        {
            _screenService.ReturnCameraToService();
        }
    }
}