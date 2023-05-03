using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField, Range(1f,25f)]
    private float maxJumpForce = 10f;

    [SerializeField]
    private LayerCheckComponent layerChecker;

    private Rigidbody2D _rb;
    private bool isGrounded = false;

    private bool alive = true;

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateIsGrounded();
    }

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    private void UpdateIsGrounded()
    {
        
        if(alive)
            
            isGrounded = layerChecker.isTouched;

    }

    public void Jump(Vector2 direction)
    {

        if(alive && isGrounded && _rb.velocity.magnitude <= 0.15f)

            _rb.AddForce(direction * maxJumpForce, ForceMode2D.Impulse);

    }

    public void Die()
    {

        alive = false;
        _rb.simulated = false;

    }

}
