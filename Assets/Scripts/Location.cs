using EnemiesSpawnPoints;
using UnityEngine;

public class Location : MonoBehaviour
{
    [SerializeField] private EntitiesContainer _enemiesContainer;
    [SerializeField] private EntitiesContainer _playersContainer;

    public EntitiesContainer EnemiesContainer => _enemiesContainer;
    public EntitiesContainer PlayersContainer => _playersContainer;
}