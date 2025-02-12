using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatData", menuName = "Scriptable Objects/CharacterStatData")]
public class CharacterStatData : ScriptableObject
{
    public event Action OnCurHpChanged;
    public event Action OnMaxHpChanged;
    [SerializeField]private int maxHp;
    public int MaxHp
    {
        get
        {
            return maxHp;
        }
        set
        {
            maxHp = value;
            OnMaxHpChanged?.Invoke();
        }
    }
    private int curHp;
    public int CurHp
    {
        get
        {
            return curHp;
        }
        set
        {
            curHp = value;
            OnCurHpChanged?.Invoke();
        }
    }
}