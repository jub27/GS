using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Scriptable Objects/AttackData")]
public class AttackData : ScriptableObject
{
    public AnimatorController animatorController;
    public ParticleSystem[] attackEffects;
}