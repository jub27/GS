using UnityEngine;
using UnityEngine.Pool;

public class DamageTextManager : Singleton<DamageTextManager>
{
    [SerializeField] private DamageText damageTextPrefab;
    private  ObjectPool<DamageText> pool = null;
    public ObjectPool<DamageText> Pool
    {
        get
        {
            return pool;
        }
    }

    void Awake()
    {
        pool = new ObjectPool<DamageText>(OnCreateDamageText,OnTakeDamageTextFromPool, OnReturnedDamageTextToPool, OnDestroyDamageText, true, 5, 50);
    }

    private DamageText OnCreateDamageText()
    {
        return Instantiate(damageTextPrefab);
    }

    private void OnTakeDamageTextFromPool(DamageText damageText)
    {
        damageText.gameObject.SetActive(true);
    }

    private void OnReturnedDamageTextToPool(DamageText damageText)
    {
        damageText.gameObject.SetActive(false);
    }

    private void OnDestroyDamageText(DamageText damageText)
    {
        Destroy(damageText.gameObject);
    }
}
