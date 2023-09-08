using Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic
{
    public class Bullet : MonoBehaviour
    {
        private float _speed;
        private float _startTime;
        private Vector3 _startPosition;

        [Inject]
        public void Construct(IStaticDataService staticDataService) =>
            _speed = staticDataService.GetPlayerData().BulletSpeed;

        private void Start()
        {
            _startTime = Time.time;
            _startPosition = transform.position;
        }

        private void Update()
        {
            float time = Time.time - _startTime;
            transform.position = BasicPhysics.BallisticTrajectory(_startPosition,transform.forward, time, _speed); 
        }
    }
}