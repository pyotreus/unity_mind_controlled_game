using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{

    Transform enemyTransform;
    Transform _Player;
    EnemyHealth enemyHealth;
    GameObject enemyObject;
    float distance;
    public Transform head, barrel;
    public float distanceLimit;
    public GameObject _Projectile;
    public float fireRate, nextFire;
    private GameObject currentTarget;  // Current enemy target

    void Start()
    {
        enemyObject = GameObject.FindGameObjectWithTag("Kevin");
        if (enemyObject != null)
        {
            enemyHealth = enemyObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null )
            {
                enemyTransform = enemyObject.transform;
            }
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget != null)
        {
            enemyHealth = currentTarget.GetComponent<EnemyHealth>();
            enemyTransform = currentTarget.transform;
            distance = Vector3.Distance(enemyTransform.position, transform.position);
            if (distance <= distanceLimit && enemyHealth.targetAcquired)
            {
                head.LookAt(enemyTransform);
                if (Time.time > nextFire)
                {
                    nextFire = Time.time + 1f / fireRate;
                    shoot();
                }

            }
        }

    }

    void shoot()
    {
        GameObject clone = Instantiate(_Projectile, barrel.position, head.rotation);
        clone.GetComponent<Rigidbody>().AddForce(head.forward * 1500);
        Destroy(clone, 10);
    }


    // Method to aim at the specified enemy
    public void AimAtEnemy(GameObject enemy)
    {
        currentTarget = enemy;
        // Add your logic here to start aiming at the enemy
        //Debug.Log("Turret aiming at: " + currentTarget.name);
    }

    // Method to stop aiming
    public void StopAiming()
    {
        currentTarget = null;
        // Add your logic here to stop aiming
        //Debug.Log("Turret stopped aiming.");
    }
}
