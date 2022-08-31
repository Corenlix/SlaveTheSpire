using Cards.Actions;
using Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Cards.CardActions
{
    [CreateAssetMenu(menuName = "Card/Card Actions/Deal damage")]
    public class DealDamageActionData : ActionData
    {
        [SerializeField] private int _damage;
        
        public override ICardAction GetCardAction(DiContainer diContainer)
        {
            return new DealDamageAction(_damage, diContainer.Resolve<IGameFactory>());
        }
    }
}