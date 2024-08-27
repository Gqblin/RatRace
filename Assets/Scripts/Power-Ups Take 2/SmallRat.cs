using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRat : ItemBehavior
{
    [SerializeField] private float speed = 7;
    [SerializeField] int ImpactDamage = 1;
    [SerializeField] float gracePeriod = 1; 
    [SerializeField] float stunLength = 2;

    private Vector2 direction;

    public override IEnumerator ItemEffect(GameObject user)
    {
        MovementScript uScript = user.GetComponent<MovementScript>();
        GameObject instantiatedGO = Instantiate(gameObject, user.transform.position, user.transform.rotation);

        switch (user.GetComponent<EntityBehavior>().faceDirection)
        {
            case EntityBehavior.FaceDirection.up:
                direction = Vector2.up;
                break;
            case EntityBehavior.FaceDirection.right:
                direction = Vector2.right;
                break;
            case EntityBehavior.FaceDirection.down:
                direction = Vector2.down;
                break;
            case EntityBehavior.FaceDirection.left:
                direction = Vector2.left;
                break;
        }

        yield return new WaitForSecondsRealtime(gracePeriod);
        Debug.LogWarning("Grace Period Over!");
        instantiatedGO.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void Update()
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject victim = collision.gameObject;
        //StartCoroutine(MouseTrapActivation(victim));
    }

}
