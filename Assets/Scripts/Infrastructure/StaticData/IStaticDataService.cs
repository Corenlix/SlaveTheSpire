using Entities.Buffs;
using Infrastructure.StaticData.Buffs;
using Infrastructure.StaticData.Cards;
using Infrastructure.StaticData.Enemies.EnemiesActions;

namespace Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        CardStaticData ForCard(CardId cardId);
        EnemyStaticData ForEnemy(EnemyId id);
        BuffStaticData ForBuff(BuffId id);
    }
}