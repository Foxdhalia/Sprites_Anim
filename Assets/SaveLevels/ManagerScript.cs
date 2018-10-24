using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour
{
    public static ManagerScript instance;

    public InputField playerName, score;
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
        string path = Application.persistentDataPath + "/saveFile.dat";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);

        SaveFile data = new SaveFile();

        ////////////////////////////////////////////////////////////////////////////////////////
        // DEIXAR ESTE TRECHO COMENTADO CASO OS ELEMENTOS DA CLASSE SaveFile() ESTEJAM PRIVADOS!
        data.playerName = playerName.text;
        data.score = int.Parse(score.text);
        data.posX = player.transform.position.x;
        data.posY = player.transform.position.y;
        ///////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////
        // DEIXAR ESTE TRECHO COMENTADO CASO OS ELEMENTOS DA CLASSE SaveFile() ESTEJAM PÚBLICOS!
        //data.SetGameData(playerName.text, int.Parse(score.text), player.transform.position.x, player.transform.position.y);
        ////////////////////////////////////////////////////////////////////////////////////////

        bf.Serialize(file, data);
        file.Close();

        print("File saved! " + Application.persistentDataPath);
    }


    // LOAD PLAYER DATA
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/saveFile.dat";

        if (File.Exists(path)) // Check if the file for load exists
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            SaveFile data = (SaveFile)bf.Deserialize(file); // Take the information contained in saveFile.dat and put at file (of FileStream).
            file.Close();

            ////////////////////////////////////////////////////////////////////////////////////////
            // DEIXAR ESTE TRECHO COMENTADO CASO OS ELEMENTOS DA CLASSE SaveFile() ESTEJAM PRIVADOS!
             playerName.text = data.playerName;
             score.text = data.score.ToString();
             playerPos = new Vector3(data.posX, data.posY, 0f);
             ///////////////////////////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////////////////////////////////
            // DEIXAR ESTE TRECHO COMENTADO CASO OS ELEMENTOS DA CLASSE SaveFile() ESTEJAM PÚBLICOS!
            /*playerName.text = data.GetPlayerName();
            score.text = data.GetScore().ToString();
            playerPos = data.GetPlayerPos();*/
            ////////////////////////////////////////////////////////////////////////////////////////
                        
            player.transform.position = playerPos;
            
            print("File loaded. " + Application.persistentDataPath);
        }

        // If the file does not exist
        else
        {
            print("File does not exist " + Application.persistentDataPath);
        }
    }
}

[Serializable]
public class SaveFile
{
    // CÓDIGO MAIS SIMPLES:
    public string playerName;
    public int score;
    public float posX;
    public float posY;


    // CÓDIGO MAIS PROTEGIDO:
    /*private string playerName;
    private int score;
    private float posX;
    private float posY;

    public void SetGameData(string v_playerName, int v_score, float v_posX, float v_posY)
    {
        playerName = v_playerName;
        score = v_score;
        posX = v_posX;
        posY = v_posY;
    }


    [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]  
    public string GetPlayerName()
    {
        return playerName;
    }
    public int GetScore()
    {
        return score;
    }
    public Vector3 GetPlayerPos()
    {
        return new Vector3(posX, posY, 0f);
    }*/
}