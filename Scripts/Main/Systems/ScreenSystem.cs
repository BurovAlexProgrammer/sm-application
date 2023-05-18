﻿using smApplication.Scripts.Main.Events;
using smApplication.Scripts.Main.Services;
using UnityEngine.Rendering.Universal;

namespace smApplication.Scripts.Main.Systems
{
    public class ScreenSystem : BaseSystem
    {
        private ScreenService _screenService;
        private SettingsService _settingsService;

        public override void Init()
        {
            base.Init();
            _screenService = Services.Services.Get<ScreenService>();
            _settingsService = Services.Services.Get<SettingsService>();
            _screenService.OnDebugProfilerToggleSwitched += OnDebugProfilerToggleSwitched;
        }

        public override void OnDispose()
        {
            _screenService.OnDebugProfilerToggleSwitched -= OnDebugProfilerToggleSwitched;
            base.OnDispose();
        }

        private void OnDebugProfilerToggleSwitched(bool value)
        {
            new ToggleInternalProfileEvent().Fire();
        }

        public override void AddEventHandlers()
        {
            base.AddEventHandlers();
            AddListener<ToggleInternalProfileEvent>(OnInternalProfileDisplayToggle);
            AddListener<ControlModeChangedEvent>(ControlModeChanged);
        }

        private void ControlModeChanged(BaseEvent baseEvent)
        {
            var modeChangedEvent = baseEvent as ControlModeChangedEvent;
            if (_settingsService.Video.PostProcessDepthOfField)
            {
                _screenService.ActiveProfileVolume<DepthOfField>(!modeChangedEvent.MenuMode);
            }
        }

        public override void RemoveEventHandlers()
        {
            base.RemoveEventHandlers();
            RemoveListener<ToggleInternalProfileEvent>();
        }

        private void OnInternalProfileDisplayToggle(BaseEvent baseEvent)
        {
            _screenService.ToggleDisplayProfiler();
        }

    }
}