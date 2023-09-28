using System.IO;
using sm_application.Service;
using UnityEngine;

namespace sm_application.Service
{
    public class DebugService : IServiceWithInstaller
    {
        private DebugServiceConfig _serviceConfig;
        private Transform _gizmosContainer;
        
        public bool IsSaveLogToFile => _serviceConfig.SaveLogToFile;
        
        public void Construct(IServiceInstaller installer)
        {
            var debugInstaller = installer as DebugServiceInstaller;
            _serviceConfig = debugInstaller.Config;
            _gizmosContainer = debugInstaller.transform;
            
            if (_serviceConfig.SaveLogToFile)
            {
                Application.logMessageReceived += LogToFile;
            }
        }
        
        private void LogToFile(string condition, string stacktrace, LogType type)
        {
            var path = Application.persistentDataPath + "/log.txt";
            using var streamWriter = File.AppendText(path);
            streamWriter.WriteLine($"{condition}");
            streamWriter.WriteLine("----");
            streamWriter.WriteLine($"{stacktrace}");
            streamWriter.WriteLine("-----------------------------------------------------------------------------------------");
        }

        public void Construct()
        {
            throw new System.NotImplementedException();
        }
    }
}