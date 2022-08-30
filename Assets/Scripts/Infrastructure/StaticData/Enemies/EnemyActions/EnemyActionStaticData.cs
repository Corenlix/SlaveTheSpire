using Entities;
using UnityEngine;
using Zenject;

public abstract class EnemyActionStaticData : ScriptableObject
{
    public abstract IEnemyAction GetEnemyAction(DiContainer diContainer, Enemy actionOwner);
}