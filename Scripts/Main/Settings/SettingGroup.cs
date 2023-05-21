using System;
using System.IO;
using smApplication.Scripts.Extension;
using sm_application.Scripts.Main.Service;
using sm_application.Scripts.Main.Wrappers;
using UnityEngine;

namespace sm_application.Scripts.Main.Settings
{
    public interface ISettingGroup
    {
        void Init(SettingsService settingsService);
        void LoadFromFile();
        void SaveToFile();
        void Cancel();
        void Restore();
        void ApplySettings(SettingsService settingsService);
    }

    [Serializable]
    public class SettingGroup<T> : ISettingGroup where T : SettingsSO
    {
        [SerializeField] private T _default;
        [SerializeField] private T _saved;
        [SerializeField] private T _current;
        
        public T CurrentSettings => _current;

        private static string StoredFolder; //TODO Move to File Service
        private string _storedFilePath;

        public void Init(SettingsService settingsService)
        {
            LoadFromFile();
            ApplySettings(settingsService);
        }

        public void ApplySettings(SettingsService settingsService)
        {
            _saved.ApplySettings(settingsService);
        }

        public void LoadFromFile()
        {
            StoredFolder = Application.dataPath + "/StoredData/";
            _storedFilePath = StoredFolder + $"Stored_{typeof(T).Name}.data";

            Directory.CreateDirectory(StoredFolder);
            _saved = ScriptableObject.CreateInstance<T>();

            if (!File.Exists(_storedFilePath))
            {
                CreateDefaultSettingFile();
                Log.Warn($"Stored file '{_storedFilePath}' not found. Default settings using instead.");
                SaveDefaults();
                _saved.CopyDataFrom(_default);
            }
            else
            {
                var json = File.ReadAllText(_storedFilePath);
                var storedData = Serializer.ParseScriptableObject<T>(json);
                if (storedData == null)
                {
                    Log.Warn($"Stored file '{_storedFilePath}' is corrupted. Default settings saved instead.");
                    SaveDefaults();
                }
                _saved.CopyDataFrom(storedData ?? _default);
            }

            _current ??= ScriptableObject.CreateInstance<T>();
            _current.CopyDataFrom(_saved);
        }

        public void SaveToFile()
        {
            var data = Serializer.ToJson(_current);
            File.WriteAllText(_storedFilePath, data);
        }

        public void SaveDefaults()
        {
            var data = Serializer.ToJson(_default);
            File.WriteAllText(_storedFilePath, data);
        }

        public void Cancel()
        {
            _current.CopyDataFrom(_saved);
        }

        public void Restore()
        {
            _current.CopyDataFrom(_default);
            _saved.CopyDataFrom(_default);
        }

        private void CreateDefaultSettingFile()
        {
            var data = Serializer.ToJson(_default);
            File.WriteAllText(_storedFilePath, data);
        }
    }
}