using UnityEngine;

public class MovingController : MonoBehaviour, IGamePlayListener, IGameFinishListener
{
    private CharacterController charController;
    private Character _ch;

    public float maxAmplitudeX = 0.5f;
    public float maxAmplitudeY = 0.5f;

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    private void Start()
    {
        charController = ServiceProvider.GetService<CharacterController>();
    }

    public void OnPlay()
    {
        _ch = charController.Character.GetComponent<Character>();
    }

    public void OnFinish()
    {
        _ch = null;
    }

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    public void Swipe(Vector2 vec)
    {
        _ch?.Jump(new Vector2(
            vec.x <= maxAmplitudeX ? vec.x : maxAmplitudeX,
            vec.y <= maxAmplitudeY ? vec.y : maxAmplitudeY)
        );

    }

    public void Stretching(Vector2 vec)
    {
        _ch?.StratchTrajectory(new Vector2(
            vec.x <= maxAmplitudeX ? vec.x : maxAmplitudeX,
            vec.y <= maxAmplitudeY ? vec.y : maxAmplitudeY)
        );
    }

    public void OnFingerUp()
    {
        _ch?.StopStratchingTrajectory();
    }
}
