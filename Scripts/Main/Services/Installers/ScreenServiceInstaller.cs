﻿using Tayx.Graphy;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace smApplication.Scripts.Main.Services
{
    public class ScreenServiceInstaller : BaseServiceInstaller
    {
        public Camera CameraMain;
        public Volume Volume;
        public GraphyManager InternalProfilerManager;
        public GameObject InternalProfilerPanels;
        public Toggle InternalProfilerToggle;
        public Image CameraTopFrame;
        public bool ShowProfilerOnStartup;
        public Transform CameraHolder;
    }
}