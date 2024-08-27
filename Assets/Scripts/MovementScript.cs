using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private EntityBehavior entityBehaviorScript;

    [SerializeField] public float maxSpeed = 7;
    [SerializeField] Rigidbody2D rb;
    public DriverState driverState;

    public Coroutine decelerationCoroutine;
    public Coroutine accelerationCoroutine;
    [SerializeField] float accelDuration;
    [SerializeField] float decelDuration;
    public float currentSpeed = 0;

    public Vector2 movementInput;
    public Vector2 receivedEndInput;
    public Vector2 processedInput;
    [SerializeField][Range(0, 1)] private float angle;
    private bool yMovementMax;
    public bool receivingInput;
    public bool atMaxSpeed;
    public bool accelerating;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        entityBehaviorScript = GetComponent<EntityBehavior>();
    }

    private void LateUpdate()
    {
        //Movement Checking
        if (movementInput == Vector2.zero) { receivingInput = false; }
        else { receivingInput = true; }

        if (currentSpeed == maxSpeed) { atMaxSpeed = true; }
        else { atMaxSpeed = false; }

        //Movement Implementation
        if (driverState == DriverState.driving) { Movement(); }

        if (receivingInput) 
        { 
            processedInput = movementInput;
            if (decelerationCoroutine != null) { StopCoroutine(decelerationCoroutine); }
        }
        else
        {
            decelerationCoroutine = StartCoroutine(Deceleration());
            if (accelerationCoroutine != null)
            {
                StopCoroutine(accelerationCoroutine);
                accelerating = false;
            }
        }

        if (receivingInput && !accelerating && !atMaxSpeed)
        { accelerationCoroutine = StartCoroutine(Acceleration()); }
    }

    public void Movement()
    {
        if (!yMovementMax)
        {
            if (movementInput.x == 1)
            {
                entityBehaviorScript.faceDirection = EntityBehavior.FaceDirection.right;

                if (movementInput.y > 0) { movementInput.y = angle; }
                else if (movementInput.y < 0) { movementInput.y = -angle; }
            }
            else if(movementInput.x == -1)
            {
                entityBehaviorScript.faceDirection = EntityBehavior.FaceDirection.left;

                if (movementInput.y > 0) { movementInput.y = angle; }
                else if (movementInput.y < 0) { movementInput.y = -angle; }
            }
        }

        if (movementInput.y == 1)
        {
            entityBehaviorScript.faceDirection = EntityBehavior.FaceDirection.up;

            if (movementInput.x > 0) { movementInput.x = angle; }
            else if (movementInput.x < 0) { movementInput.x = -angle; }
            yMovementMax = true;
        }
        else if(movementInput.y == -1)
        {
            entityBehaviorScript.faceDirection = EntityBehavior.FaceDirection.down;

            if (movementInput.x > 0) { movementInput.x = angle; }
            else if (movementInput.x < 0) { movementInput.x = -angle; }
            yMovementMax = true;
        }
        else { yMovementMax = false; }

        transform.Translate(processedInput * currentSpeed * Time.deltaTime);
    }

    public IEnumerator Acceleration()
    {
        accelerating = true;
        float startSpeed = currentSpeed;
        float t = 0;

        while (t < 1)
        {
            float lerpedSpeed = Mathf.Lerp(startSpeed, maxSpeed, t);
            currentSpeed = lerpedSpeed;
            t += Time.deltaTime / accelDuration;
            if (t > 0.97) { t = 1; }
            yield return null;
        }
        accelerating = false;
        accelerationCoroutine = null;
    }

    public IEnumerator Deceleration()
    {
        processedInput = receivedEndInput;
        float startSpeed = currentSpeed;
        float t = 0;

        while (t < 1)
        {
            float lerpedSpeed = Mathf.Lerp(startSpeed, 0, t);
            currentSpeed = lerpedSpeed;
            t += Time.deltaTime / decelDuration;
            if (lerpedSpeed < 0.05) { t = 1; }
            yield return null;
        }

        processedInput = movementInput;
    }
}
