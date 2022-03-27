using System;
using System.Collections;
using System.Collections.Generic;
using Circle;
using Knife;
using UIAndOther;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Current;
    public int currentLvL = 0;
    public int currentNeededKnifeCount = 0;
    public int currentKnife = 0;
    public bool isAnimationsOfLogDestructionPlaying = false;
    public float currentScore;
    public void LoadNewLevel()
    {
        currentScore += (float)(currentKnife * (1 + 0.01 * currentLvL));
        CircleController.Current.DeletePreviousCircle();
        CircleController.Current.SpawnNewCircle();
        isAnimationsOfLogDestructionPlaying = false;
        CircleController.Current.GenerateNewSpinParams();
        if (KnifeSpawner.Current.currentKnife == null) KnifeSpawner.Current.SpawnKnife();
        ObjectsOnCircleSpawner.Current.SpawnRandomUnits();
        SkyBoxManager.Current.SetNewBackGroundColors();
        currentLvL++;
        currentKnife = 0;
        currentNeededKnifeCount = (int) (Random.Range(3, 7) * (1f + 0.01f * currentLvL));
        KnifesCountVisualisator.Current.SetCurrentNeededCount(currentNeededKnifeCount - currentKnife);
    }

    public void StartGame()
    {
        currentLvL = 0;
        CanvasesManager.Current.CloseHomeMenu();
        LoadNewLevel();
    }

    public void NewKnifeStuck()
    {
        currentKnife++;
        KnifesCountVisualisator.Current.SetCurrentNeededCount(currentNeededKnifeCount - currentKnife);
        if (currentKnife >= currentNeededKnifeCount)
        {
             isAnimationsOfLogDestructionPlaying = true;
             StartCoroutine(LevelPassed());
             StopCoroutine(LevelPassed());
        }
    }

    public void LooseGame()
    {
        if(CanvasesManager.Current == null) Debug.Log("canvasesManager is null!");
        if(SaveManager.Current == null) Debug.Log("SaveManager is null!");
        CanvasesManager.Current.OpenLooseMenu();
        SaveManager.Current.CheckForScore(currentScore);
        currentLvL = 0;
        currentScore = 0f;
    }

    private IEnumerator LevelPassed()
    {
        VibrationManager.Current.Vibrate();
        CircleController.Current.PlayWinAnimations();
        yield return new WaitForSeconds(1.3f);
        LoadNewLevel();
    }

    private void Awake()
    {
        Current = this;
    }
}
