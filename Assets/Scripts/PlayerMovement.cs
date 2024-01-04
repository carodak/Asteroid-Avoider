using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float rotationSpeed;
    private Camera mainCamera;
    private Rigidbody rb;

    private Vector3 movementDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        KeepPlayerOnScreen();
        RotateToFaceVelocity();
    }

    private void FixedUpdate() {
        if(movementDirection== Vector3.zero) return;
        rb.AddForce(movementDirection * forceMagnitude, ForceMode.Force); // We don't need Time.deltaTime as we are in the FixedUpdate
        Vector3 speed = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        rb.velocity = speed;
    }

    private void ProcessInput(){
        if(Touchscreen.current.primaryTouch.press.isPressed){
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
            movementDirection = transform.position - worldPosition; //Vector between the player position and where we touch
            movementDirection.z = 0f;
            movementDirection.Normalize(); //we don't go faster if our figer is closer/far from the spaceship, only care about the direction without the magnitude
        }
        else{
            movementDirection = Vector3.zero;
        }
    }

    private void KeepPlayerOnScreen(){
        Vector3 newPosition = transform.position;

        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        if(viewportPosition.x > 1){
            newPosition.x = -newPosition.x+0.1f;
        }

        else if(viewportPosition.x < 0){
            newPosition.x = -newPosition.x-0.1f;
        }

        else if(viewportPosition.y > 1){
            newPosition.y = -newPosition.y+0.1f;
        }

        else if(viewportPosition.y < 0){
            newPosition.y = -newPosition.y-0.1f;
        }

        transform.position = newPosition;
    }

    private void RotateToFaceVelocity(){
        if(rb.velocity == Vector3.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(rb.velocity, Vector3.back);
        transform.rotation = Quaternion.Lerp(
            transform.rotation, targetRotation, rotationSpeed*Time.deltaTime);
    }
}
