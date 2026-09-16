using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class CameraInputHandler : MonoBehaviour
{
    [Header("Pan Settings")]
    [SerializeField] private float panSpeed = 0.1f;
    [SerializeField] private float panXMin = -15f, panXMax = 15f;
    [SerializeField] private float panZMin = -15f, panZMax = 15f;

    [Header("Zoom Settings")]
    [SerializeField] private float zoomSpeed = 0.05f;
    [SerializeField] private float zoomMin = 5f, zoomMax = 20f;

    [Header("Target")]
    [SerializeField] private CinemachineCamera vc_Gameplay;

    private Vector2 lastPanPos;
    private bool isPanning;

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouseInput();
#else
        HandleTouchInput();
#endif
    }

    void HandleMouseInput()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            lastPanPos = mouse.position.ReadValue();
            isPanning = true;
        }
        if (mouse.leftButton.wasReleasedThisFrame) isPanning = false;

        if (isPanning)
        {
            Vector2 delta = mouse.position.ReadValue() - lastPanPos;
            Pan(-delta.x * panSpeed, -delta.y * panSpeed);
            lastPanPos = mouse.position.ReadValue();
        }

        float scroll = mouse.scroll.ReadValue().y;
        if (scroll != 0f) Zoom(-scroll * 0.01f);
    }

    void HandleTouchInput()
    {
        var touches = Touch.activeTouches;

        if (touches.Count == 1)
        {
            var t = touches[0];
            if (t.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                lastPanPos = t.screenPosition;
                isPanning = true;
            }
            if (t.phase == UnityEngine.InputSystem.TouchPhase.Ended) isPanning = false;

            if (isPanning && t.phase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                Vector2 delta = t.screenPosition - lastPanPos;
                Pan(-delta.x * panSpeed, -delta.y * panSpeed);
                lastPanPos = t.screenPosition;
            }
        }
        else if (touches.Count == 2)
        {
            isPanning = false;
            var t0 = touches[0];
            var t1 = touches[1];

            float prevDist = Vector2.Distance(
                t0.screenPosition - t0.delta,
                t1.screenPosition - t1.delta);
            float currDist = Vector2.Distance(t0.screenPosition, t1.screenPosition);
            Zoom((prevDist - currDist) * zoomSpeed);
        }
    }

    void Pan(float dx, float dz)
    {
        if (vc_Gameplay == null) return;
        Vector3 pos = vc_Gameplay.transform.position;
        pos.x = Mathf.Clamp(pos.x + dx, panXMin, panXMax);
        pos.z = Mathf.Clamp(pos.z + dz, panZMin, panZMax);
        vc_Gameplay.transform.position = pos;
    }

    void Zoom(float delta)
    {
        if (vc_Gameplay == null) return;
        var lens = vc_Gameplay.Lens;
        lens.OrthographicSize = Mathf.Clamp(lens.OrthographicSize + delta, zoomMin, zoomMax);
        vc_Gameplay.Lens = lens;
    }
}
