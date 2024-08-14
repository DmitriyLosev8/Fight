using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
internal class EnemyData : ScriptableObject
{
    [field: SerializeField] public int IdOfType;
    [field: SerializeField] public int MinBorderOfChanceOfCreate;
    [field: SerializeField] public int MaxBorderOfChanceOfCreate;
    [field: SerializeField] public float Health;
    [field: SerializeField] public float StrengthOfAttack;   
    [field: SerializeField] public float DelayOfAttack;
    [field: SerializeField] public float SpeedOfAttack;
}
