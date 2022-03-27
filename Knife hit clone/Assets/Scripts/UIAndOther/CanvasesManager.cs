using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIAndOther.UiAnimations;
using UnityEngine;

namespace UIAndOther
{
    public class CanvasesManager : MonoBehaviour
    {
        public static CanvasesManager Current;
        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject looseMenu;
        [SerializeField] private GameObject inGameUI;
        [SerializeField] private TextMeshProUGUI maxScore;
        [SerializeField] private TextMeshProUGUI currentStage;
        [SerializeField] private TextMeshProUGUI melons;

        public bool IsAnyWindowOpen()
        {
            if (startMenu.activeSelf || looseMenu.activeSelf) return true;
            return false;
        }

        public void OpenHomeMenu()
        {
            CloseInGameUI();
            if(startMenu.activeSelf) return;
            looseMenu.SetActive(false);
            startMenu.SetActive(true);
        }

        public void CloseHomeMenu()
        {
            startMenu.SetActive(false);
            OpenInGameUI();
        }

        public void OpenLooseMenu()
        {
            CloseInGameUI();
            if(looseMenu.activeSelf) return;
            looseMenu.SetActive(true);
            if (currentStage != null) currentStage.text = "Stage: " + LevelManager.Current.currentLvL;
            if (maxScore != null) maxScore.text = "Max Score: " + SaveManager.Current.playerBestScore + "\n" + "Score: " + LevelManager.Current.currentScore;
            if (melons != null) melons.text = "Melons: " + SaveManager.Current.currentMelons;
        }

        public void OpenInGameUI()
        {
            inGameUI.SetActive(true);
        }

        public void CloseInGameUI()
        {
            inGameUI.SetActive(false);
        }
        public void Restart()
        {
            looseMenu.SetActive(false);
            LevelManager.Current.LoadNewLevel();
            OpenInGameUI();
        }
        private void Awake()
        {
            Current = this;
        }
    }
}
