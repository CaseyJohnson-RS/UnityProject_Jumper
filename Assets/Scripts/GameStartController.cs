using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartController : MonoBehaviour
{
    public string TutorialSceneName = "Tutorial";
    public string MainSceneName = "MainScene";

    public void Awake() => DataManager.LoadData();

    public void Start()
    {
        if (!DataManager.CheckValueExist("Is it first play?") || DataManager.GetValue<bool>("Is it first play?"))
        {
            StartCoroutine(LoadLevel(TutorialSceneName));
        }
    }

    IEnumerator LoadLevel(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        yield return null;
    }
}
