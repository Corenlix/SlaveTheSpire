using System;
using UnityEngine;

public class CardFactory : MonoBehaviour
{
    [SerializeField] private CardView _cardViewPrefab;
    
    public void CreateCard(int cost, ICardAction action)
    {
        Card card = new Card(cost, action);
        CardView cardView = Instantiate(_cardViewPrefab);
        cardView.GetComponent<CardActivator>().Init(card);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            CreateCard(1, new CardActionDestroyEntity());
    }
}