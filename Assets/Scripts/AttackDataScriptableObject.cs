using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackDataScriptableObject", menuName = "Scriptable Objects/AttackDataScriptableObject")]
public class AttackDataScriptableObject : ScriptableObject
{
    public AnimatorController animatorController;
    public ParticleSystem[] attackEffects;
}