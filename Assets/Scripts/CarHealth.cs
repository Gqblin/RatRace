using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHealth : MonoBehaviour
{
    public DriverState driverState;
    public int maxHealth = 3;
    public int currentHealth;

    MovementScript pm;

    private void Start()
    {
        currentHealth = maxHealth;
        pm = gameObject.GetComponent<MovementScript>();
    }

    private void Update()
    {
        pm.driverState = driverState;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0)
        {
            StartCoroutine(RepairCar());
        }
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
    }

    IEnumerator RepairCar()
    {
        driverState = DriverState.dead;
        yield return new WaitForSeconds(5);
        driverState = DriverState.driving;
        currentHealth = maxHealth;
    }
}
