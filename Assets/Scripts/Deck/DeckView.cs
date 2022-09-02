using System.Collections.Generic;
using System.Linq;
using Cards;
using DG.Tweening;
using UnityEngine;

namespace Deck
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private Transform _drawPile;
        [SerializeField] private Transform _cardsContainer;
        private readonly List<Card> _cards = new();
        private Card _selectedCard;
        
        public void AddCard(Card card)
        {
            card.transform.SetParent(_cardsContainer);
            card.transform.position = _drawPile.position;
            card.transform.localScale = Vector3.one * 0.1f;
            card.transform.rotation = Quaternion.Euler(0, 0, -90f);
            _cards.Add(card);
            card.Destroyed += RemoveCard;
            
            MoveCards();
        }

        public void SelectCard(Card card)
        {
            _selectedCard = card;
            DeselectCard();
            
            card.transform.DOKill();
            float scale = 1.3f;
            card.transform.localScale = scale * Vector2.one;
            card.transform.position = new Vector2(card.transform.position.x, card.GetComponent<RectTransform>().rect.height * scale/2f);
            card.transform.SetAsLastSibling();
        }

        public void DeselectCard()
        {
            if(_selectedCard)
                _selectedCard.transform.DOKill();
            MoveCards();
        }

        private void MoveCards()
        {
            if (_cards.Count == 0)
                return;
            float center = Screen.currentResolution.width / 2f;
            float cardWidth = _cards[0].GetComponent<RectTransform>().rect.width;
            float cardHeight = _cards[0].GetComponent<RectTransform>().rect.height;
            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].transform.DOMove(new Vector3(center -_cards.Count / 2f * cardWidth + cardWidth * i + cardWidth/2f, cardHeight/2f), 0.5f);
                _cards[i].transform.DOScale(1, 0.5f);
                _cards[i].transform.DORotate(Vector3.zero, 0.5f);
            }
        }

        private void RemoveCard(Card card)
        {
            _cards.Remove(card);
            MoveCards();
            card.Destroyed -= RemoveCard;
        }

        public void DiscardCards()
        {
            foreach (var card in _cards.ToList())
            {
                Destroy(card.gameObject);
            }
        }
    }
}