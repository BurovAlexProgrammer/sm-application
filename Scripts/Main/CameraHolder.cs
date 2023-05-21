using sm_application.Scripts.Main.Service;
using sm_application.Scripts.Main.Service;
using UnityEngine;

namespace sm_application.Scripts.Main
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