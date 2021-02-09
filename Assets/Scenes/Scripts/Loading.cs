using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Loading : MonoBehaviour
{
    [SerializeField]
    private Transform recordAndScore;
    public static GameObject audioSource;
    private void Awake()
    {
       if (audioSource == null)
        {
            audioSource = Instantiate(GameObjects.gameObjects.Music);
            DontDestroyOnLoad(audioSource);
        };
        Vibration.Init();
        Levels.Stages(Data.CurrentStage);
        gameObject.SetActive(true);
        Data.Load();
    }
    private void Start()
    { 
        if (Data.ContinueGame)
        {
            Data.ContinueGame = false;
            UIObjects.objectsUI.GameUI.gameObject.SetActive(true);
            //UiKnivesAndScoreEvents.uiKnivesAndScore.HitCount += Data.CurrentHits;
            
            gameObject.SetActive(false);
        }
        if(Data.Restart)
        {
            audioSource.GetComponent<AudioSource>().clip = GameObjects.gameObjects.Musics[Random.Range(0, 3)];
            audioSource.GetComponent<AudioSource>().Play();
            UIObjects.objectsUI.GameUI.gameObject.SetActive(true);
            gameObject.SetActive(false);
            Data.Restart = false;
        }
        UiKnivesAndScoreEvents.uiKnivesAndScore.Score = Data.CurrentScore >0 ? Data.CurrentScore : Data.Score;
        UIObjects.objectsUI.HitCount.GetComponent<TextMeshProUGUI>().SetText("Hits: "+ Data.CurrentHits);
        UIObjects.objectsUI.StageUI.GetComponent<TextMeshProUGUI>().SetText("Stage: "+Data.CurrentStage);
        UIObjects.objectsUI.ScoreUI.GetComponent<TextMeshProUGUI>().SetText("Score: "+ UiKnivesAndScoreEvents.uiKnivesAndScore.Score);
        
        TextMeshProUGUI textMeshProUGUI = recordAndScore.GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.SetText("Stage record: " + Data.StageRecord +"\n" + "Hit record: " +
        Data.HitRecord + "\n" + "Score: " + Data.Score);
    }
}
