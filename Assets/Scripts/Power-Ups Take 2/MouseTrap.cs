using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrap : ItemBehavior
{
    [Tooltip("Regulates the damage inflicted by the mousetrap")]
    [SerializeField] float MousetrapStrength;

    [Tooltip("Regulates the period of time allowed before collision with the original user is allowed.")]
    [SerializeField] float gracePeriod;

    [Tooltip("Regulates the period of time that the MouseTrap holds the player.")]
    [SerializeField] float trapPeriod;

    public override IEnumerator ItemEffect(GameObject user)
    {
        MovementScript uScript = user.GetComponent<MovementScript>();
        GameObject instantiatedGO = Instantiate(gameObject, user.transform.position, user.transform.rotation);
        yield return new WaitForSecondsRealtime(gracePeriod);
        Debug.LogWarning("Grace Period Over!");
        instantiatedGO.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject victim = collision.gameObject;
        StartCoroutine(MouseTrapActivation(victim));
    }

    public IEnumerator MouseTrapActivation(GameObject entity)
    {
        Debug.Log("Victim Caught!");
        spriteRenderer = entity.GetComponent<SpriteRenderer>();
        CarHealth entityHScript = entity.GetComponent<CarHealth>();

        entityHScript.driverState = DriverState.stunned;
        entityHScript.ChangeHealth(-1);
        for(int i = 0; i < 5; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(.15f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(.15f);
        }
        //yield return new WaitForSecondsRealtime(trapPeriod);
        if (entityHScript.driverState == DriverState.stunned) { entityHScript.driverState = DriverState.driving; }
        Destroy(gameObject);
    }
}
