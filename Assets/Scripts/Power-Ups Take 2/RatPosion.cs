using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatPosion : ItemBehavior
{
    [Tooltip("Regulates the strength of the ratPoison")]
    [SerializeField] float poisonStrength;

    [Tooltip("Regulates the speed decline duration of the poison")]
    [SerializeField] float poisonLength;

    public override IEnumerator ItemEffect(GameObject user)
    {
        MovementScript uScript = user.GetComponent<MovementScript>();
        uScript.currentSpeed -= poisonStrength;
        yield return new WaitForSecondsRealtime(poisonLength);
        uScript.currentSpeed = uScript.maxSpeed;
    }
}
