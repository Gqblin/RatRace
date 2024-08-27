using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBehavior : MonoBehaviour
{
    public enum FaceDirection
    {
        up,
        right,
        down,
        left
    }

    public FaceDirection faceDirection;

    public MovementScript mScript;
    public CarHealth hScript;
    public int entityID;
    public int lastCheckpointPassed = 0;
    public int currentLap = 1;
    public float currentLapTimer;
    public float previousLapTime;
    public float personalRecord;
    public float[] lapTimes;

    private Animator animator;

    protected virtual void Awake()
    {
        mScript = gameObject.GetComponent<MovementScript>();
        hScript = gameObject.GetComponent<CarHealth>();
    }

    public virtual void Start()
    {
        GameManager.instance.entities.Add(this);
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (lapTimes.Length == 0)
        {
            lapTimes = new float[GameManager.instance.maxLaps];
        }

        if (GameManager.instance.gameState == GameState.Racing)
        {
            currentLapTimer += Time.deltaTime;
        }
        animator.SetFloat("LookDir", (float)faceDirection);

        GameManager.instance.playerLapTimes = lapTimes;
    }

public virtual void LapComplete()
    {
        previousLapTime = currentLapTimer;
        lapTimes[currentLap - 2] = previousLapTime;
        currentLapTimer = 0;
    }

    protected virtual void SendDataToManager()
    {
        GameManager gmInstance = GameManager.instance;
    }
}
