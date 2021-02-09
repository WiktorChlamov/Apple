using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Levels
{
   public static void Stages(int stage)
    {
        switch(stage)
        {
            case int _ when stage < 2:
                Settings.Knifes = 4;
                Settings.settings.WheelSpeed += 0.4f;
                break;
            case int _ when stage < 4:
                Settings.Knifes = 6;
                Settings.settings.WheelSpeed += 0.4f;
                break;
            case int _ when stage < 6:
                Settings.Knifes = 8;
                Settings.settings.WheelSpeed += 0.4f;
                break;
            case int _ when stage < 8:
                Settings.Knifes = 10;
                break;
            case int _ when stage < 10:
                Settings.Knifes = 4;
                Settings.settings.WheelSpeed += 0.2f;
                break;
            case int _ when stage < 12:
                Settings.Knifes = 6;
                Settings.settings.WheelSpeed += 0.2f;
                break;
            case int _ when stage < 14:
                Settings.Knifes = 8;
                Settings.settings.WheelSpeed += 0.2f;
                break;
            case int _ when stage < 16:
                Settings.Knifes = 10;
                Settings.settings.WheelSpeed += 0.2f;
                break;
            default:
                Settings.Knifes = 12;
                Settings.settings.WheelSpeed += 1f;
                break;
        }
    }
}
