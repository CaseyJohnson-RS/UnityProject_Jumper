using UnityEngine;

public class DataManagerUpdater : MonoBehaviour, IGameFinishListener
{

    public void OnFinish() => DataManager.SaveData();

    public void OnApplicationQuit() => DataManager.SaveData();

}
