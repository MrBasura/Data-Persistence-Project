using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DataStorage : MonoBehaviour
{
    public static DataStorage Instance;
    public string LastScore;
    public string playerName;
    public GameObject inputField;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }

   public void StartTheGameButton()
    {
        SceneManager.LoadScene(1);
    }

    public void StoreName()
    {
        playerName = inputField.GetComponent<TextMeshProUGUI>().text;
    }

    [System.Serializable]
    class SaveData
    {
        public string LastScore;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.LastScore = LastScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            LastScore = data.LastScore;
        }
    }


}
