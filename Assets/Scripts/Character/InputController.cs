using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private MovingController controller;
    private Vector2 downMousePosition;

    void Update()
    {
        SlideInput();
    }

    /// <summary>
    /// Считывает слайды мыши
    /// </summary>
    void SlideInput()
    {
        if (Input.GetMouseButtonDown(0))
        {

            downMousePosition = Input.mousePosition;

        }
        else if (Input.GetMouseButtonUp(0))
        {

            Vector2 upMousePosition = Input.mousePosition;

            controller.Swipe(new Vector2(
                (downMousePosition.x - upMousePosition.x) / Screen.width,
                (downMousePosition.y - upMousePosition.y) / Screen.height
                )
            );

        }
    }

    /// <summary>
    /// Считывает свайпы пальцем
    /// </summary>
    void SwipeInput()
    {
        // Доделай...
    }
}
