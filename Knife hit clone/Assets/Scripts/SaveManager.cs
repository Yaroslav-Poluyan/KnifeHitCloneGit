using System.IO;
using CI.QuickSave;
using UnityEditor;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Current;
    public bool removeSaves = false;
    public float playerBestScore = 0f;
    public int currentPlayerKnife = 0;
    public int currentMelons = 0;

    public void CheckForScore(float newScore)
    {
         if (newScore > playerBestScore) playerBestScore = newScore;
    }
    public void SavePlayerResults()
    {
        QuickSaveWriter quickSaveWriter = QuickSaveWriter.Create("Player", new QuickSaveSettings()
        {
            SecurityMode = SecurityMode.Aes,
            Password = "pentagon"
        });
        quickSaveWriter.Write("bestScore", playerBestScore);
        quickSaveWriter.Write("currentKnife", currentPlayerKnife);
        quickSaveWriter.Write("melon", currentMelons);
        quickSaveWriter.Commit();
    }

    public void LoadPlayerResults()
    {
        QuickSaveReader quickSaveReader = QuickSaveReader.Create("Player",  new QuickSaveSettings()
        {
            SecurityMode = SecurityMode.Aes,
            Password = "pentagon"
        });
        quickSaveReader.TryRead("bestScore", out playerBestScore);
        quickSaveReader.TryRead("currentKnife", out currentPlayerKnife);
        quickSaveReader.TryRead("melon", out currentMelons);
    }

    public void SaveAll()
    {
        if (Directory.Exists((QuickSaveGlobalSettings.StorageLocation)))
        {
            SavePlayerResults();
        }
    }

    public void LoadAll()
    {
        if (Directory.Exists((QuickSaveGlobalSettings.StorageLocation)))
        {
            if (removeSaves) ClearAllSaves();
            LoadPlayerResults();
        }
    }

    private void OnApplicationQuit()
    {
        if(removeSaves) ClearAllSaves();
        SaveAll();
    }

    private void ClearAllSaves()
    {
        if (Directory.Exists(QuickSaveGlobalSettings.StorageLocation))
            Directory.Delete(QuickSaveGlobalSettings.StorageLocation);
        removeSaves = false;
    }
    private void Awake()
    {
        QuickSaveGlobalSettings.StorageLocation = Application.persistentDataPath;
        Debug.Log("Save path - " + QuickSaveGlobalSettings.StorageLocation);
        Current = this;
        if(Current != null) Debug.Log("Save manager initialised");
        LoadAll();
    }
}
