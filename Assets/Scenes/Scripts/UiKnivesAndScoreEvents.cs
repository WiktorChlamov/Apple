using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using static Settings;
using UnityEngine.SceneManagement;
using System;

public class UiKnivesAndScoreEvents : MonoBehaviour
{
    public static UiKnivesAndScoreEvents uiKnivesAndScore;
    private Transform[] allKnifes;

    [HideInInspector]
    public UnityEvent appleHitEvent = new UnityEvent(),
        onFireEvent = new UnityEvent(),
        onWheelHitEvent = new UnityEvent(),
        OnKnifeHit = new UnityEvent();

    public int Score { get; set; }
    public int HitCount { get; set; }

    private void Awake()
    {
        uiKnivesAndScore = this;
        appleHitEvent.AddListener(AppleHitting);
        onFireEvent.AddListener(Shot);
        onWheelHitEvent.AddListener(WheelHit);
        OnKnifeHit.AddListener(KnifeHit);
    }
    private void Start()
    {
        allKnifes = new Transform[Knifes];
        for (int i = 0; i < allKnifes.Length; i++)
        {
            allKnifes[i] = Instantiate(UIObjects.objectsUI.KnifesPrefubUI, UIObjects.objectsUI.KnifesUI);
        }
    }
    private void Shot()
    {
        PlaySound(GameObjects.gameObjects.KnifeThrow);
        if (Knifes > 0)
        {
            Color color = new Color { a = 0.4f };
            allKnifes[allKnifes.Length - Knifes].GetComponent<Image>().color = color;
        }
    }
    private void AppleHitting()
    {
        PlaySound(GameObjects.gameObjects.AppleHitting);
        Vibration.VibratePop();
        Score +=10;
        UIObjects.objectsUI.ScoreUI.GetComponent<TextMeshProUGUI>().SetText("Score: " + Score);
        foreach (Transform piece in GameObjects.gameObjects.Apple.GetComponent<FruitPB>().Pieces)
        {
            Vector2 vector2 = piece.position - GameObjects.gameObjects.ExpPoint.position;

            piece.gameObject.AddComponent<Rigidbody2D>();
            piece.GetComponent<Rigidbody2D>().AddForce(vector2 * 50);
        }
    }
    private void WheelHit()
    {
        Vibration.Vibrate(200);
        PlaySound(GameObjects.gameObjects.WheelHitting);
        HitCount++;
        UIObjects.objectsUI.HitCount.GetComponent<TextMeshProUGUI>().SetText("Hits: " + (Data.CurrentHits+HitCount));
        if (HitCount == allKnifes.Length)
        {
            NextLevel();
        }
        GameObjects.gameObjects.Particle.GetComponent<ParticleSystem>().Play();
    }
    private void KnifeHit()
    {
        Vibration.Vibrate(200);
        PlaySound(GameObjects.gameObjects.KnifeHitting);
        Data.CurrentHits += HitCount;
        Data.Save();
        HitCount = 0;
        StartCoroutine(Delay(() => TheEnd()));
    }
    private void NextLevel()
    {   
        Data.ContinueGame = true;
        Data.CurrentHits += HitCount;
        Data.CurrentScore = Score;
        //Data.Save();
        Data.CurrentStage++;
        StartCoroutine(Delay(() => RestartLevel(true)));
        CrackWoodAndKnifes();
    }
    public void CrackWoodAndKnifes()
    {
        foreach (Transform piece in GameObjects.gameObjects.WoodPieces)
        {   
            settings.WheelSpeed = 0;
            Rigidbody rb = piece.GetComponent<Rigidbody>();
            rb.AddExplosionForce(500, GameObjects.gameObjects.ExpPoint.position, 200);
            rb.useGravity = true;
        }
        foreach (Transform piece in GameObjects.gameObjects.KnifesInWood)
        {
            piece.GetComponent<PolygonCollider2D>().enabled = false;
            piece.SetParent(null);
            Vector2 vector2 = piece.position - GameObjects.gameObjects.ExpPoint.position;
            Rigidbody2D rb = piece.GetComponent<Rigidbody2D>();
            rb.AddForce(vector2*50);
            rb.gravityScale = 1;
        }
        if (GameObjects.gameObjects.Apple != null)
        {
            GameObject apple = GameObjects.gameObjects.Apple;
            apple.transform.SetParent(null);
            apple.GetComponent<Rigidbody2D>().gravityScale = 1;
            apple.GetComponent<CircleCollider2D>().enabled = false;
        }

    }

    public IEnumerator Delay(Action action)
    {
        yield return new WaitForSeconds(2f);
        action();
    }
    private void TheEnd()
    {
        UIObjects.objectsUI.EndMenu.gameObject.SetActive(true);
        UIObjects.objectsUI.EndMenuStage.GetComponent<TextMeshProUGUI>().
            SetText("Stage: " + Data.CurrentStage + "\n" + "Record: " + Data.StageRecord);
        UIObjects.objectsUI.EndMenuHits.GetComponent<TextMeshProUGUI>().
            SetText("Hits: " + Data.CurrentHits + "\n" + "Record: " + Data.HitRecord);
    }
        public void RestartLevel(bool continueGame = false)
    {   
        if(continueGame)
        {
            Vibration.VibrateNope();
            Data.ContinueGame = continueGame;
        }
        else
        {
            Data.CurrentStage = 1;
            Data.CurrentHits = 0;
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void PlaySound(AudioClip audioClip)
    {
        GameObjects.gameObjects.Sound.clip = audioClip;
        GameObjects.gameObjects.Sound.Play();
    }
    
}
