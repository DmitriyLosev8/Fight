using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    
    private float _health;
    private float _strengthOfAttack;
    

    public int IdOfType {  get; private set; }
    public float MinBorderOfChanceOfCreate { get; private set; }
    public float MaxBorderOfChanceOfCreate { get; private set; }
    public float DelayOfAttack { get; private set; }

    public void Init()
    {
        IdOfType = _enemyData.IdOfType;
        _health = _enemyData.Health;
        _strengthOfAttack = _enemyData.StrengthOfAttack;
        MinBorderOfChanceOfCreate = _enemyData.MinBorderOfChanceOfCreate;
        MaxBorderOfChanceOfCreate = _enemyData.MaxBorderOfChanceOfCreate;
        DelayOfAttack = _enemyData.DelayOfAttack;
    }
}
