using UnityEngine;

public class Platform : MonoBehaviour
{
    private Rigidbody2D _rb;

    [HideInInspector]
    public PlatformsContoller platformsController;

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdatePosition();
    }

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    private void UpdatePosition()
    {
        if (platformsController)
        {
            _rb.MovePosition(transform.position + Vector3.down * platformsController.Speed);
        }
    }

}
