using UnityEngine;

public class Hitbox2: MonoBehaviour
{
    [SerializeField] private SlicableObject2 slicable;
    [SerializeField] private string sideToSlice; // "left" o "right"

    private bool hasBeenSliced = false;
    private Vector2 lastMouseWorldPos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 currentMouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 delta = currentMouseWorldPos - lastMouseWorldPos;

            bool isMovingDown = delta.y < -0.01f && Mathf.Abs(delta.y) > Mathf.Abs(delta.x);

            if (isMovingDown)
            {
                RaycastHit2D hit = Physics2D.Raycast(currentMouseWorldPos, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject && !hasBeenSliced)
                {
                    hasBeenSliced = true;
                    slicable.Slice(sideToSlice);
                }
            }

            lastMouseWorldPos = currentMouseWorldPos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            hasBeenSliced = false;
        }
    }
}
