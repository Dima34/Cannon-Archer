using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Infrastructure.Logic
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _shakeDuration = 0.5f;
        [SerializeField] private float _shakeAmount = 0.2f;

        private Vector3 _originalPosition;
        private IInputService _inputService;
        private Coroutine _shakeCoroutine;

        [Inject]
        public void Construct(IInputService inputService) =>
            _inputService = inputService;

        void Start()
        {
            _originalPosition = _camera.transform.localPosition;
        }

        private void Update()
        {
            if (_inputService.IsFireTap && ShakeNotPlaying())
                Shake();
        }

        private bool ShakeNotPlaying() =>
            _shakeCoroutine == null;

        private void Shake() =>
            _shakeCoroutine = StartCoroutine(ShakeCoroutine());

        private IEnumerator ShakeCoroutine()
        {
            float elapsed = 0f;

            while (elapsed < _shakeDuration)
            {
                Vector3 shakeOffset = Random.insideUnitSphere * _shakeAmount;

                _camera.transform.localPosition = _originalPosition + shakeOffset;

                elapsed += Time.deltaTime;

                yield return null;
            }

            _camera.transform.localPosition = _originalPosition;
            _shakeCoroutine = null;
        }
    }
}