using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;
using Utilities;

public class EnemiesContainer : MonoBehaviour
{
    [SerializeField] private List<EnemyPoint> _enemyPoints;

    public bool HasFreePoint()
    {
        return GetFreePoint() != null;
    }

    public void Add(Enemy enemy)
    {
        _enemyPoints.Shuffle();
        var freePoint = GetFreePoint();
        if (freePoint == null)
            throw new ArgumentOutOfRangeException();
        freePoint.Hold(enemy);
        enemy.Destroyed += Remove;
    }
    
    public void Remove(Enemy enemy) {
        GetPointForEnemy(enemy)?.UnHold();
        enemy.Destroyed -= Remove;
    }

    private EnemyPoint GetFreePoint()
    {
        return _enemyPoints.FirstOrDefault(x => x.IsFree);
    }

    private EnemyPoint GetPointForEnemy(Enemy enemy)
    {
        return _enemyPoints.FirstOrDefault(x => x.HoldEnemy == enemy);
    }
}