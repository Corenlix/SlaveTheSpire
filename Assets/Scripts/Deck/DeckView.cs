using System.Collections.Generic;
using System.Linq;
using Cards;
using DG.Tweening;
using UnityEngine;

namespace Deck
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private Transform _cardsContainer;
        private readonly List<Card> _cards = new();
        private Card _selectedCard;
        
        public void AddCard(Card card)
        {
            card.transform.SetParent(_cardsContainer);
            card.transform.position = Vector3.zero;
            card.transform.localScale = Vector3.one * 0.1f;
            card.transform.rotation = Quaternion.Euler(0, 0, -90f);
            _cards.Add(card);
            card.Destroyed += RemoveCard;
            
            MoveCards();
        }

        public void SelectCard(Card card)
        {
            DeselectCard();
            _selectedCard = card;

            card.transform.DOKill();
            var scale = 1.3f;
            card.transform.localScale = scale * Vector2.one;
            card.transform.position = new Vector2(card.transform.position.x, card.GetComponent<RectTransform>().sizeDelta.y*card.transform.lossyScale.y/2f);
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
            float cardWidth = _cards[0].GetComponent<RectTransform>().rect.width;
            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].transform.DOLocalMove(new Vector3(-_cards.Count / 2f * cardWidth + cardWidth * i + cardWidth/2f, 0), 0.5f);
                _cards[i].transform.DOScale(1, 0.5f);
                _cards[i].transform.DOLocalRotate(Vector3.zero, 0.5f);
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