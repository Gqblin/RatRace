using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : EntityBehavior
{
    private enum Difficulty
    {
        easy,
        normal,
        hard,
        expert
    }

    [SerializeField] private Difficulty difficulty = Difficulty.normal;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask roadSideLayer;

    private Inventory inv;

    [SerializeField] private float viewDistance;
    [SerializeField] private float reactionTime;
    [SerializeField] private float minSpaceBetweenDrivers;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        inv = GetComponent<Inventory>();
        mScript.movementInput.x = 1;
    }

    protected override void Update()
    {
        Collider2D[] targetsInView = Physics2D.OverlapCircleAll(transform.position, 120, layerMask);
        for(int i = 0; i < targetsInView.Length; i++)
        {
            if(targetsInView[i].transform.root != transform)
            {
                Transform otherDriver = targetsInView[i].transform;
                float dstToTarget = Vector2.Distance(transform.position, otherDriver.position);
                if(dstToTarget < viewDistance)
                {
                    Vector2 dirToTarget = (otherDriver.position - transform.position);
                    switch (faceDirection)
                    {
                        case FaceDirection.right:
                            if ((dirToTarget.x < viewDistance && dirToTarget.x > 0) && (dirToTarget.y < minSpaceBetweenDrivers && dirToTarget.y > -minSpaceBetweenDrivers))
                            {
                                mScript.movementInput.y = 1;
                            }
                            else
                            {
                                mScript.movementInput.y = 0;
                            }
                            break;
                        case FaceDirection.left:
                            if (dirToTarget.x > -viewDistance && (dirToTarget.y < minSpaceBetweenDrivers && dirToTarget.y > -minSpaceBetweenDrivers))
                            {
                                //mScript.movementInput.y = 1;
                            }
                            else
                            {
                                //mScript.movementInput.y = 0;
                            }
                            break;
                        case FaceDirection.up:
                            if (dirToTarget.y < viewDistance && (dirToTarget.x < minSpaceBetweenDrivers && dirToTarget.x > -minSpaceBetweenDrivers))
                            {
                                //mScript.movementInput.x = 1;
                            }
                            else
                            {
                                //mScript.movementInput.x = 0;
                            }
                            break;
                        case FaceDirection.down:
                            if (dirToTarget.y > -viewDistance && (dirToTarget.x < minSpaceBetweenDrivers && dirToTarget.x > -minSpaceBetweenDrivers))
                            {
                                //mScript.movementInput.x = 1;
                            }
                            else
                            {
                                //mScript.movementInput.x = 0;
                            }
                            break;
                    }     
                }
            }
        }

        if (Physics2D.Raycast(transform.position, Vector2.right, viewDistance, roadSideLayer))
        {
            print("right");
        }
        if (Physics2D.Raycast(transform.position, Vector2.down, viewDistance, roadSideLayer))
        {
            print("down");
        }
        if (Physics2D.Raycast(transform.position, Vector2.up, viewDistance, roadSideLayer))
        {
            print("up");
        }
        if (Physics2D.Raycast(transform.position, Vector2.left, viewDistance, roadSideLayer))
        {
            print("left");
        }
    }
}
