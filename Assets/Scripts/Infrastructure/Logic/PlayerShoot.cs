using System;
using Infrastructure.Factories;
using Infrastructure.Services.Input;
using Infrastructure.Services.StaticData;
using StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        private IInputService _inputService;
        private IGameFactory _gameFactory;
        private float _minBulletSpeed;
        private float _maxBulletSpeed;
        Vector3[] trajectoryPoints = new Vector3[TRAJECTORY_POINTS_AMOUNT];
        private PlayerStaticData _playerData;

        private const int TRAJECTORY_POINTS_AMOUNT = 100;

        [Inject]
        public void Construct(IInputService inputService, IGameFactory gameFactory,
            IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;
            _inputService = inputService;
            _playerData = staticDataService.GetPlayerData();
        }

        private void Start()
        {
            _minBulletSpeed = _playerData.MinBulletSpeed;
            _maxBulletSpeed = _playerData.MaxBulletSpeed;
        }

        private void Update()
        {
            RenderTrajectory();

            if (IsFireTapped())
                Shoot();
        }

        private void RenderTrajectory()
        {
            _lineRenderer.positionCount = trajectoryPoints.Length;
            Vector3 startPosition = transform.position;
            
            for (int i = 0; i < trajectoryPoints.Length; i++)
            {
                float time = i * 0.1f;
                trajectoryPoints[i] = BasicPhysics.BallisticTrajectory(startPosition, transform.forward, time, GetSpeed());

                if (TrajectoryUnderGround(i))
                {
                    _lineRenderer.positionCount = i + 1;
                    break;
                }
            }
            
            _lineRenderer.SetPositions(trajectoryPoints);
        }

        private float GetSpeed() =>
            Helpers.PercentageToLimitedRange(_inputService.VerticalPercentage, _minBulletSpeed, _maxBulletSpeed);

        private bool TrajectoryUnderGround(int i) =>
            trajectoryPoints[i]. y < 0;

        private bool IsFireTapped() =>
            _inputService.IsFireTap;

        private void Shoot()
        {
            Bullet bullet = _gameFactory.CreateBullet();
            bullet.Initialize(transform.position, transform.rotation, GetSpeed());
        }
    }
}