using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseWheel : ItemBehavior
{
    [Tooltip("Regulates the speed boost strength of the cheese")]
    [SerializeField] float cheeseStrength;

    [Tooltip("Regulates the speed boost duration of the cheese")]
    [SerializeField] float cheeseLength;

    public override IEnumerator ItemEffect(GameObject user)
    {
        MovementScript uScript = user.GetComponent<MovementScript>();
        uScript.currentSpeed = uScript.maxSpeed * cheeseStrength;
        yield return new WaitForSecondsRealtime(cheeseLength);
        uScript.currentSpeed = uScript.maxSpeed;
    }
}
