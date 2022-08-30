using System.Collections.Generic;
using System.Linq;
using Card;
using DG.Tweening;
using UnityEngine;

namespace Deck
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private Transform _drawPile;
        [SerializeField] private Transform _cardsContainer;
        private readonly List<CardHolder> _cards = new();
        
        public void AddCard(CardHolder cardHolder)
        {
            cardHolder.transform.SetParent(_cardsContainer);
            cardHolder.transform.position = _drawPile.position;
            cardHolder.transform.localScale = Vector3.one * 0.1f;
            cardHolder.transform.rotation = Quaternion.Euler(0, 0, -90f);
            _cards.Add(cardHolder);
            cardHolder.Destroyed += RemoveCard;
            
            MoveCards();
        }

        public void SelectCard(CardHolder cardHolder)
        {
            DeselectCard();
            cardHolder.transform.DOKill();
            float scale = 1.3f;
            cardHolder.transform.localScale = scale * Vector2.one;
            cardHolder.transform.position = new Vector2(cardHolder.transform.position.x, cardHolder.GetComponent<RectTransform>().rect.height * scale/2f);
            cardHolder.transform.SetAsLastSibling();
        }

        public void DeselectCard()
        {
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

        private void RemoveCard(CardHolder cardHolder)
        {
            _cards.Remove(cardHolder);
            MoveCards();
            cardHolder.Destroyed -= RemoveCard;
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