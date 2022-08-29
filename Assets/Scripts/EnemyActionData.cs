using UnityEngine;
using Zenject;

public abstract class EnemyActionData : ScriptableObject
{
    public abstract IEnemyAction GetEnemyAction(DiContainer diContainer);
}