using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")] 

    [SerializeField] float fireRate;
    [SerializeField] bool semiAuto;
    float fireRateTimer;

    [Header("Bullet properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPosition;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletPerShot;
    public float damage = 20;
    AimStateManager aim;
    void Start()
    {
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFire())
        {
            Fire();
        }
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }

    void Fire()
    {
        
        fireRateTimer = 0;
        //barrelPosition.LookAt(aim.aimPosition);

        //Debug.Log("TEST1");
        for (int i = 0;i < bulletPerShot; i++)
        {
            //Debug.Log("TEST2");
            GameObject currentBullet = Instantiate(bullet, barrelPosition.position, barrelPosition.rotation);
            Bullet bulletScript = currentBullet.GetComponent<Bullet>();
            bulletScript.weapon = this;
            Rigidbody rigidbody = currentBullet.GetComponent<Rigidbody>();
            rigidbody.AddForce(barrelPosition.forward * bulletVelocity, ForceMode.Impulse);
            
        }
    }
}
