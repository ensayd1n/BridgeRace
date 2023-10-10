using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public GameData GameData;

    private void Awake()
    {
        SetFirstGameData();
    }

    
    
    
    
    
    
    
    
    
    
    #region Save&Load
    private void SetFirstGameData()
    {
        if (!File.Exists(Application.persistentDataPath + "/GameData.json"))
        {
            GameData.Level = 1;
            GameData.TakenBrick = 0;
            Save();
            Load();
        }
    }
    public void Save()
    {
        string json = JsonUtility.ToJson(GameData,true);
        File.WriteAllText(Application.persistentDataPath+"/GameData.json",json);
    }
    public void Load()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/GameData.json");
        GameData = JsonUtility.FromJson<GameData>(json);
    }
    
    #endregion
}
