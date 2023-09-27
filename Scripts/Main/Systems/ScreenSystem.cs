using sm_application.Events;
using sm_application.Service;
using UnityEngine.Rendering.Universal;

namespace sm_application.Systems
{
    public class ScreenSystem : BaseSystem
    {
        private ScreenService _screenService;
        private SettingsService _settingsService;

        public override void Init()
        {
            base.Init();
            _screenService = Services.Get<ScreenService>();
            _settingsService = Services.Get<SettingsService>();
            _screenService.OnDebugProfilerToggleSwitched += OnDebugProfilerToggleSwitched;
        }

        public override void Dispose()
        {
            _screenService.OnDebugProfilerToggleSwitched -= OnDebugProfilerToggleSwitched;
            base.Dispose();
        }
        
        public override void AddEventHandlers()
        {
            base.AddEventHandlers();
            AddListener<ToggleInternalProfileEvent>(OnInternalProfileDisplayToggle);
            AddListener<ControlModeChangedEvent>(ControlModeChanged);
        }

        public override void RemoveEventHandlers()
        {
            base.RemoveEventHandlers();
            RemoveListener<ToggleInternalProfileEvent>();
            RemoveListener<ControlModeChangedEvent>();
        }

        private void OnDebugProfilerToggleSwitched(bool value)
        {
            new ToggleInternalProfileEvent().Fire();
        }

        private void ControlModeChanged(BaseEvent baseEvent)
        {
            var modeChangedEvent = baseEvent as ControlModeChangedEvent;
            if (_settingsService.Video.PostProcessDepthOfField)
            {
                _screenService.ActiveProfileVolume<DepthOfField>(!modeChangedEvent.MenuMode);
            }
        }
        
        private void OnInternalProfileDisplayToggle(BaseEvent baseEvent)
        {
            _screenService.ToggleDisplayProfiler();
        }
    }
}