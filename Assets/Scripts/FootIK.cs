using UnityEngine;

public class FootIK : MonoBehaviour
{
    private Animator animator;

    [SerializeField,Range(0, 1.0f)]
    private float rayDistance;

    private void Awake() {
        animator = GetComponent<Animator>();
    }


    private void OnAnimatorIK() {
        if (animator)
        {
            SetIk(AvatarIKGoal.LeftFoot);
            SetIk(AvatarIKGoal.RightFoot);
        }
    }

    private void SetIk(AvatarIKGoal avatarIKGoal)
    {
        animator.SetIKPositionWeight(avatarIKGoal, 1);
        animator.SetIKRotationWeight(avatarIKGoal, 1);

        Ray ray = new Ray(animator.GetIKPosition(avatarIKGoal) + Vector3.up, Vector3.down);
        Debug.DrawRay(ray.origin, Vector3.down * (rayDistance + 1), Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance + 1, 1 << LayerMask.NameToLayer("Ground")))
        {
            // 걸을 수 있는 땅이라면
            if (hit.transform.CompareTag("Ground"))
            {
                Debug.Log($"{avatarIKGoal} On Ground");
                Vector3 footPosition = hit.point;
                footPosition.y += rayDistance;

                animator.SetIKPosition(avatarIKGoal, footPosition);
                animator.SetIKRotation(avatarIKGoal, Quaternion.LookRotation(transform.forward, hit.normal));
            }
        }
    }
}
