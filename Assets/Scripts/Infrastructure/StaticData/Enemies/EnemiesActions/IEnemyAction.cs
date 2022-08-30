using System;

namespace Infrastructure.StaticData.Enemies.EnemiesActions
{
    public interface IEnemyAction
    {
        event Action<IEnemyAction> ActionEnded;
        void Use();
    }
}