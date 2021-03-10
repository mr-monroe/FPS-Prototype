using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float gravity = -45f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private Transform cameraCrouchPosition;


    private CharacterController characterController;
    private Camera mainCamera;
    private Vector2 moveDirection;
    private float velocityY = 0f; // for dynamic gravity applying, i.e. the longer you fall the faster the speed
    private bool hasJumpPressed = false; // for checking if jump button has been presed
    private bool isCrouching = false;
    private float smooth = 10f;
    private float actualSpeed;
    private float crouchMovespeedCoefficient = 0.4f;

    public float MoveSpeed 
    {
        get { return moveSpeed; }
        set 
        { 
            moveSpeed = value;
        }
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        // Gravity
        if (characterController.isGrounded) 
        {
            velocityY = 0f;
        }
        velocityY += gravity * Time.deltaTime;

        // Jump
        // Jump velocity = sqrt(-2 * jumpHeight * gravity)
        if (hasJumpPressed && !isCrouching) 
        {
            if (characterController.isGrounded) 
            {
                print("should jump");
                velocityY = Mathf.Sqrt(-2 * jumpHeight * gravity);
            }
            hasJumpPressed = false;
        }

        // Crouch
        if (isCrouching)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraCrouchPosition.position, Time.deltaTime * smooth);
            actualSpeed = moveSpeed * crouchMovespeedCoefficient;
        }
        else
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraPosition.position, Time.deltaTime * smooth);
            actualSpeed = moveSpeed;
        }


        print(velocityY);
        // Move
        Vector3 velocity = (transform.right * moveDirection.x + transform.forward * moveDirection.y) * actualSpeed + Vector3.up * velocityY;
        characterController.Move(velocity  * Time.deltaTime);
    }

    public void Move(Vector2 _direction)
    {
        moveDirection = _direction;
    }

    public void OnJumpPressed() 
    {
        hasJumpPressed = true;
    }

    public void OnCrouchPressed()
    {
        isCrouching = !isCrouching;
        print(actualSpeed);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadMovementSettings();
        Time.timeScale = 1f;
    }

    private void LoadMovementSettings()
    {
        PlayerData loadedData = SaveSystem.LoadSettings();
        if (loadedData != null)
            moveSpeed = loadedData.movespeed;
    }
}
