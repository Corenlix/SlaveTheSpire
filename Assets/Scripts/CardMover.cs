using System;
using System.Collections.Generic;
using Card;
using Card.TargetSelectors;
using Deck;
using Infrastructure;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class CardMover : MonoBehaviour
{
    private EventSystem _eventSystem;
    private DeckView _deckView;
    private CardMovingState _cardMovingState = CardMovingState.None;
    private CardHolder _selectedCard;
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
        CardHolder cardUnderCursor = FindCardUnderCursor();
        if(cardUnderCursor != null)
            NoneToHoldStateTransition(cardUnderCursor);
    }
    
    private void HoldStateUpdate()
    {
        CardHolder cardUnderCursor = FindCardUnderCursor();
        if (_selectedCard != cardUnderCursor)
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
            _cardTargetSelectors.Get(_selectedCard.CardStaticData.CardTargetSelectorType).FinishSelecting();
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
    
    private void NoneToHoldStateTransition(CardHolder holdCard)
    {
        _deckView.SelectCard(holdCard);
        _selectedCard = holdCard;
        _cardMovingState = CardMovingState.HoldCard;
    }

    private void HoldToSelectingStateTransition()
    {
        _cardMovingState = CardMovingState.SelectingTarget;
        _cardTargetSelectors.Get(_selectedCard.CardStaticData.CardTargetSelectorType).StartSelecting(_selectedCard);
        _cardTargetSelectors.Get(_selectedCard.CardStaticData.CardTargetSelectorType).Selected += SelectingToNoneStateTransition;
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
        _cardTargetSelectors.Get(_selectedCard.CardStaticData.CardTargetSelectorType).Selected -= SelectingToNoneStateTransition;
    }
    
    private enum CardMovingState
    {
        None,
        HoldCard,
        SelectingTarget,
    }
}