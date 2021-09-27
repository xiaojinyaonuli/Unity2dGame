using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CanDamage 
{
    void EnemyTopGetHit(Vector2 direction);

    void EnemyComboatk1GetHit(Vector2 direction);

    void EnemyGetHit(Vector2 direction);

    void Enemy_I_SkillGetHit(Vector2 direction);

    int Damage{get;set;}

    
}
