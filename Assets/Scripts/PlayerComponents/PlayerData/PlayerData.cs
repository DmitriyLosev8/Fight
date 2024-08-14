using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
internal class PlayerData : ScriptableObject
{
    [field: SerializeField] public float Health;
    [field: SerializeField] public float Armor;
    [field: SerializeField] public float StrengthOfAttack;
    [field: SerializeField] public float PreAttackDelay;
}
