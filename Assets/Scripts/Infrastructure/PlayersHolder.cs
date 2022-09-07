using System.Collections.Generic;
using System.Linq;
using EnemiesSpawnPoints;
using Entities;
using Infrastructure.Factories;
using Infrastructure.SaveLoad;
using Infrastructure.StaticData.Cards;
using UnityEngine;

namespace Infrastructure
{
    public class PlayersHolder : EntitiesHolder<Player>, IPlayersHolder
    {
        private readonly LocationHolder _locationHolder;
        private readonly IGameFactory _gameFactory;

        public List<Player> Players => Entities;

        protected override EntitiesContainer EntitiesContainer => _locationHolder.Location.PlayersContainer;

        public PlayersHolder(LocationHolder locationHolder, IGameFactory gameFactory)
        {
            _locationHolder = locationHolder;
            _gameFactory = gameFactory;
        }

        public void Save(ISaveLoadService saveLoadService)
        {
            var playersData = Players.Select(x => x.GetData());
            saveLoadService.SetValue(playersData, SaveLoadKey.PlayersBattleGroup);
        }

        public void Load(ISaveLoadService saveLoadService)
        {
            Entities.ForEach(x=>Object.Destroy(x.gameObject));
            
            var defaultValue = new List<PlayerData>
            {
                new PlayerData()
                {
                    Name = "4eJ1",
                    Cards = new List<CardId>()
                    {
                        CardId.WarriorAttack,
                        CardId.WarriorAttack,
                        CardId.WarriorAttack,
                        CardId.WarriorDefense,
                        CardId.WarriorDefense,
                        CardId.WarriorDefense,
                        CardId.WarriorAoe,
                        CardId.WarriorEating,
                        CardId.WarriorSalo,
                        CardId.WarriorValor,
                        CardId.WarriorDrinkBeer,
                        CardId.WarriorMegaAttack,
                        CardId.WarriorDamageLikeDefense,
                    },
                    MaxHealth = 40,
                    Health = 40,
                    MaxEnergy = 3,
                    Initiative = 0,
                },
                new PlayerData()
                {
                    Name = "XyU",
                    Cards = new List<CardId>()
                    {
                        CardId.WarriorAttack,
                        CardId.WarriorAttack,
                        CardId.WarriorAttack,
                        CardId.WarriorDefense,
                        CardId.WarriorDefense,
                        CardId.WarriorDefense,
                        CardId.WarriorAoe,
                        CardId.WarriorEating,
                        CardId.WarriorSalo,
                        CardId.WarriorValor,
                        CardId.WarriorDrinkBeer,
                        CardId.WarriorMegaAttack,
                        CardId.WarriorDamageLikeDefense,
                    },
                    MaxHealth = 40,
                    Health = 40,
                    MaxEnergy = 3,
                    Initiative = 0,
                }
            };
            var playersData = saveLoadService.GetValue(SaveLoadKey.PlayersBattleGroup, defaultValue);
            foreach (PlayerData playerData in playersData)
            {
                Player newPlayer = _gameFactory.SpawnPlayer(playerData);
                Add(newPlayer);
            }
        }
    }
}