using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour {
    //Player Controller for the whole player: Movement & camera.
    //This script should be attached to the main player object.


    //Serialized Variables:
    [Header("Basic Movement")]
    [SerializeField] private float _MovementSpeed = 5;
    [SerializeField] private bool _CanRun = true;
    [SerializeField] private float _RunningSpeed = 9;
    [SerializeField] private KeyCode runningKey = KeyCode.LeftShift;
    [Space(10)]
    [Header("Jumping")]
    [SerializeField] private bool _CanJump = true;
    [SerializeField] private float _JumpingStrength = 2;
    [SerializeField] private float _GroundedThreshold = 0.1f;
    [Space(10)]
    [Header("Camera Settings")]
    [SerializeField] private float _CameraSensitivity = 2;
    [SerializeField] private float _CameraSmoothing = 1.5f;


    //Private Variables:  
    private Rigidbody rb;
    
    //Private Movement Variables:
    private bool _IsRunning;

    //Private Jumping Variables:
    private bool _IsGrounded;

    //Private Camera Variables:
    private Camera cameraObject;
    private Vector2 velocity;
    private Vector2 frameVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Locks the mouse to the center of the screen.
        cameraObject = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        HandleCamera();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void LateUpdate()
    {
        HandleJumps();
    }

    private void HandleMovement()
    {
        _IsRunning = _CanRun && Input.GetKey(runningKey); //If we can run, AND we press the key...

        float targetMovingSpeed = _IsRunning ? _RunningSpeed : _MovementSpeed; //FloatValue = BoolTrue? OptionYes : OptionNo
        float speedX = Input.GetAxis("Horizontal") * targetMovingSpeed;
        float speedY = Input.GetAxis("Vertical") * targetMovingSpeed;
        Vector2 targetVelocity = new Vector2(speedX, speedY);
        
        rb.linearVelocity = transform.rotation * new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.y);
    }
    private void HandleJumps()
    {
        if (_CanJump) 
        {
            Vector3 origin = transform.position + Vector3.up * 0.01f; //Shoot the ray 1cm above the ground downwards, so that it doesnt start under the ground.
            _IsGrounded = Physics.Raycast(origin, Vector3.down, _GroundedThreshold);

            if (Input.GetButtonDown("Jump") && _IsGrounded) //If we press Jump and we are grounded, we Jump!
            {
                rb.AddForce(Vector3.up * 100 * _JumpingStrength);
            }
        }
    }
    private void HandleCamera()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * _CameraSensitivity);

        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / _CameraSmoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // Rotate camera up-down and controller left-right from velocity.
        cameraObject.transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        transform.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }
}
