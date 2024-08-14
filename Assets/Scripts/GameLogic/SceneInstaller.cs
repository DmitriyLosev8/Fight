using UnityEngine;
using Assets.Scripts.EnemyComponents;
using Assets.Scripts.PlayerComponents;

namespace Assets.Scripts.GameLogic
{
    internal class SceneInstaller : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _spawner;
        [SerializeField] private Player _player;
        [SerializeField] private GlobalUI _globalUI;

        private void OnEnable()
        {
            _player.TakeGUI(_globalUI);
            _spawner.TakeGUI(_globalUI);
            _spawner.EnemyCreated += _player.TakeEnemy;
            _spawner.EnemyDied += _player.OnSpawning;
            _player.PlayersIndicators.Lost += _spawner.StopFight;
            _player.ReadyToSpawn += _spawner.Spawn;
            _globalUI.CancelButtonClicked += _spawner.StopFight;
        }

        private void OnDisable()
        {
            _spawner.EnemyCreated -= _player.TakeEnemy;
            _spawner.EnemyDied -= _player.OnSpawning;
            _player.ReadyToSpawn -= _spawner.Spawn;
            _globalUI.CancelButtonClicked -= _spawner.StopFight;
            _player.PlayersIndicators.Lost -= _spawner.StopFight;
        }
    }
}