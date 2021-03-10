using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensetivity = 3.5f;

    private Transform playerBody;
    private Camera playerCamera;
    private float cameraPitch = 0f;

    private void Awake()
    {
        playerBody = transform;
        playerCamera = Camera.main;
    }

    public void UpdateMouseLook(Vector2 direction)
    {
        Vector2 mouseDelta = direction;
        
        // Vertical Rotation
        cameraPitch -= mouseDelta.y * mouseSensetivity * Time.deltaTime;
        cameraPitch = Mathf.Clamp(cameraPitch, -50f, 50f);
        playerCamera.transform.localEulerAngles = Vector3.right * cameraPitch;

        // Horizontal Rotation
        playerBody.Rotate(Vector3.up * mouseDelta.x * Time.deltaTime);
    }
}
