using UnityEngine;

namespace Card.Actions.Data
{
    [CreateAssetMenu(menuName = "Card/Card Actions/Deal damage")]
    public class DealDamageActionData : ActionData
    {
        [SerializeField] private int _damage;
        
        public override ICardAction GetCardAction()
        {
            return new DealDamageAction(_damage);
        }
    }
}