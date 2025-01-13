using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    private CharacterController characterController;
    private Vector2 moveDir = Vector2.zero;
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

    private void Update() {
        Vector3 forward = followCamTransform.forward;
        forward.y = 0;
        Vector3 right = followCamTransform.right;
        right.y = 0;

        transform.Translate((forward * moveDir.y + right * moveDir.x) * 5 * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }
}
