using Infrastructure.Constants;
using Infrastructure.Logic;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private DiContainer _container;

        public GameFactory(DiContainer container)
        {
            _container = container;
        }

        public GameObject CreatePlayer() =>
            _container.InstantiatePrefabResource(ResourcePaths.PLAYER);
        
        public GameObject CreateMap() =>
            _container.InstantiatePrefabResource(ResourcePaths.MAP);

        public Bullet CreateBullet() =>
            _container.InstantiatePrefabResource(ResourcePaths.BULLET).GetComponent<Bullet>();
    }
}