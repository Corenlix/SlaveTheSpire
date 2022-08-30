using System;
using Entities;
using Infrastructure;
using Zenject;

public interface IEnemyAction
{
    event Action<IEnemyAction> ActionEnded;
    void Use();
}