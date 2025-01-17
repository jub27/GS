using UnityEngine;
using UnityEngine.Pool;

public class DamageText : MonoBehaviour
{
    private static ListPool<DamageText> pool = null;

    private void Awake()
    {
        if(pool == null)
        {
            pool = new ListPool<DamageText>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
