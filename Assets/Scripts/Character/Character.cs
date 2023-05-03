using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField, Range(1f,25f)]
    private float maxJumpForce = 10f;

    [SerializeField]
    private LayerCheckComponent layerChecker;
    [SerializeField]
    private LineRenderer lineRenderer;

    public UnityEvent OnDie;

    private Rigidbody2D _rb;
    private bool isGrounded = false;

    private bool alive = true;
    private bool isMovable = false;

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateIsMovable();
    }

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    void UpdateIsMovable()
    {
        CheckGrounded();

        isMovable = alive && isGrounded && _rb.velocity.magnitude <= 0.15f;
    }

    private void CheckGrounded()
    {
        if(alive)
            
            isGrounded = layerChecker.isTouched;
    }

    public void Jump(Vector2 direction)
    {
        if(isMovable)

            _rb.AddForce(direction * maxJumpForce, ForceMode2D.Impulse);
    }

    public void Die()
    {
        alive = false;

        _rb.simulated = false;

        OnDie?.Invoke();
    }

    public void StopStratchingTrajectory()
    {
        lineRenderer.enabled = false; //Скрываем траекторию
    }

    public void StratchTrajectory(Vector2 direction)
    {

        if(isMovable)
        {
            lineRenderer.enabled = true; // Показываем траекторию

            Vector3[] list = SimulateArch(
                transform.position,
                direction.normalized,
                direction.magnitude * maxJumpForce,
                _rb.mass);

            lineRenderer.positionCount = list.Length;

            for ( int i = 0; i < list.Length;++i )
            {
                lineRenderer.SetPosition(i, list[i]);
            }
            
        }

    }

    private Vector3[] SimulateArch(Vector2 launchPosition, Vector2 dir, float _force, float _mass, float timeStepInterval = 0.05f, float duration = 4f)
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
        RaycastHit2D hit = Physics2D.Raycast(start, end - start, 0.75f, LayerMask.GetMask("Ground"));

        return (hit.collider != null);
    }

}
