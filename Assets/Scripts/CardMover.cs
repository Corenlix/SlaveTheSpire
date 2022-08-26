using System;
using System.Collections.Generic;
using Card;
using Deck;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class CardMover : MonoBehaviour
{
    private GraphicRaycaster _raycaster;
    private EventSystem _eventSystem;
    private DeckHolder _deckHolder;
    private CardMovingState _cardMovingState = CardMovingState.None;
    private CardHolder _selectedCard;

    [Inject]
    private void Init(GraphicRaycaster raycaster, EventSystem eventSystem)
    {
        _raycaster = raycaster;
        _eventSystem = eventSystem;
    }

    public void UseDeck(DeckHolder deckHolder)
    {
        _deckHolder = deckHolder;
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
        var pointerEventData = new PointerEventData(_eventSystem);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        _raycaster.Raycast(pointerEventData, results);
        foreach (var result in results)
        {
            if (result.gameObject.TryGetComponent<CardHolder>(out var cardHolder))
            {
                _deckHolder.SelectCard(cardHolder);
                _selectedCard = cardHolder;
                _cardMovingState = CardMovingState.HoldCard;
                return;
            }
        }
    }
    
    private void HoldStateUpdate()
    {
        var pointerEventData = new PointerEventData(_eventSystem);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        _raycaster.Raycast(pointerEventData, results);
        foreach (var result in results)
        {
            if (result.gameObject.TryGetComponent<CardHolder>(out var cardHolder))
            {
                if (_selectedCard == cardHolder)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        _cardMovingState = CardMovingState.SelectingTarget;
                        _selectedCard.CardTargetSelector.StartSelecting();
                        _selectedCard.CardTargetSelector.Selected += OnSelect;
                    }
                    return;
                }

                _deckHolder.SelectCard(cardHolder);
                _selectedCard = cardHolder;
            }
        }
        _deckHolder.DeselectCard();
        _cardMovingState = CardMovingState.None;
    }

    private void SelectingStateUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _selectedCard.CardTargetSelector.FinishSelecting();
            _cardMovingState = CardMovingState.None;
            _deckHolder.DeselectCard();
            _selectedCard.CardTargetSelector.Selected -= OnSelect;
        }
    }

    private void OnSelect(List<Entity> targets)
    {
        _selectedCard.CardTargetSelector.Selected -= OnSelect;
        _cardMovingState = CardMovingState.None;
        _deckHolder.DeselectCard();
    }

    enum CardMovingState
    {
        None,
        HoldCard,
        SelectingTarget,
    }
}