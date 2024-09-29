using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : MonoBehaviour
{
    public Transform player;          // Reference to the player's transform
    public float moveSpeed = 5.0f;    // Speed at which the enemy moves
    public float rotationSpeed = 5.0f; // Speed at which the enemy rotates to face the player
    public float explosionRange = 2.0f; // Range at which the enemy explodes and deals damage
    public int damageAmount = 20;
    private ExplosionControl explosionControl;


    void Start()
    {
        // Set initial random direction
        player = GameObject.FindGameObjectWithTag("Player").transform;
        explosionControl = gameObject.GetComponent<ExplosionControl>();
    }

    void Update()
    {

        RotateTowardsPlayer();
        // Move towards the player
        MoveTowardsPlayer();

        // Check if within explosion range
        if (Vector3.Distance(transform.position, player.position) <= explosionRange)
        {
            explosionControl.Explosion();
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    // Method to move the enemy towards the player
    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }
    
}
