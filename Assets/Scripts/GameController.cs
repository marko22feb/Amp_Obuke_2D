using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Control;
    public GameObject player;
    public int GemAmount;
    private Text gemText;
    public bool NewGame = false;

    private void Awake()
    {
        if (Control == null)
        {
            Control = this;
        }
        else if (Control != this)
        {
            Destroy(this.gameObject);
        }

        gemText = GameObject.Find("GemText").GetComponent<Text>();
        player = GameObject.Find("Player");
        if (!NewGame)
        LoadGame();
    }

    private void Start()
    {
        UpdateGemText();
    }

    public void UpdateGemText()
    {
        gemText.text = "GEMS: " + GemAmount;
    }

    public void SaveGame()
    {
        Save save = new Save();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.123");

        save.GemsAmount = GemAmount;
        save.lastPlayedScene = SceneManager.GetActiveScene().buildIndex;
        save.CurrentHealth = player.GetComponent<StatComponent>().currentHP;

        save.playerPositionX = player.transform.position.x;
        save.playerPositionY = player.transform.position.y + .5f;
        save.playerPositionZ = player.transform.position.z;

        bf.Serialize(file, save);
        file.Close();
    }

    public bool CanLoadGame()
    {
        bool CanLoad = false;
        if (File.Exists(Application.persistentDataPath + "/gamesave.123")) CanLoad = true;
        return CanLoad;
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.123"))
        {
            Save save = new Save();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.123", FileMode.Open);
            save = (Save)bf.Deserialize(file);

            GemAmount = save.GemsAmount;
            player.GetComponent<StatComponent>().currentHP = save.CurrentHealth;
            player.transform.position = new Vector3(save.playerPositionX, save.playerPositionY, save.playerPositionZ);

            file.Close();
        }
    }

    public int GetLastScene()
    {
        int index = 0;
        if (File.Exists(Application.persistentDataPath + "/gamesave.123"))
        {
            Save save = new Save();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.123", FileMode.Open);
            save = (Save)bf.Deserialize(file);

            index = save.lastPlayedScene;

            file.Close();
        }

        return index;
    }
}

[System.Serializable]
public class Save
{
    public int lastPlayedScene;
    public int GemsAmount;
    public float CurrentHealth;

    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;

    public Save()
    {
        lastPlayedScene = 2;
        GemsAmount = 0;
        CurrentHealth = 100;

        playerPositionX = 0;
        playerPositionY = 0;
        playerPositionZ = 0;
    }
}
