using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    private static Dictionary<string, object> dict = new Dictionary<string, object>();

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    public static void LoadData()
    {
        if(File.Exists(Application.persistentDataPath + "/GameData.json"))
        {
            string jsonFile = File.ReadAllText(Application.persistentDataPath + "/GameData.json");
            dict = JsonUtility.FromJson<Dictionary<string, object>>(jsonFile);
        } else
        {
            Debug.Log("No data to load... new game");
        }
    }

    public static void SaveData()
    {
        string jsonFile = JsonUtility.ToJson(dict);
        File.WriteAllText(Application.persistentDataPath + "/GameData.json", jsonFile);
    }

    public static void AddValue(string key, object value)
    {
        dict[key] = value;
    }

    public static void RemoveValue(string key)
    {
        dict.Remove(key);
    }

    public static T GetValue<T>(string key)
    {
        return (T)dict[key];
    }

    public static bool CheckValueExist(string key)
    {
        return dict.ContainsKey(key);
    }


}
