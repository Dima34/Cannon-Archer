using Infrastructure.Logic;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGameFactory
    {
        GameObject CreatePlayer();
        GameObject CreateMap();
        Bullet CreateBullet();
    }
}