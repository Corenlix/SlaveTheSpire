using System;

public interface IEnemyAction
{
    event Action<IEnemyAction> ActionEnded;
    void Use();
}