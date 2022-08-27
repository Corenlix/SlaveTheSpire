using System;
using System.Collections.Generic;
using Deck;
using DG.Tweening;
using Entities;
using Infrastructure.Factories;
using UnityEngine;

namespace Card
{
    public class CardHolder : MonoBehaviour
    {
        public event Action<CardHolder> Destroyed;
        [SerializeField] private CardView _cardView;
        private CardStaticData _cardStaticData;

        public CardStaticData CardStaticData => _cardStaticData;
        
        public void Init(CardStaticData cardStaticData)
        {
            _cardStaticData = cardStaticData;
            _cardView.Init(cardStaticData.Cost, cardStaticData.Name, cardStaticData.Description, cardStaticData.Icon);
        }

        public void Use(List<Entity> targets)
        {
            _cardStaticData.CardActions.ForEach(x=> x.Activate(targets));
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            transform.DOKill();
            Destroyed?.Invoke(this);
        }
    }
}