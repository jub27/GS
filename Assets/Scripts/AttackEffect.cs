using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(100);
        }
    }
}
