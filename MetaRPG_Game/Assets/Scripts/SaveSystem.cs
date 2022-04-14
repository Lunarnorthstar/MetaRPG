using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public struct gameData
{
    public Vector3 playerPos;
}

public class SaveSystem : MonoBehaviour
{
    [Header ("Referenced Objects")]
    public Transform player;

    [Header ("Game Defaults")]
    public Vector3 defaultPlayerPosition;

    const string fileName = "saveState.json";
    string filePath;
    string combinedFilePath;

    gameData gD;

    void Awake()
    {
        filePath = Application.persistentDataPath;

        combinedFilePath = filePath + "/" + fileName;

        gD = new gameData();
    }

    void Start()
    {
        loadGameData();
    }

    void OnApplicationQuit()
    {
        saveData();
    }

    public void saveData()
    {
        gD.playerPos = player.position;

        string savedJsonData = JsonUtility.ToJson(gD);
        File.WriteAllText(combinedFilePath, savedJsonData);
    }

    void loadGameData()
    {
        if (File.Exists(combinedFilePath))
        {
            string loadedJson = File.ReadAllText(combinedFilePath);
            gD = JsonUtility.FromJson<gameData>(loadedJson);
        }
        else
        {
            resetGameData();
        }

        player.position = gD.playerPos;
    }

    void resetGameData()
    {
        player.position = defaultPlayerPosition;
        saveData();
    }
}