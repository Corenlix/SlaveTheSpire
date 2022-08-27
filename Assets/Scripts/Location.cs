using UnityEngine;

public class Location : MonoBehaviour
{
    [SerializeField] private EnemiesContainer _enemiesContainer;
    [SerializeField] private Transform _playerSpawnPoint;

    public EnemiesContainer EnemiesContainer => _enemiesContainer;
    public Transform PlayerSpawnPoint => _playerSpawnPoint;
}