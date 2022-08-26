using System.Collections.Generic;
using Card.TargetSelectors;
using Deck;
using DG.Tweening;
using Infrastructure;
using UnityEngine;

namespace Card
{
    public class CardHolder : MonoBehaviour
    {
        [SerializeField] private CardView _cardView;
        private CardStaticData _cardStaticData;
        private DeckHolder _deck;
        private IGameFactory _gameFactory;

        public CardStaticData CardStaticData => _cardStaticData;
        
        public void Init(CardStaticData cardStaticData, DeckHolder deck)
        {
            _deck = deck;
            _cardStaticData = cardStaticData;
            _cardView.Init(cardStaticData.Cost, cardStaticData.Name, cardStaticData.Description, cardStaticData.Icon);
            _deck.AddCard(this);
        }

        public void Use(List<Entity> targets)
        {
            _cardStaticData.CardActions.ForEach(x=> x.Activate(targets));
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            transform.DOKill();
            _deck.RemoveCard(this);
        }
    }
}