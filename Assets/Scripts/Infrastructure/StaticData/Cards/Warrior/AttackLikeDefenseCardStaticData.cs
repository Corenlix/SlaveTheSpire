using Cards;
using Cards.CardActions;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Cards.Warrior
{
    [CreateAssetMenu(menuName = MenuNames.CardDataMenuName+"Warrior/Attack like defense")]
    public class AttackLikeDefenseCardStaticData : CardStaticData
    {
        public override ICardAction GetCardAction(DiContainer diContainer)
        {
            return new AttackLikeDefenseCardAction();
        }
    }
}