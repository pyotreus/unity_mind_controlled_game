using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] float health;
    public bool targetAcquired;
    //public bool noisetagVisible;
    //public GameObject explosionPrefab;
    //public Transform player;
    private ExplosionControl explosionControl;

    void Start()
    {
        //Transform sphere = transform.Find("Sphere");
        //Renderer sphereRenderer = sphere.GetComponent<Renderer>();
        //sphereRenderer.enabled = false;
        explosionControl = gameObject.GetComponent<ExplosionControl>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health > 0)
        {
            Debug.Log("HIT");
        }
        
        if (health <= 0)
        {
            EnemyDeath();
        }
        
    }

    private void EnemyDeath()
    {
        explosionControl.Explosion();
        Destroy(gameObject);
    }


}
