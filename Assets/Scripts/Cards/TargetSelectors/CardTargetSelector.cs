using System;
using System.Collections.Generic;
using Entities;
using Infrastructure.Factories;
using UnityEngine;

namespace Cards.TargetSelectors
{
    public abstract class  CardTargetSelector : MonoBehaviour
    {
        public event Action Selected;
        
        [SerializeField] private CardTargetSelectorType _selectorType;
    
        protected Card SelectedCard { get; private set; }

        private bool _isSelecting;

        public CardTargetSelectorType SelectorType => _selectorType;
        
        public void StartSelecting(Card card)
        {
            if (_isSelecting)
                return;

            SelectedCard = card;
            OnStartSelecting();
            _isSelecting = true;
        }

        protected void SelectTargets(List<Entity> targets)
        {
            FinishSelecting();
            SelectedCard.Use(targets);
            Selected?.Invoke();
        }

        public void FinishSelecting()
        {
            if (!_isSelecting)
                return;
            
            _isSelecting = false;
            OnFinishSelecting();
        }

        protected abstract void OnStartSelecting();

        private void Update()
        {
            if(_isSelecting)
                OnSelectingUpdate();
        }

        protected abstract void OnSelectingUpdate();

        protected abstract void OnFinishSelecting();
    }
}