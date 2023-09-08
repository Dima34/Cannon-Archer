using UnityEngine;

namespace Infrastructure.Logic
{
    public class Bullet : MonoBehaviour
    {
        private float _startTime;
        private Vector3 _startPosition;
        private float _speed;

        public void Initialize(Vector3 pos, Quaternion rot,float speed)
        {
            transform.position = pos;
            transform.rotation = rot;
            _speed = speed;
        }

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