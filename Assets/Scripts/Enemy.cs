using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public void TakeDamage(float damage)
    {
        Debug.Log("asd");
    }
}