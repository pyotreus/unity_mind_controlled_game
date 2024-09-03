using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] float health;
    public bool targetAcquired;
    public bool noisetagVisible;

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
        Debug.Log("DEATH");
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Transform sphere = transform.Find("Sphere");
        Renderer sphereRenderer = sphere.GetComponent<Renderer>();
        sphereRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
