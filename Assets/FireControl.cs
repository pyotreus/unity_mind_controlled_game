using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class FireControl : MonoBehaviour
{
    private Image rectangleImage;  // Reference to the Image component
    private Text rectangleText;    // Reference to the Text component
    private GameObject currentTarget;

    void Start()
    {
        // Cache the components
        rectangleImage = GetComponent<Image>();
        rectangleText = GetComponentInChildren<Text>();
        SetFireButton(false);
    }

    void Update()
    {

    }

    public void SetFireButton(bool isVisible)
    {
        rectangleImage.enabled = isVisible;
        rectangleText.enabled = isVisible;   
    }

    public void SetTarget(GameObject enemy)
    {
        if (enemy != null)
        {
            currentTarget = enemy;
            SetFireButton(true);
        }

    }

    public void Fire()
    {
        EnemyHealth enemyHealth = currentTarget.GetComponent<EnemyHealth>();
        enemyHealth.targetAcquired = true;
        SetFireButton(false);
    }

}
