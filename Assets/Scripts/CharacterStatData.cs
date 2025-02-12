using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatData", menuName = "Scriptable Objects/CharacterStatData")]
public class CharacterStatData : ScriptableObject
{
    public int maxHp;
    public int curHp;
}