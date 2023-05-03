using UnityEngine;

public class LayerCheckComponent : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    public bool isTouched { get { return touched; } }

    private bool touched = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & layerMask) != 0)
        {
            touched = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (((1 << collision.gameObject.layer) & layerMask) != 0)
        {
            touched = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & layerMask) != 0)
        {
            touched = false;
        }
    }
}
