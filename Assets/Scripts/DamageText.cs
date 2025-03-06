using TMPro;
using UnityEngine;
using DG.Tweening;

public class DamageText : MonoBehaviour
{
    private TextMeshPro textMesh;
    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void SetText(string str)
    {
        textMesh.text = str;
    }

    public void StartAnimation()
    {
        transform.DOLocalMoveY(0.7f, 0.8f).SetRelative();
        transform.localScale = Vector3.one;
        transform.DOScale(0.7f, 0.81f).OnComplete(()=>
        {
            DamageTextManager.Instance.Pool.Release(this);
        });
    }

    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}