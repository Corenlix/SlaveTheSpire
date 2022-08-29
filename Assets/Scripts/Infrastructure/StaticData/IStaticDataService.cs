using Entities.Buffs;

namespace Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        CardStaticData ForCard(CardId cardId);
        EnemyStaticData ForEnemy(EnemyId id);
        BuffStaticData ForBuff(BuffId id);
    }
}