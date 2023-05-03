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

}
