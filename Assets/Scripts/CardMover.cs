using System;
using System.Collections.Generic;
using Card;
using Card.TargetSelectors;
using Deck;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMover : MonoBehaviour
{
    [SerializeField] private DeckView _deckView;
    [SerializeField]  GraphicRaycaster _raycaster;
    PointerEventData _pointerEventData;
    [SerializeField] EventSystem _eventSystem;
    private CardMovingState _cardMovingState = CardMovingState.None;
    private CardView _selectedCard;
    
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
        _pointerEventData = new PointerEventData(_eventSystem);
        _pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        _raycaster.Raycast(_pointerEventData, results);
        foreach (var result in results)
        {
            if (result.gameObject.TryGetComponent<CardView>(out var cardView))
            {
                _deckView.SelectCard(cardView);
                _selectedCard = cardView;
                _cardMovingState = CardMovingState.HoldCard;
                return;
            }
        }
    }
    
    private void HoldStateUpdate()
    {
        _pointerEventData = new PointerEventData(_eventSystem);
        _pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        _raycaster.Raycast(_pointerEventData, results);
        foreach (var result in results)
        {
            if (result.gameObject.TryGetComponent<CardView>(out var cardView))
            {
                if (_selectedCard == cardView)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        _cardMovingState = CardMovingState.SelectingTarget;
                        _selectedCard.GetComponent<CardTargetSelector>().StartSelecting();
                        _selectedCard.GetComponent<CardTargetSelector>().Selected += OnSelect;
                    }
                    return;
                }

                _deckView.SelectCard(cardView);
                _selectedCard = cardView;
            }
        }
        _deckView.DeselectCard();
        _cardMovingState = CardMovingState.None;
    }

    private void SelectingStateUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _selectedCard.GetComponent<CardTargetSelector>().FinishSelecting();
            _cardMovingState = CardMovingState.None;
            _deckView.DeselectCard();
            _selectedCard.GetComponent<CardTargetSelector>().Selected -= OnSelect;
        }
    }

    private void OnSelect(List<Entity> targets)
    {
        _selectedCard.GetComponent<CardTargetSelector>().Selected -= OnSelect;
        _cardMovingState = CardMovingState.None;
        _deckView.DeselectCard();
    }

    enum CardMovingState
    {
        None,
        HoldCard,
        SelectingTarget,
    }
}