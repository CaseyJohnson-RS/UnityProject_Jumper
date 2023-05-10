using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField, Range(1f,25f)]
    private float maxJumpForce = 10f;

    [SerializeField]
    private LayerCheckComponent groundChecker;
    [SerializeField]
    private LineRenderer lineRenderer;

    public UnityEvent OnDie;
    public UnityEvent OnRevive;

    private Rigidbody2D _rb;
    private bool isGrounded = false;
    public bool IsGrounded {  get { return isGrounded; } }

    private bool alive = true;
    private bool isMovable = false;

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckGrounded();
        UpdateIsMovable();
    }

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    private void UpdateIsMovable()
    {   
        isMovable = alive && isGrounded && Mathf.Abs(_rb.velocity.x) <= 0.15f;
    }

    private void CheckGrounded()
    {
        isGrounded = groundChecker.isTouched;
    }

    public void Jump(Vector2 direction)
    {
        if(isMovable)

            _rb.AddForce(direction * maxJumpForce, ForceMode2D.Impulse);
    }

    public void Die()
    {
        if (!alive) return;

        alive = false;

        _rb.simulated = false;

        OnDie?.Invoke();
    }

    public void Revive(Vector3 pos)
    {
        if (alive) return;

        alive = true;

        transform.position = pos;

        _rb.simulated = true;

        _rb.velocity = Vector3.zero;

        OnRevive?.Invoke();

        
    }

    public void StopStratchingTrajectory()
    {
        lineRenderer.enabled = false; //Скрываем траекторию
    }

    public void StratchTrajectory(Vector2 direction)
    {
        if (isMovable)
        {
            lineRenderer.enabled = true; // Показываем траекторию
            
            Vector3[] list = SimulateArch(
                transform.position,
                direction.normalized,
                direction.magnitude * maxJumpForce + _rb.velocity.y*_rb.mass,
                _rb.mass);

            lineRenderer.positionCount = list.Length;

            for ( int i = 0; i < list.Length;++i )
            {
                lineRenderer.SetPosition(i, list[i]);
            }
            
        }

    }

    private Vector3[] SimulateArch(Vector2 launchPosition, Vector2 dir, float _force, float _mass, float timeStepInterval = 0.05f, float duration = 1f)
    {
        int n = (int)(duration / timeStepInterval);

        List<Vector3> list = new List<Vector3>();

        float velocity = _force / _mass;
        
        for(int i = 0; i < n; i++)
        {
            Vector2 calculatedPosition = launchPosition + dir * i * velocity * timeStepInterval;

            calculatedPosition.y += Physics2D.gravity.y / 2 * Mathf.Pow(i * timeStepInterval, 2);

            list.Add(calculatedPosition);

            if (i > 0 && CheckForCollision(list[i-1], calculatedPosition))
            {
                break;
            }

        }

        return list.ToArray();
        
    }

    private bool CheckForCollision(Vector2 start, Vector2 end)
    {
        RaycastHit2D hit = Physics2D.Raycast(start, end - start, 0.1f, LayerMask.GetMask("Ground"));

        return (hit.collider != null);
    }

}
