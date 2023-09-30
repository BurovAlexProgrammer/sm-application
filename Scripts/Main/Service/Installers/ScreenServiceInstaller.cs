﻿using Tayx.Graphy;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace sm_application.Service
{
    public class ScreenServiceInstaller : BaseServiceInstaller
    {
        public Camera CameraMain;
        public Volume Volume;
        public GraphyManager InternalProfilerManager;
        public GameObject InternalProfilerPanels;
        public Toggle InternalProfilerToggle;
        public bool ShowProfilerOnStartup;
        public Transform CameraHolder;
        public Image TopFrame;
    }
}