using Card;
using Deck;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private DeckView _deckView;
    [SerializeField] private CardFactory _cardFactory;
    [SerializeField] private CardStaticData _cardStaticData;
    private DeckHolder _deckHolder;
    
    private void Start()
    {
        _deckHolder = new DeckHolder(_deckView);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            CardHolder card = _cardFactory.SpawnCard(_cardStaticData);
            _deckHolder.AddCard(card);
        }
    }
}
