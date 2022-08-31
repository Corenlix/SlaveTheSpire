using System;
using System.Collections.Generic;
using Cards.TargetSelectors;
using DG.Tweening;
using Entities;
using Infrastructure.StaticData.Cards;
using UnityEngine;

namespace Cards
{
    public class Card : MonoBehaviour
    {
        public event Action<Card> Destroyed;
        
        [SerializeField] private CardView _cardView;
        public CardTargetSelectorType CardTargetSelectorType { get; private set; }
        public CardId CardId { get; private set; }
        private ICardActivator _cardActivator;

        public void Init(CardStaticData cardStaticData, ICardActivator cardActivator)
        {
            CardTargetSelectorType = cardStaticData.CardTargetSelectorType;
            CardId = cardStaticData.Id;
            _cardActivator = cardActivator;
            _cardView.Init(cardStaticData.Cost, cardStaticData.Name, cardStaticData.Description, cardStaticData.Icon);
        }

        public bool IsAvailableToUse() =>_cardActivator.IsAvailableToUse();

        public void Use(List<Entity> targets)
        {
            _cardActivator.Use(targets);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
            transform.DOKill();
        }
    }
}