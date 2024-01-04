using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth == null) return;
        
        playerHealth.Crash();
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
