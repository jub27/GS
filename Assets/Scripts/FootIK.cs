using UnityEngine;

public class FootIK : MonoBehaviour
{
    private Animator animator;

    private float distanceGround = 5f;

    private void Awake() {
        animator = GetComponent<Animator>();
    }


    private void OnAnimatorIK() {
        if (animator)
        {
            Debug.Log("?@@");
            // Left Foot
            // Position �� Rotation weight ����
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);

            ///<summary>
            /// GetIKPosition 
            ///   => IK�� �Ϸ��� ��ü�� ��ġ �� ( �Ʒ����� �ƹ�Ÿ���� LeftFoot�� �ش��ϴ� ��ü�� ��ġ �� )
            /// Vector3.up�� ���� ���� 
            ///   => origin�� ��ġ�� ���� �÷� �ٴڿ� ���� �ٴ��� �ν� ���ϴ� �� �����ϱ� ����
            ///      (LeftFoot�� �߸� ������ �ֱ� ������ �߹ٴڰ� ��� ���� �Ÿ��� �ְ�, Vector3.up�� �������� ������ �߸� �������� ó���� �Ǿ� �� �Ϻΰ� �ٴڿ� ����.)
            ///</summary>
            Ray leftRay = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);

            // distanceGround: LeftFoot���� �������� �Ÿ�
            // +1�� ���� ����: Vector3.up�� ���־��� ����
            if (Physics.Raycast(leftRay, out RaycastHit leftHit, distanceGround + 1f, 1 << 3))
            {
                // ���� �� �ִ� ���̶��
                if (leftHit.transform.tag == "Ground")
                {
                    Vector3 footPosition = leftHit.point;
                    //footPosition.y += distanceGround;

                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, leftHit.normal));
                }
            }

            // Right Foot
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);

            Ray rightRay = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);

            if (Physics.Raycast(rightRay, out RaycastHit rightHit, distanceGround + 1f, 1 << 3))
            {
                if (rightHit.transform.tag == "Ground")
                {
                    Vector3 footPosition = rightHit.point;
                    //footPosition.y += distanceGround;

                    animator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, rightHit.normal));
                }
            }
        }
    }
}
