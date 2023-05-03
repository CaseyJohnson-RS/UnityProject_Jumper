using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField]
    private float speed = 0f;

    float Speed
    {
        get { return speed; }
        set
        {
            if (value < 0f)
            {
                Debug.LogWarning("Скорость платформы может быть только положительной!");
                return;
            }
            else
            {
                speed = value;
            }
        }
    }

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    private void Awake()
    {

        _rb = GetComponent<Rigidbody2D>();
        speed = 0f;
    }

    private void Update()
    {
        if(speed > 0f)
        {
            _rb.MovePosition(transform.position + Vector3.down * speed);
        }
    }

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


}
