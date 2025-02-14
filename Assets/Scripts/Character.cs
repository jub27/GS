using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    private const float RUN_MULTIPLY = 2f;
    private const float WALK_SPEED = 3f;

    private CharacterController characterController;
    private Animator animator;
    [SerializeField] private Transform followCamTransform;
    [SerializeField] private AttackData attackDataScriptableObject;
    [SerializeField] private CharacterStatData characterStatData;

    private float jump = -10;
    private bool isRun = false;
    private float targetSpeed = 0;
    private Vector2 inputVector = Vector2.zero;
    private ParticleSystem[] attackEffects;
    private Quaternion[] attackEffectsOriginLocalRotations;
    private Vector3[] attackEffectsOriginLocalPositions;

    private bool IsMoveState
    {
        get
        {
            AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return animatorStateInfo.shortNameHash == Animator.StringToHash("Move");
        }
    }

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = attackDataScriptableObject.animatorController;
        attackEffects = new ParticleSystem[attackDataScriptableObject.attackEffects.Length];
        attackEffectsOriginLocalRotations = new Quaternion[attackDataScriptableObject.attackEffects.Length];
        attackEffectsOriginLocalPositions = new Vector3[attackDataScriptableObject.attackEffects.Length];
        for(int i = 0; i < attackEffects.Length; i++)
        {
            attackEffects[i] = Instantiate(attackDataScriptableObject.attackEffects[i]);
            attackEffects[i].Stop();
            attackEffectsOriginLocalRotations[i] = attackEffects[i].transform.localRotation;
            attackEffectsOriginLocalPositions[i] = attackEffects[i].transform.localPosition;
        }
        characterStatData.CurHp = characterStatData.MaxHp;
        HpBar.Instance.SetCharacterStatData(characterStatData);
    }

    private void OnMove(InputValue inputValue)
    {
        this.inputVector = inputValue.Get<Vector2>();
    }

    private void OnJump(InputValue inputValue)
    {
        if (!characterController.isGrounded || !IsMoveState)
            return;
        jump = inputValue.Get<float>() * 11f;
        animator.SetTrigger("Jump");
        animator.SetBool("IsGround", false);
    }

    private void OnSprint(InputValue inputValue)
    {
        isRun = inputValue.Get<float>() != 0;
    }

    private void OnAttack(InputValue inputValue)
    {
        if (inputValue.Get<float>() != 0 && characterController.isGrounded)
        {
            isRun = false;
            animator.SetTrigger("Attack");
            targetSpeed = 0;
            animator.SetFloat("Move", 0);
            animator.applyRootMotion = true;
        }
    }

    public void ShowAttackEffect(int index)
    {
        Debug.Log(index);
        attackEffects[index].transform.position = transform.position + attackEffectsOriginLocalPositions[index];
        attackEffects[index].transform.rotation = transform.rotation * attackEffectsOriginLocalRotations[index];
        attackEffects[index].Play();
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

    private void Update()
    {
        characterStatData.CurHp -= 1;
        Vector3 moveDir = GetDirection(inputVector);
        float speed = GetSpeed(inputVector);
        targetSpeed = Mathf.Lerp(targetSpeed, speed, 0.15f);
        animator.SetFloat("Move", targetSpeed / (WALK_SPEED * RUN_MULTIPLY));
        
        if(!IsMoveState)
        {
            if(animator.GetBool("IsGround"))
                speed = 0;
            moveDir = transform.forward;
        }
        else
        {
            animator.applyRootMotion = false;
        }

        if (characterController.isGrounded)
            characterController.Move((jump * Vector3.up + moveDir * speed) * Time.deltaTime);
        else
            characterController.Move((jump * Vector3.up + transform.forward * speed) * Time.deltaTime);
        if(moveDir != Vector3.zero && characterController.isGrounded)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir, Vector3.up), 0.15f);
        jump += Physics.gravity.y * Time.deltaTime;
        if (characterController.isGrounded)
            jump = -2f;
        animator.SetBool("IsGround", characterController.isGrounded);
    }
}
