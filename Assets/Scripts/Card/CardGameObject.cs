using System;
using System.Collections.Generic;
using Card.TargetSelectors;
using DG.Tweening;
using Entities;
using UnityEngine;

namespace Card
{
    public class CardGameObject : MonoBehaviour
    {
        public event Action<CardGameObject> Destroyed;
        [SerializeField] private CardView _cardView;
        private ICardActivator _cardActivator;
        private CardTargetSelectorType _cardTargetSelectorType;

        public CardTargetSelectorType CardTargetSelectorType => _cardTargetSelectorType;

        public void Init(int cost, string name, string description, Sprite icon,
            CardTargetSelectorType cardTargetSelectorType, ICardActivator cardActivator)
        {
            _cardActivator = cardActivator;
            _cardView.Init(cost, name, description, icon);
            _cardTargetSelectorType = cardTargetSelectorType;
        }

        public bool IsAvailableToUse() => _cardActivator.IsAvailableToUse();

        public void Use(List<Entity> targets)
        {
            _cardActivator.Use(targets);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            transform.DOKill();
            Destroyed?.Invoke(this);
        }
    }
}