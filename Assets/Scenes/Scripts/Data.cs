using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class Data: MonoBehaviour
{   [Serializable]
    private struct SavingData
    {
        public int hitRecord,score,stageRecord;
    }
    private static int hitRecord,currentHits,stageRecord,currentStage = 1,scoreRecord,currentScore;
    private static bool continueGame = false,restart = false;
    private static string SavePath => $"{Application.persistentDataPath}/save.txt";
    public static int Score { get => scoreRecord; set => scoreRecord = value; }
    public static int HitRecord { get => hitRecord; set => hitRecord = value; }
    public static bool ContinueGame { get => continueGame; set => continueGame = value; }
    public static int CurrentHits { get => currentHits; set => currentHits = value; }
    public static bool Restart { get => restart; set => restart = value; }
    public static int StageRecord { get => stageRecord; set => stageRecord = value; }
    public static int CurrentStage { get => currentStage; set => currentStage = value; }
    public static int CurrentScore { get => currentScore; set => currentScore = value; }

    public static void Save()
    {
      SaveFile(CaptureData());
    }
    public static void Load()
    {
        var state = LoadFile();
        RestoreState(state);
    }
    private static void RestoreState(SavingData state)
    {   scoreRecord = state.score;
        hitRecord = state.hitRecord;
        stageRecord = state.stageRecord;
    }
        private static SavingData LoadFile()
    {
        if (!File.Exists(SavePath))
        {
            return new SavingData();
        }
        using (FileStream stream = File.Open(SavePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (SavingData)formatter.Deserialize(stream);
        }
    }
    private static SavingData CaptureData()
    {  
        return new SavingData { score = UiKnivesAndScoreEvents.uiKnivesAndScore.Score,
            hitRecord = hitRecord >= currentHits ? hitRecord : currentHits,
        stageRecord =stageRecord>=currentStage ? stageRecord: currentStage };
    }
    private static void SaveFile(object state)
    {
        using (var stream = File.Open(SavePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }
}
