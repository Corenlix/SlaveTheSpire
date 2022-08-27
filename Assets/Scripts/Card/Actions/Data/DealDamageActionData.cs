using UnityEngine;
using Zenject;

namespace Card.Actions.Data
{
    [CreateAssetMenu(menuName = "Card/Card Actions/Deal damage")]
    public class DealDamageActionData : ActionData
    {
        [SerializeField] private int _damage;
        
        public override ICardAction GetCardAction(DiContainer diContainer)
        {
            return new DealDamageAction(_damage);
        }
    }
}