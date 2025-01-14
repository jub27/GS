using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    private Vector2 moveDir = Vector2.zero;
    private float jump = -10;
    [SerializeField]private Transform followCamTransform;
    private float speed = 0;
    private float runSpeed = 5f;
    private float walkSPeed = 3f;
    private bool isRun = false;
    private float targetSpeed = 0;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputValue inputValue)
    {
        moveDir = inputValue.Get<Vector2>();
    }

    public void OnJump(InputValue inputValue)
    {
        jump += inputValue.Get<float>() * 13f;
    }

    public void OnSprint(InputValue inputValue)
    {
        isRun = inputValue.Get<float>() != 0;
    }

    private void Update()
    {
        Vector3 forward = followCamTransform.forward;
        forward.y = 0;
        Vector3 right = followCamTransform.right;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        

        speed = moveDir.normalized.magnitude * walkSPeed;

        if(isRun)
            speed = runSpeed;
        if(moveDir == Vector2.zero)
        {
            speed = 0;
            isRun = false;
        }

        targetSpeed = Mathf.Lerp(targetSpeed, speed, 0.1f);
        animator.SetFloat("Move", targetSpeed / runSpeed);

        Vector3 moveVector = (forward * moveDir.y + right * moveDir.x).normalized * speed;

        characterController.Move((jump * Vector3.up + moveVector) * Time.deltaTime);
        if(moveDir != Vector2.zero)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveVector, Vector3.up), 0.3f);

        jump += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            jump = -2f;
        }
    }
}
