using Card.Activators;
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
            CardHolder cardHolder = new CardHolder(cardStaticData.Cost, cardStaticData.CardActions, cardView, this);
            cardHolder.Used += DestroyCard;
            CardTargetSelector cardTargetSelector = CardActivatorFactory.InstantiateActivator(cardView.gameObject, cardStaticData.CardActivatorType);
            cardTargetSelector.Init(cardHolder);
            return cardHolder;
        }

        public void DestroyCard(CardHolder cardHolder)
        {
            cardHolder.Used -= DestroyCard;
            Object.Destroy(cardHolder.CardView.gameObject);
        }
    }
}