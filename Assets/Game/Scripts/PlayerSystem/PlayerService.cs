using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.StorageSystem;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PlayerSystem
{
    public class PlayerService : IPlayerService
    {
        private static readonly TimeSpan _autoSaveInterval = TimeSpan.FromSeconds(30);
        
        private const string PLAYER_KEY = "playerSaveData";
        private readonly IStorageService _storageService;
        private PlayerSaveData _playerSaveData;
       
        [Inject]
        public PlayerService(IStorageService storageService)
        {
            _storageService = storageService;
        }
        
        public async UniTask Initialize(CancellationToken token)
        {
            _playerSaveData = await _storageService.Load<PlayerSaveData>(PLAYER_KEY) ?? new PlayerSaveData();
            AutoSaveRoutine(token).Forget();
        }


        public T GetData<T>(Func<PlayerSaveData, T> selector)
        {
            return selector.Invoke(_playerSaveData);
        }
        
        public void SetData(Action<PlayerSaveData> action)
        {
            action.Invoke(_playerSaveData);
        }
        
        private async UniTask Save()
        {
            if (_playerSaveData != null)
            {
                //This file may be massive for client-backend ping-pong. We can consider make "changed" flags and send only changed parts of the file
                await _storageService.Save(PLAYER_KEY, _playerSaveData.DeepClone());
            }
        }

        private async UniTask AutoSaveRoutine(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.Delay(_autoSaveInterval, cancellationToken: token);
                await Save();
            }
        }
        
        public void OnApplicationQuit()
        {
            Save().Forget();
        }

        public void OnApplicationFocus(bool hasFocus)
        {
            Save().Forget();
        }
    }
}