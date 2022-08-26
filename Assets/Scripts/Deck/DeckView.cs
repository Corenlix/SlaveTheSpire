using System.Collections.Generic;
using Card;
using DG.Tweening;
using UnityEngine;

namespace Deck
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private Transform _cardsContainer;
        [SerializeField] private List<CardView> _cards;
        
        public void AddCard(CardView card)
        {
            card.transform.SetParent(_cardsContainer);
            _cards.Add(card);
            MoveCards();
        }

        public void SelectCard(CardView card)
        {
            DeselectCard();
            card.transform.DOKill();
            float scale = 1.5f;
            card.transform.localScale = 1.5f * Vector2.one;
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

        public void RemoveCard(CardView card)
        {
            _cards.Remove(card);
            MoveCards();
        }
    }
}