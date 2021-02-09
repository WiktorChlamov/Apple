using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Settings;
using static GameObjects;
using UnityEngine.EventSystems;

public class Fire : MonoBehaviour
{
    private float timer;
    private bool canFired = true;
    public static Fire fire;
    private GameObject knifeToFire;
    private void Awake()
    {
        fire = this;
    }
    private void Start()
    {
        InstKnife();
    }
    private void InstKnife()
    {
        knifeToFire = Instantiate(gameObjects.KnifePB, gameObjects.KnifeSpawnPoint);
        gameObjects.KnifesInWood.Add(knifeToFire.transform);
        knifeToFire.GetComponent<Rigidbody2D>().rotation = 180;
        knifeToFire.GetComponent<Knife>().Fly = false;

    }
    public void FireOn()
    {
        if (canFired && Knifes>0)
        {
            knifeToFire.GetComponent<Knife>().Fly = true;
            knifeToFire.GetComponent<PolygonCollider2D>().enabled = true;
            canFired = false;
            timer = 0;
            UiKnivesAndScoreEvents.uiKnivesAndScore.onFireEvent.Invoke();
            Knifes--;
            if (Knifes > 0)
            {
                InstKnife();
            }
        }
    }
    public void FixedUpdate()
    {
        if (!canFired && timer <settings.FireRate)
        {
            timer += 0.01f;
            return;
        }
        if(timer >= settings.FireRate && Knifes>0)
        {
            
            canFired = true;
        }
    }
}
