using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    [Header("Bounds")]
    public float minX = -50f;
    public float maxX = 50f;
    public float minY = -50f;
    public float maxY = 50f;

    [Header("Zoom")]
    public float zoomSpeed = 0.02f;
    public float minZoom = 5f;
    public float maxZoom = 25f;

    [Header("Drag")]
    public float dragSpeed = 1f;

    private Camera cam;

    private Vector3 dragOrigin;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        HandleZoom();

        HandleDrag();

        ClampCamera();
    }

    void HandleZoom()
    {
        float scroll =
            Mouse.current.scroll.ReadValue().y;

        if (scroll != 0)
        {
            cam.orthographicSize -=
                scroll * zoomSpeed;

            cam.orthographicSize =
                Mathf.Clamp(
                    cam.orthographicSize,
                    minZoom,
                    maxZoom
                );
        }
    }

    void HandleDrag()
    {
        if (Mouse.current.middleButton.wasPressedThisFrame)
        {
            dragOrigin =
                cam.ScreenToWorldPoint(
                    Mouse.current.position.ReadValue()
                );
        }

        if (Mouse.current.middleButton.isPressed)
        {
            Vector3 currentPos =
                cam.ScreenToWorldPoint(
                    Mouse.current.position.ReadValue()
                );

            Vector3 difference =
                dragOrigin - currentPos;

            transform.position += difference;
        }
    }

    void ClampCamera()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(
            pos.x,
            minX,
            maxX
        );

        pos.y = Mathf.Clamp(
            pos.y,
            minY,
            maxY
        );

        transform.position = pos;
    }


}