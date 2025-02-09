using UnityEngine;

public class FootIK : MonoBehaviour
{
    private Animator animator;

    private float rayDistance = 0.7f;

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

        Ray ray = new Ray(animator.GetIKPosition(avatarIKGoal) + new Vector3(0, rayDistance / 2f, 0), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, 1 << LayerMask.NameToLayer("Ground")))
        {
            // 걸을 수 있는 땅이라면
            if (hit.transform.CompareTag("Ground"))
            {
                Debug.Log("@@");
                Vector3 footPosition = animator.GetIKPosition(avatarIKGoal);
                footPosition.y = Mathf.Lerp(footPosition.y, hit.point.y + 0.1f, 0.3f);

                animator.SetIKPosition(avatarIKGoal, footPosition);
                animator.SetIKRotation(avatarIKGoal, Quaternion.LookRotation(transform.forward, hit.normal));
            }
        }
    }
}
