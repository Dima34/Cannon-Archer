using UnityEngine;

namespace Infrastructure.Logic
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _maxSpeedDebuffPerc;
        [SerializeField] private int _minSpeedDebuffPerc;
        [SerializeField] private int _ricochetAngle;
        [SerializeField] private int _maxCollisionsCount;
        [SerializeField] private float _lifeTime;
        
        private float _startTime;
        private Vector3 _startPosition;
        private Vector3 _prevPos;
        private float _speed;
        private int _collisionsCount;

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

        private void StartDestroySelfProcess() =>
            Destroy(gameObject, _lifeTime);

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
            if (_collisionsCount >= _maxCollisionsCount) 
                DestroySelf();

            Vector3 collidedObjNormal = collidedObj.transform.up;
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

        private void DestroySelf() =>
            Destroy(gameObject);

        private bool IsRicochet(float hitAngle) =>
            hitAngle > _ricochetAngle;

        private Vector3 GetFlyDirection() =>
            _prevPos - transform.position;
    }
}