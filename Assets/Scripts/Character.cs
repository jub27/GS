using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    private const float RUN_MULTIPLY = 2f;
    private const float WALK_SPEED = 3f;

    private CharacterController characterController;
    private Animator animator;
    [SerializeField] private Transform followCamTransform;

    private float jump = -10;
    private bool isRun = false;
    private float targetSpeed = 0;
    private Vector2 inputVector = Vector2.zero;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void OnMove(InputValue inputValue)
    {
        inputVector = inputValue.Get<Vector2>();
    }

    private void OnLook(InputValue inputValue)
    {
        Vector2 inputVector = inputValue.Get<Vector2>();
        Debug.Log(inputVector);
    }

    private void OnJump(InputValue inputValue)
    {
        if (!characterController.isGrounded)
            return;
        jump = inputValue.Get<float>() * 11f;
    }

    private void OnSprint(InputValue inputValue)
    {
        isRun = inputValue.Get<float>() != 0;
    }

    private Vector3 GetDirection(Vector2 inputDir)
    {
        Vector3 forward = followCamTransform.forward;
        forward.y = 0;
        Vector3 right = followCamTransform.right;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = right * inputDir.x + forward * inputDir.y;
        return moveDir.normalized;
    }

    private float GetSpeed(Vector2 inputVector)
    {
        return inputVector.normalized.magnitude * WALK_SPEED * (isRun ? RUN_MULTIPLY : 1);
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = GetDirection(inputVector);
        float speed = GetSpeed(inputVector);
        targetSpeed = Mathf.Lerp(targetSpeed, speed, 0.15f);
        animator.SetFloat("Move", targetSpeed / (WALK_SPEED * RUN_MULTIPLY));
        
        if (characterController.isGrounded)
            characterController.Move((jump * Vector3.up + moveDir * speed) * Time.fixedDeltaTime);
        else
            characterController.Move((jump * Vector3.up + transform.forward * speed) * Time.fixedDeltaTime);
        if(moveDir != Vector3.zero && characterController.isGrounded)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir, Vector3.up), 0.15f);
        jump += Physics.gravity.y * Time.fixedDeltaTime;
        if (characterController.isGrounded)
            jump = -2f;
        animator.SetBool("IsGround", characterController.isGrounded);
    }
}
