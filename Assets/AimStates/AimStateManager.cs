using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    AimBaseState currentState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();

    [SerializeField] float mouseSense = 1;
    [SerializeField] Transform camFollowPos;
    float xAxis, yAxis;

    [HideInInspector] public Animator anim;
    [HideInInspector] public CinemachineVirtualCamera vCam;
    public float adsFov = 40;
    [HideInInspector] public float hipFov = 10;
    [HideInInspector] public float currentFov =10;
    public float fovSmoothSpeed = 10;

    public Transform aimPosition;
    [HideInInspector] Vector3 actualAimPosition;
    [SerializeField] float aimSmoothSpeed = 20f;
    [SerializeField] LayerMask aimMask;

    private EnemyHealth currentTarget;


    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFov = vCam.m_Lens.FieldOfView;
        anim = GetComponent<Animator>();
        SwitchState(Hip);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -80, 80);

        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);

        Vector2 screenCentre = new Vector2(Screen.width/2, Screen.height/2);
        Ray ray = Camera.main.ScreenPointToRay(screenCentre);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPosition.position = Vector3.Lerp(aimPosition.position, hit.point, aimSmoothSpeed * Time.deltaTime);
            // Check if the hit object has the tag "Kevin"
            if (hit.collider.CompareTag("Kevin"))
            {
                GameObject enemyObject = hit.collider.gameObject;
                currentTarget = hit.collider.GetComponent<EnemyHealth>();

                Transform sphereTransform = enemyObject.transform.Find("Sphere");
                if (sphereTransform != null)
                {
                    SetSphereVisibility(currentTarget.gameObject, true);
                    NoisetagBehaviour noisetagBehavior = sphereTransform.GetComponent<NoisetagBehaviour>();
                    noisetagBehavior.isAimed = true;
                    
                    // Enemy is targeted and has targetAcquired == true
                    // Notify turret to aim at this enemy
                    //NotifyTurretToAim(currentTarget.gameObject);
                }
                else
                {
                    // Enemy is not a valid target or targetAcquired is false
                    //NotifyTurretToStopAiming();
                }
            }
            else
            {
                //SetSphereVisibility(currentTarget?.gameObject, false);
                //currentTarget = null;
                //NotifyTurretToStopAiming();
            }
        }
        else
        {
            // No object is hit by the ray
            //NotifyTurretToStopAiming();
        }

        currentState.UpdateState(this);
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    private void NotifyTurretToAim(GameObject enemy)
    {
        // Notify all turrets to aim at the specified enemy
        TurretControl[] turrets = FindObjectsOfType<TurretControl>();
        foreach (TurretControl turret in turrets)
        {
            turret.AimAtEnemy(enemy);
        }
    }

    private void NotifyTurretToStopAiming()
    {
        // Notify all turrets to stop aiming
        TurretControl[] turrets = FindObjectsOfType<TurretControl>();
        foreach (TurretControl turret in turrets)
        {
            turret.StopAiming();
        }
    }

    private void SetSphereVisibility(GameObject enemy, bool isVisible)
    {
        if (enemy == null) return;

        // Assuming the sphere is a direct child of the enemy GameObject
        Transform sphere = enemy.transform.Find("Sphere");  // Replace "Sphere" with the actual name of the sphere child

        if (sphere != null)
        {
            Renderer sphereRenderer = sphere.GetComponent<Renderer>();
            if (sphereRenderer != null)
            {
                sphereRenderer.enabled = isVisible;
            }
        }
    }
}
