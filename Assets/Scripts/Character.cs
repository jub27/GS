using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    private CharacterController characterController;
    private Vector2 moveDir = Vector2.zero;
    private float jump = -10;
    [SerializeField]private Transform followCamTransform;
    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnMove(InputValue inputValue)
    {
        moveDir = inputValue.Get<Vector2>();
    }

    public void OnJump(InputValue inputValue)
    {
        jump += inputValue.Get<float>() * 13f;
    }

    private void Update()
    {
        Vector3 forward = followCamTransform.forward;
        forward.y = 0;
        Vector3 right = followCamTransform.right;

        right.y = 0;

        //transform.Translate(5 * Time.deltaTime * (forward * moveDir.y + right * moveDir.x));
        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);

        characterController.Move((jump * Vector3.up + (forward * moveDir.y + right * moveDir.x) * 5) * Time.deltaTime);
        jump += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            jump = -2f;
        }
    }
}
