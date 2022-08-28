using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Card.TargetSelectors
{
    public abstract class CardTargetSelector : MonoBehaviour
    {
        public event Action Selected;
        
        [SerializeField] private CardTargetSelectorType _selectorType;
        protected CardHolder SelectedCardHolder { get; private set; }
        private bool _isSelecting;

        public CardTargetSelectorType SelectorType => _selectorType;
        
        public void StartSelecting(CardHolder cardHolder)
        {
            if (_isSelecting)
                return;
            
            _isSelecting = true;
            SelectedCardHolder = cardHolder;
            OnStartSelecting();
        }

        protected void SelectTargets(List<Entity> targets)
        {
            FinishSelecting();
            SelectedCardHolder.Use(targets);
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