using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tape : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            CarHealth ch = collision.GetComponent<CarHealth>();
            if(ch.currentHealth < ch.maxHealth)
            {
                collision.GetComponent<CarHealth>().ChangeHealth(1);
                Destroy(gameObject);
            }
        }
    }
}
