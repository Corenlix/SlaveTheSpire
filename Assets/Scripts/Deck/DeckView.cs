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
        private readonly List<CardGameObject> _cards = new();
        
        public void AddCard(CardGameObject cardGameObject)
        {
            cardGameObject.transform.SetParent(_cardsContainer);
            cardGameObject.transform.position = _drawPile.position;
            cardGameObject.transform.localScale = Vector3.one * 0.1f;
            cardGameObject.transform.rotation = Quaternion.Euler(0, 0, -90f);
            _cards.Add(cardGameObject);
            cardGameObject.Destroyed += RemoveCard;
            
            MoveCards();
        }

        public void SelectCard(CardGameObject cardGameObject)
        {
            DeselectCard();
            cardGameObject.transform.DOKill();
            float scale = 1.3f;
            cardGameObject.transform.localScale = scale * Vector2.one;
            cardGameObject.transform.position = new Vector2(cardGameObject.transform.position.x, cardGameObject.GetComponent<RectTransform>().rect.height * scale/2f);
            cardGameObject.transform.SetAsLastSibling();
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

        private void RemoveCard(CardGameObject cardGameObject)
        {
            _cards.Remove(cardGameObject);
            MoveCards();
            cardGameObject.Destroyed -= RemoveCard;
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