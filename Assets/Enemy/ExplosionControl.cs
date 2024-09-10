using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionControl : MonoBehaviour
{

    public GameObject explosionPrefab;
    private bool hasExploded = false;
    public Transform player;
    public int damageAmount = 20;
    public float explosionRange = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Explosion()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }

        // Assume the player has a method to receive damage
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (Vector3.Distance(transform.position, player.position) <= explosionRange && playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }

        Destroy(gameObject);
    }
}
