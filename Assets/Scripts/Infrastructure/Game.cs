using UnityEngine;

namespace Infrastructure
{
    public class Game
    {
        private readonly IGameFactory _gameFactory;
        private readonly Canvas _canvas;

        public Game(IGameFactory gameFactory, Canvas canvas)
        {
            _gameFactory = gameFactory;
            _canvas = canvas;
            Start();
        }

        private void Start()
        {
            var playerDeck = _gameFactory.SpawnDeck(new Vector3(Screen.width / 2f, 0));
            playerDeck.transform.SetParent(_canvas.transform);
            for (int i = 0; i < 6; i++)
            {
                _gameFactory.SpawnCard(playerDeck, CardId.Damage);
            }
            _gameFactory.SpawnCardMover(playerDeck);
        }
    }
}