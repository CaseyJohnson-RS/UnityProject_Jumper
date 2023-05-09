using UnityEngine;

public class GameStateMachineInstaller : MonoBehaviour
{
    public MonoBehaviour[] gameEventListeners;

    void Awake()
    {
        foreach (object gameListener in gameEventListeners)
        {
            GameStateMachine.AddListener(gameListener);
        }
    }

    [ContextMenu("Play")]
    public void Play()
    {
        GameStateMachine.Play();
    }

    [ContextMenu("Finish")]
    public void Finish()
    {
        GameStateMachine.Finish();
    }

    [ContextMenu("Pause")]
    public void Pause()
    {
        GameStateMachine.Pause();
    }

    [ContextMenu("Resume")]
    public void Resume()
    {
        GameStateMachine.Resume();
    }

    [ContextMenu("GetInFreeState")]
    public void GetInFreeState()
    {
        GameStateMachine.GetInFreeState();
    }

}
