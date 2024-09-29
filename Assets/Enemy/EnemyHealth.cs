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
    public delegate void EnemyDestroyed();
    public event EnemyDestroyed OnEnemyDestroyed;

    void Start()
    {
        explosionControl = gameObject.GetComponent<ExplosionControl>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        //if (health > 0)
        //{
        //    Debug.Log("HIT");
        //}
        
        if (health <= 0)
        {
            EnemyDeath();
        }
        
    }

    private void EnemyDeath()
    {
        // Notify any listeners that the enemy is destroyed
        if (OnEnemyDestroyed != null)
        {
            OnEnemyDestroyed.Invoke();
        }
        explosionControl.Explosion();
        Destroy(gameObject);
    }

    public void NotifyTurretToAim()
    {
        targetAcquired = true;
        TurretControl[] turrets = FindObjectsOfType<TurretControl>();
        foreach (TurretControl turret in turrets)
        {
            turret.AimAtEnemy(gameObject);
        }
    }


}
