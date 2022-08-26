using Card.TargetSelectors;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(menuName="Factories/Card Factory")]
    public class CardFactory : ScriptableObject
    {
        [SerializeField] private CardView _cardViewPrefab;
    
        public CardHolder SpawnCard(CardStaticData cardStaticData)
        {
            CardView cardView = Instantiate(_cardViewPrefab);
            cardView.Init(cardStaticData.Cost, cardStaticData.Name, cardStaticData.Description, cardStaticData.Icon);
            CardTargetSelector cardTargetSelector = CardTargetSelectorFactory.InstantiateActivator(cardView.gameObject, cardStaticData.CardTargetSelectorType);
            CardHolder cardHolder = new CardHolder(cardStaticData.Cost, cardStaticData.CardActions, cardView, this, cardTargetSelector);
            cardHolder.Used += DestroyCard;
            return cardHolder;
        }

        public void DestroyCard(CardHolder cardHolder)
        {
            cardHolder.Used -= DestroyCard;
            cardHolder.Dispose();
            Destroy(cardHolder.CardView.gameObject);
        }
    }
}