using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class Rotate : MonoBehaviour
{
    private float timeBetweenWhilling;
    private float stoppingTime;
    private float rotating = 0f;
    private float rotatingSpeed;
    private float fps = 50;
    private void Start()
    {
        timeBetweenWhilling = Settings.settings.WheelingTime* fps;
        stoppingTime = Settings.settings.WheelStoppingTime* fps;
       
    }
    
        void FixedUpdate() 
    {  
        rotatingSpeed = Settings.settings.WheelSpeed;
        Quaternion angle = Quaternion.Euler(0, 0, rotating);
        GameObjects.gameObjects.Wheel.transform.rotation =
            Quaternion.Slerp(GameObjects.gameObjects.Wheel.transform.rotation, angle, 0.05f);
        if(stoppingTime != Settings.settings.WheelStoppingTime * fps)
        {   
            stoppingTime++;
            return;
        }
        if(rotating > 360 || rotating < -360)
        {
            rotating = 0;
        }
        rotating+=rotatingSpeed;
        if (timeBetweenWhilling >= 0)
        {
            timeBetweenWhilling--;
        }
        else
        { 
            timeBetweenWhilling = Settings.settings.WheelingTime* fps;
            stoppingTime = 0;
            Random rd = new Random();
            int[] dir = new int[2] { -1, 1 };
            rotatingSpeed *= dir[rd.Next(0,2)];
            Debug.Log(rd);
        }
    }
}
