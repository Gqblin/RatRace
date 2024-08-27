using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    public EntityBehavior collidedEntity;
    public int checkpointNumber;
    public bool isGoal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collidedEntity = collision.gameObject.GetComponent<EntityBehavior>();

        if (isGoal && collidedEntity.lastCheckpointPassed == 4) 
        { 
            collidedEntity.currentLap += 1;
            collidedEntity.LapComplete();
        }
        collidedEntity.lastCheckpointPassed = checkpointNumber;
    }
}
