using Infrastructure.Factories;
using Infrastructure.Services.Input;
using Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        private IInputService _inputService;
        private IGameFactory _gameFactory;
        private float _speed;
        Vector3[] trajectoryPoints = new Vector3[TRAJECTORY_POINTS_AMOUNT];
        
        private const int TRAJECTORY_POINTS_AMOUNT = 100;

        [Inject]
        public void Construct(IInputService inputService, IGameFactory gameFactory,
            IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;
            _inputService = inputService;
            _speed = staticDataService.GetPlayerData().BulletSpeed;
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
            Vector3 startPosition = transform.position + transform.forward;
            
            for (int i = 0; i < trajectoryPoints.Length; i++)
            {
                float time = i * 0.1f;
                trajectoryPoints[i] = BasicPhysics.BallisticTrajectory(startPosition, transform.forward, time, _speed);

                if (TrajectoryUnderGround(i))
                {
                    _lineRenderer.positionCount = i + 1;
                    break;
                }
            }
            
            _lineRenderer.SetPositions(trajectoryPoints);
        }

        private bool TrajectoryUnderGround(int i) =>
            trajectoryPoints[i]. y < 0;

        private bool IsFireTapped() =>
            _inputService.OnFireTap;

        private void Shoot()
        {
            Bullet bullet = _gameFactory.CreateBullet();
            bullet.transform.position = transform.position + transform.forward;
            bullet.transform.rotation = transform.rotation;
        }
    }
}