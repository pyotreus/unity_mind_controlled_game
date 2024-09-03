using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float currentMoveSpeed;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float runSpeed = 7, runBackSpeed = 5;
    public float crouchSpeed = 2, crouchBackSpeed = 1;
    public float airSpeed = 1.5f;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float hzInput, vInput;
    CharacterController controller;

    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePosition;

    #region Gravity
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpForce = 10;
    [HideInInspector] public bool jumped;
    Vector3 velocity;
    #endregion

    [HideInInspector] public Animator animator;

    #region States
    public MovementBaseState previousState;
    public MovementBaseState currentState;

    public IdleState idle = new IdleState();
    public WalkState walk = new WalkState(); 
    public RunState run = new RunState();
    public CrouchingState crouch = new CrouchingState();   
    public JumpState jump = new JumpState();
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(idle);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();
        //Falling();
        animator.SetFloat("hzInput", hzInput);
        animator.SetFloat("vInput", vInput);
        currentState.UpdateState(this);

    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove() {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        Vector3 airDirection = Vector3.zero;
        //if (!IsGrounded())
        //{
        //    airDirection = transform.forward * vInput + transform.right * hzInput;
        //}
        //else
        //{
            
        //}
        direction = transform.forward * vInput + transform.right * hzInput;
        controller.Move(direction.normalized * currentMoveSpeed * Time.deltaTime);
    }

    //TODO nie dziala
    public bool IsGrounded() {
        spherePosition = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePosition, controller.radius - 0.05f, groundMask)) return true;
        return false;
    }

    void Gravity() {
        if (!IsGrounded()) {
            velocity.y += gravity * Time.deltaTime;
        } else if (velocity.y < 0) {
            velocity.y = -2;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    void Falling()
    {
        animator.SetBool("Falling", !IsGrounded());
    }

    public void JumpForce()
    {
        
        velocity.y += jumpForce;
    }

    public void Jumped() {
        jumped = true;
    }

    //private void OnDrawGizmos() {
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(spherePosition, controller.radius - 0.05f);
    //}
}
