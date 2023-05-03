using UnityEngine;

public class MovingController : MonoBehaviour
{
    [HideInInspector]
    public Character _chr;
    public float maxAmplitudeX = 0.5f;
    public float maxAmplitudeY = 0.5f;

    public void Swipe(Vector2 vec)
    {

        _chr?.Jump(new Vector2(
            vec.x <= maxAmplitudeX ? vec.x : maxAmplitudeX,
            vec.y <= maxAmplitudeY ? vec.y : maxAmplitudeY)
        );

    }

    public void Stretching(Vector2 vec)
    {
        _chr?.StratchTrajectory(new Vector2(
            vec.x <= maxAmplitudeX ? vec.x : maxAmplitudeX,
            vec.y <= maxAmplitudeY ? vec.y : maxAmplitudeY)
        );
    }

    public void OnFingerUp()
    {
        _chr?.StopStratching();
    }
}
