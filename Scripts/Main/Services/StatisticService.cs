﻿using System;
using System.IO;
using smApplication.Scripts.Main.DTO;
using smApplication.Scripts.Main.Services;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using static smApplication.Scripts.Main.DTO.StatisticData;

namespace smApplication.Scripts.Main.Services
{
    public class StatisticService : IService, IConstruct
    {
        public Action<RecordName, string> RecordChanged; 
        
        private StatisticData _statisticData;
        private string _storedFolder;
        private string _storedFolderPath;

        public void Construct()
        {
            _statisticData = new StatisticData();
            _storedFolder ??= Application.dataPath + "/StoredData/";
            _storedFolderPath = _storedFolder + "Statistic.data";
            LoadFromFile();
            TimerExecuting();
        }

        ~StatisticService()
        {
            SaveToFile();
        }

        public string GetRecord(RecordName recordName)
        {
            return _statisticData.CommonRecords[recordName];
        }

        public void AddValueToRecord(RecordName recordName, int value)
        {
            if (value == 0) return;

            var commonRecordValue = int.Parse(_statisticData.CommonRecords[recordName]);
            _statisticData.CommonRecords[recordName] = (commonRecordValue + value).ToString();
            var sessionRecordValue = int.Parse(_statisticData.SessionRecords[recordName]);
            _statisticData.SessionRecords[recordName] = (sessionRecordValue + value).ToString();
            RecordChanged?.Invoke(recordName, _statisticData.SessionRecords[recordName]);
        }

        public void AddValueToRecord(RecordName recordName, float value)
        {
            if (value == 0f) return;

            var commonRecordValue = float.Parse(_statisticData.CommonRecords[recordName]);
            _statisticData.CommonRecords[recordName] = (commonRecordValue + value).ToString();
            var sessionRecordValue = float.Parse(_statisticData.SessionRecords[recordName]);
            _statisticData.SessionRecords[recordName] = (sessionRecordValue + value).ToString();
            RecordChanged?.Invoke(recordName, _statisticData.SessionRecords[recordName]);
        }

        public float GetFloatValue(RecordName recordName, FormatType formatType = FormatType.Common)
        {
            return formatType switch
            {
                FormatType.Common => float.Parse(_statisticData.CommonRecords[recordName]),
                FormatType.Session => float.Parse(_statisticData.SessionRecords[recordName]),
                _ => throw new ArgumentOutOfRangeException(nameof(formatType), formatType, null)
            };
        }
        
        public int GetIntegerValue(RecordName recordName, FormatType formatType = FormatType.Common)
        {
            return formatType switch
            {
                FormatType.Common => int.Parse(_statisticData.CommonRecords[recordName]),
                FormatType.Session => int.Parse(_statisticData.SessionRecords[recordName]),
                _ => throw new ArgumentOutOfRangeException(nameof(formatType), formatType, null)
            };
        }

        public void ResetSessionRecords()
        {
            _statisticData.ResetSessionData();
            
            foreach (var pair in _statisticData.SessionRecords)
            {
                RecordChanged?.Invoke(pair.Key, pair.Value);
            }
        }

        public void SaveToFile()
        {
            var data = JsonConvert.SerializeObject(_statisticData);
            File.WriteAllText(_storedFolderPath, data);
        }
        
        public void CalculateSessionDuration()
        {
            var sessionDuration = GetFloatValue(RecordName.LastGameSessionDuration, FormatType.Session);
            var longestSession = GetFloatValue(RecordName.LongestGameSessionDuration);
            var averageSession = GetFloatValue(RecordName.AverageGameSessionDuration);
            longestSession = Mathf.Max(longestSession, sessionDuration);
            averageSession = averageSession == 0 ? sessionDuration : (averageSession * 3 + sessionDuration) / 4f;
            SetRecord(RecordName.LongestGameSessionDuration, longestSession.ToString());
            SetRecord(RecordName.AverageGameSessionDuration, averageSession.ToString());
        }

        public void SetScores(int value)
        {
            SetRecord(RecordName.Scores, value.ToString());
            var maxScores = GetIntegerValue(RecordName.MaxScores);
            maxScores = Mathf.Max(maxScores, maxScores);
            SetRecord(RecordName.MaxScores, maxScores.ToString());
        }

        public void EndGameDataSaving()
        {
            CalculateSessionDuration();
            SaveToFile();
        }
        
        private void SetRecord(RecordName recordName, string value)
        {
            _statisticData.CommonRecords[recordName] = value;
            _statisticData.SessionRecords[recordName] = value;
            RecordChanged?.Invoke(recordName, _statisticData.SessionRecords[recordName]);
        }

        private async void TimerExecuting()
        {
            var delta = 0f;

            while (this != null)
            {
                await UniTask.NextFrame();
                delta += Time.deltaTime;

                if (delta < 1f) continue;

                // if (_gameManager.ActiveStateEquals<GameStates.PlayNewGame>())
                {
                    AddValueToRecord(RecordName.LastGameSessionDuration, delta);
                }

                delta = 0f;
            }
        }

        private void LoadFromFile()
        {
            Directory.CreateDirectory(_storedFolder);

            if (!File.Exists(_storedFolderPath))
            {
                Debug.LogWarning($"Stored file '{_storedFolderPath}' not found. Created empty statistic file.");
                var emptyRecords = JsonConvert.SerializeObject(_statisticData);
                File.WriteAllText(_storedFolderPath, emptyRecords);
            }

            var json = File.ReadAllText(_storedFolderPath);
            _statisticData = JsonConvert.DeserializeObject<StatisticData>(json);
        }
    }
}