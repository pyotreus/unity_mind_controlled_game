using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f; // Speed at which the enemy moves
    public float rotationSpeed = 100.0f; // Speed at which the enemy rotates
    public float changeDirectionTime = 2.0f; // Time interval to change direction

    private float timeToChangeDirection = 0.0f; // Timer to track when to change direction
    private float randomRotationY; // Stores the random rotation value for Y-axis

    void Start()
    {
        // Set initial random direction
        ChangeDirection();
    }

    void Update()
    {
        // Move the enemy forward in the current direction
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // Rotate the enemy around the Y-axis
        transform.Rotate(Vector3.up, randomRotationY * rotationSpeed * Time.deltaTime);

        // Update the timer
        timeToChangeDirection -= Time.deltaTime;

        // If it's time to change direction, pick a new random direction
        if (timeToChangeDirection <= 0.0f)
        {
            ChangeDirection();
        }
    }

    // Method to change the movement direction randomly
    private void ChangeDirection()
    {
        // Generate a new random Y rotation
        randomRotationY = Random.Range(-1.0f, 1.0f); // Random value between -1 and 1

        // Set a new random time for the next direction change
        timeToChangeDirection = Random.Range(1.0f, changeDirectionTime); // Random interval between 1 and changeDirectionTime seconds
    }
}
