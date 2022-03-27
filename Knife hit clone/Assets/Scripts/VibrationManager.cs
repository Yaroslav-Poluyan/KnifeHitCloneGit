using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager Current;
    public void Vibrate()
    {
        Vibration.VibratePop();
    }
    private void Start()
    {
        Vibration.Init();
    }

    private void Awake()
    {
        Current = this;
    }
}
