using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ManagerJSON : MonoBehaviour {

    public static ManagerJSON instance;

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
        SaveFile data = new SaveFile();
        data.playerName = playerName.text;
        data.score = int.Parse(score.text);
        data.posX = player.transform.position.x;
        data.posY = player.transform.position.y;

        string path = Application.persistentDataPath + "/saveFileJson.dat";
       // FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        StreamWriter sw = new StreamWriter(path);       

        sw.WriteLine(JsonUtility.ToJson(data));

        sw.Close();

        print("File saved at " + path);
    }


    // LOAD PLAYER DATA
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/saveFileJson.dat";

        if (File.Exists(path)) // Check if the file for load exists
        {
            StreamReader sr = new StreamReader(path);

            SaveFile data = new SaveFile();
            data = JsonUtility.FromJson<SaveFile>(sr.ReadLine());

            sr.Close();

            playerName.text = data.playerName;
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
