using Cards;
using Cards.CardActions.Warrior;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Cards.Warrior
{
    [CreateAssetMenu(menuName = MenuNames.CardDataMenuName+"Warrior/DefaultAttack")]
    public class DefaultAttackCardStaticData : CardStaticData
    {
        [SerializeField] private int _damage;
        
        public override ICardAction GetCardAction(DiContainer diContainer)
        {
            return new DefaultAttackAction(_damage);
        }
    }
}