using UnityEngine;
using UnityEngine.Events;

public class DeadZoneComponent : MonoBehaviour
{
    public UnityEvent onKill;

    private void KillCharacter(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Character"))
        {

            collision.gameObject.GetComponent<Character>().Die();
            onKill?.Invoke();

        }

    }

    private void OnTriggerEnter2D(Collider2D collision) => KillCharacter(collision);

    private void OnTriggerStay2D(Collider2D collision)  => KillCharacter(collision);
}
