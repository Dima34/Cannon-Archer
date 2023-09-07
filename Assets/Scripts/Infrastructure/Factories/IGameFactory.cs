using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGameFactory
    {
        GameObject CreatePlayer();
        GameObject CreateMap();
    }
}