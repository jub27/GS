using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public void TakeDamage(float damage)
    {
        DamageText damageText = DamageTextManager.Instance.Pool.Get();
        damageText.transform.position = transform.position;
        damageText.SetText(Mathf.FloorToInt(damage).ToString());
        damageText.StartAnimation();
    }
}