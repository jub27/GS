using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    private const float RUN_SPEED = 5f;
    private const float WALK_SPEED = 3f;

    private CharacterController characterController;
    private Animator animator;
    [SerializeField] private Transform followCamTransform;

    private float jump = -10;
    private float speed = 0;
    private bool isRun = false;
    private float targetSpeed = 0;
    private Vector3 moveDir = Vector3.zero;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 inputVector = inputValue.Get<Vector2>();
        SetDirection(inputVector);
        SetSpeed(inputVector);
    }

    private void OnJump(InputValue inputValue)
    {
        jump += inputValue.Get<float>() * 13f;
    }

    private void OnSprint(InputValue inputValue)
    {
        isRun = inputValue.Get<float>() != 0;
    }

    private void SetDirection(Vector2 inputDir)
    {
        Vector3 forward = followCamTransform.forward;
        forward.y = 0;
        Vector3 right = followCamTransform.right;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveDir = right * inputDir.x + forward * inputDir.y;
        moveDir.Normalize();
    }

    private void SetSpeed(Vector2 inputDir)
    {
        speed = inputDir.normalized.magnitude * WALK_SPEED;
        if (speed == 0)
            isRun = false;
        if (isRun)
            speed = RUN_SPEED;
    }

    private void FixedUpdate()
    {
        targetSpeed = Mathf.Lerp(targetSpeed, speed, 0.15f);
        animator.SetFloat("Move", targetSpeed / RUN_SPEED);

        characterController.Move((jump * Vector3.up + moveDir * speed) * Time.deltaTime);
        if(moveDir != Vector3.zero)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir, Vector3.up), 0.15f);

        jump += Physics.gravity.y * Time.deltaTime;
        if (characterController.isGrounded)
            jump = -2f;
    }
}
