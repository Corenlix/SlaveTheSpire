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

        private void MoveCards()
        {
            float center = Screen.currentResolution.width / 2;
            float cardWidth = _cards[0].GetComponent<RectTransform>().rect.width;
            float cardHeight = _cards[0].GetComponent<RectTransform>().rect.height;
            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].transform.DOMove(new Vector3(center -_cards.Count / 2f * cardWidth + cardWidth * i + cardWidth/2f, cardHeight/2f), 0.5f);
            }
        }

        public void RemoveCard(CardView card)
        {
            _cards.Remove(card);
            MoveCards();
        }
    }
}