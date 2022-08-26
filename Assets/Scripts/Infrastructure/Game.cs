namespace Infrastructure
{
    public class Game
    {
        private readonly IGameFactory _gameFactory;

        public Game(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            Start();
        }

        private void Start()
        {
            var uiContainer = _gameFactory.SpawnUIContainer();
            var playerDeck = uiContainer.PlayerDeck;
            var canvas = uiContainer.Canvas;
            playerDeck.transform.SetParent(canvas.transform);
            var targetSelectorsPool = _gameFactory.SpawnCardTargetSelectorsPool();
            targetSelectorsPool.transform.SetParent(canvas.transform);
            for (int i = 0; i < 6; i++)
            {
                _gameFactory.SpawnCard(playerDeck, CardId.Damage);
            }
            var cardMover = _gameFactory.SpawnCardMover(playerDeck);
            cardMover.Init(targetSelectorsPool);
        }
    }
}