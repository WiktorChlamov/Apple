using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIObjects : MonoBehaviour
{
    [SerializeField] Transform knifesUI, scoreUI, knifesPrefubUI, hitCount,endMenu,gameUI,stageUI,endMenuStage, endMenuHits;
    public static UIObjects objectsUI;
    public Transform KnifesUI { get => knifesUI; set => knifesUI = value; }
    public Transform ScoreUI { get => scoreUI; set => scoreUI = value; }
    public Transform KnifesPrefubUI { get => knifesPrefubUI; set => knifesPrefubUI = value; }
    public Transform HitCount { get => hitCount; set => hitCount = value; }
    public Transform EndMenu { get => endMenu; set => endMenu = value; }
    public Transform GameUI { get => gameUI; set => gameUI = value; }
    public Transform StageUI { get => stageUI; set => stageUI = value; }
    public Transform EndMenuStage { get => endMenuStage; set => endMenuStage = value; }
    public Transform EndMenuHits { get => endMenuHits; set => endMenuHits = value; }

    private void Awake()
    {
        objectsUI = this;
    }

}
