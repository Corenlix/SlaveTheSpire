using System;
using System.Collections.Generic;
using Deck;
using Infrastructure;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Card
{
    public class CardMover : MonoBehaviour
    {
        private EventSystem _eventSystem;
        private DeckView _deckView;
        private CardMovingState _cardMovingState = CardMovingState.None;
        private CardHolder _selectedCardHolder;
        private CardTargetSelectorsPool _cardTargetSelectors;
    
        public void Init(CardTargetSelectorsPool cardTargetSelectors)
        {
            _cardTargetSelectors = cardTargetSelectors;
            _eventSystem = EventSystem.current;
        }

        public void UseDeck(DeckView deckView)
        {
            _deckView = deckView;
        }

        void Update()
        {
            switch (_cardMovingState)
            {
                case CardMovingState.None:
                    NoneStateUpdate();
                    break;
                case CardMovingState.HoldCard:
                    HoldStateUpdate();
                    break;
                case CardMovingState.SelectingTarget:
                    SelectingStateUpdate();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void NoneStateUpdate()
        {
            CardHolder cardHolderUnderCursor = FindCardUnderCursor();
            if(cardHolderUnderCursor != null)
                NoneToHoldStateTransition(cardHolderUnderCursor);
        }
    
        private void HoldStateUpdate()
        {
            CardHolder cardHolderUnderCursor = FindCardUnderCursor();
            if (_selectedCardHolder != cardHolderUnderCursor)
            {
                HoldToNoneStateTransition();
            } 
            else if(Input.GetMouseButtonDown(0))
            {
                HoldToSelectingStateTransition();
            }
        }
    
        private void SelectingStateUpdate()
        {
            if (Input.GetMouseButtonDown(1))
            {
                SelectingToNoneStateTransition();
            }
        }

        private CardHolder FindCardUnderCursor()
        {
            var eventData = new PointerEventData(_eventSystem);
            eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            _eventSystem.RaycastAll(eventData, results);
            foreach (var result in results)
            {
                if (result.gameObject.TryGetComponent(out CardHolder cardHolder))
                    return cardHolder;
            }
        
            return null;
        }
    
        private void NoneToHoldStateTransition(CardHolder holdCardHolder)
        {
            _deckView.SelectCard(holdCardHolder);
            _selectedCardHolder = holdCardHolder;
            _cardMovingState = CardMovingState.HoldCard;
        }

        private void HoldToSelectingStateTransition()
        {
            _cardMovingState = CardMovingState.SelectingTarget;
            _cardTargetSelectors.Get(_selectedCardHolder.CardStaticData.CardTargetSelectorType).StartSelecting(_selectedCardHolder);
            _cardTargetSelectors.Get(_selectedCardHolder.CardStaticData.CardTargetSelectorType).Selected += SelectingToNoneStateTransition;
        }

        private void HoldToNoneStateTransition()
        {
            _deckView.DeselectCard();
            _cardMovingState = CardMovingState.None;
        }
    
        private void SelectingToNoneStateTransition()
        {
            _cardMovingState = CardMovingState.None;
            _deckView.DeselectCard();
            var selector = _cardTargetSelectors.Get(_selectedCardHolder.CardStaticData.CardTargetSelectorType);
            selector.Selected -= SelectingToNoneStateTransition;
            selector.FinishSelecting();
        }
    
        private enum CardMovingState
        {
            None,
            HoldCard,
            SelectingTarget,
        }
    }
}