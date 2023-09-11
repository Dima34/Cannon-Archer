using System.Collections;
using Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _maxSpeedDebuffPerc;
        [SerializeField] private int _minSpeedDebuffPerc;
        [SerializeField] private int _maxCollisionsCount;
        [SerializeField] private float _lifeTime;
        
        private float _startTime;
        private Vector3 _startPosition;
        private Vector3 _prevPos;
        private float _speed;
        private int _collisionsCount;
        private IGameFactory _gameFactory;
        private Coroutine _selfDestroyCoroutine;

        [Inject]
        public void Construct(IGameFactory gameFactory) =>
            _gameFactory = gameFactory;

        public void Initialize(Vector3 pos, Quaternion rot, float speed)
        {
            transform.position = pos;
            transform.rotation = rot;
            _speed = speed;
        }

        private void Start()
        {
            ResetStartTimeAndPosition();
            StartDestroySelfProcess();
        }

        private void StartDestroySelfProcess()
        {
            _selfDestroyCoroutine = StartCoroutine(DelayedSelfDestroy());
        }

        private void ResetStartTimeAndPosition()
        {
            _startTime = Time.time;
            _startPosition = transform.position;
        }

        private void Update()
        {
            _prevPos = transform.position;

            float time = Time.time - _startTime;
            transform.position = BasicPhysics.BallisticTrajectory(_startPosition, transform.forward, time, _speed);
        }

        private void OnTriggerEnter(Collider collidedObj)
        {
            Vector3 flyDirection = GetFlyDirection();
            Ricochet(collidedObj, flyDirection);
        }

        private void Ricochet(Collider collidedObj, Vector3 inDirecion)
        {
            Vector3 collidedObjNormal = collidedObj.transform.up;
            
            if (_collisionsCount >= _maxCollisionsCount) 
                DestroySelf(-collidedObjNormal);

            Vector3 reflectedVector = Vector3.Reflect( -inDirecion, collidedObjNormal);

            ResetStartTimeAndPosition();
            transform.forward = reflectedVector;
            
            float shootAngle = Vector3.Angle(-collidedObj.transform.up, inDirecion);
            float speedDebuffPercentage = Helpers.LinearTransform(shootAngle, 1, _maxSpeedDebuffPerc, 180, _minSpeedDebuffPerc);
            _speed = _speed - (_speed / 100 * speedDebuffPercentage);
            
            _collisionsCount++;
            
            Debug.DrawRay(transform.position, collidedObjNormal, Color.blue, 5);
            Debug.DrawRay(transform.position, reflectedVector, Color.green, 5);
            Debug.DrawRay(transform.position, inDirecion, Color.red, 5);
            Debug.Log($"Angle {shootAngle}");
            Debug.Log($"Debuff {speedDebuffPercentage}");
        }

        private void DestroySelf(Vector3 surfaceNormalDirecion)
        {
            if (_selfDestroyCoroutine != null)
            {
                StopCoroutine(_selfDestroyCoroutine);
                _selfDestroyCoroutine = null;
            }
            
            GameObject explosion = _gameFactory.CreateExplosion();
            explosion.transform.position = transform.position;
            explosion.transform.up = surfaceNormalDirecion;
            Destroy(gameObject);
        }

        private Vector3 GetFlyDirection() =>
            _prevPos - transform.position;

        private IEnumerator DelayedSelfDestroy()
        {
            yield return new WaitForSecondsRealtime(_lifeTime);
            DestroySelf(Vector3.zero);

            _selfDestroyCoroutine = null;
        }
    }
}