using System;
using System.Collections.Generic;
using DG.Tweening;
using Entities;
using Infrastructure;
using UnityEngine;

namespace Card
{
    public class CardHolder : MonoBehaviour
    {
        public event Action<CardHolder> Destroyed;
        [SerializeField] private CardView _cardView;
        private CardStaticData _cardStaticData;
        private CardActivator _cardActivator;

        public CardStaticData CardStaticData => _cardStaticData;
        
        public void Init(CardStaticData cardStaticData, CardActivator cardActivator)
        {
            _cardStaticData = cardStaticData;
            _cardActivator = cardActivator;
            _cardView.Init(cardStaticData.Cost, cardStaticData.Name, cardStaticData.Description, cardStaticData.Icon);
        }

        public void Use(List<Entity> targets)
        {
            _cardActivator.Use(this, targets);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            transform.DOKill();
            Destroyed?.Invoke(this);
        }
    }
}