using System.Collections.Generic;
using Card;
using DG.Tweening;
using UnityEngine;

namespace Deck
{
    public class DeckHolder : MonoBehaviour
    {
        [SerializeField] private Transform _cardsContainer;
        private readonly List<CardHolder> _cards = new();
        
        public void AddCard(CardHolder card)
        {
            card.transform.SetParent(_cardsContainer);
            _cards.Add(card);
            MoveCards();
        }

        public void SelectCard(CardHolder card)
        {
            DeselectCard();
            card.transform.DOKill();
            float scale = 1.3f;
            card.transform.localScale = scale * Vector2.one;
            card.transform.position = new Vector2(card.transform.position.x, card.GetComponent<RectTransform>().rect.height * scale/2f);
            card.transform.SetAsLastSibling();
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
                _cards[i].transform.DOMove(new Vector3(center -_cards.Count / 2f * cardWidth*0.8f + cardWidth * i + cardWidth/2f, cardHeight/2f), 0.5f);
                _cards[i].transform.DOScale(1, 0.5f);
            }
        }

        public void RemoveCard(CardHolder card)
        {
            _cards.Remove(card);
            MoveCards();
        }
    }
}