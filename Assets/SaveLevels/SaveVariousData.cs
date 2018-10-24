using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveVariousData : MonoBehaviour {

    public static SaveVariousData instance;

    public InputField playerName, score;
    public GameObject panelChoices;
    private Vector3 playerPos;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // SAVE PLAYER DATA
    public void SaveData()
    {       
        SaveMultiplesDataFile data = new SaveMultiplesDataFile();
        string path = Application.persistentDataPath + "/saveMultipleData.dat";

        if (File.Exists(path)) // Check if the file for load exists
        {
            // FIRST: search for user names:
            StreamReader sr = new StreamReader(path);

            string lineFromFile = "";
            bool foundPlayerName = false;
            print(sr.ReadLine());

            while ((lineFromFile = sr.ReadLine()) != null)
            {
                //print(lineFromFile);
                   if (lineFromFile.Contains(playerName.text))
                    {
                        panelChoices.SetActive(true);
                        foundPlayerName = true;
                        print("This user already exist!");
                    }
            }
            sr.Close();

            // If the user name does not exist:
            if (!foundPlayerName)
            {
                StreamWriter sw = new StreamWriter(path, true);
                data.playerName = playerName.text;
                data.score = int.Parse(score.text);
                data.posX = player.transform.position.x;
                data.posY = player.transform.position.y;

                sw.WriteLine(JsonUtility.ToJson(data));

                sw.Close();

                print("File saved at " + path);
            }
        }

        else
        {
            StreamWriter sw = new StreamWriter(path);
            data.playerName = playerName.text;
            data.score = int.Parse(score.text);
            data.posX = player.transform.position.x;
            data.posY = player.transform.position.y;

            sw.WriteLine(JsonUtility.ToJson(data));

            sw.Close();

            print("File created and saved at " + path);
        }
    }


    // LOAD PLAYER DATA
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/saveMultipleData.dat";

        if (File.Exists(path)) // Check if the file for load exists
        {
             StreamReader sr = new StreamReader(path);
            
            string lineFromFile = "";
            bool foundPlayerName = false;
            print(sr.ReadLine());

            while ((lineFromFile = sr.ReadLine()) != null && !foundPlayerName)
            {
                //print(lineFromFile);
                if (lineFromFile.Contains(playerName.text))
                {                   
                    foundPlayerName = true;
                    print("User located!");
                }
            }

            print(lineFromFile);
            SaveMultiplesDataFile data = JsonUtility.FromJson<SaveMultiplesDataFile>(lineFromFile);
            sr.Close();

             //playerName.text = data.playerName;
             score.text = data.score.ToString();
             playerPos = new Vector3(data.posX, data.posY, 0f);
             player.transform.position = playerPos;


             print("File loaded from " + path);
        }

        // If the file does not exist
        else
        {
            print("File does not exist at " + Application.persistentDataPath);
        }
    }

}

[Serializable]
public class SaveMultiplesDataFile
{    
    public string playerName;
    public int score;
    public float posX;
    public float posY;
}