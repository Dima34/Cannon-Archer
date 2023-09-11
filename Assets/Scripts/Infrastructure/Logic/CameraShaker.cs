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


        // Variables for controlling the camera shake
        public float shakeDuration = 0.5f; // Duration of the shake
        public float shakeAmount = 0.2f; // Amount of camera shake

        private Vector3 originalPosition;
        private IInputService _inputService;
        private Coroutine _shakeCoroutine;

        [Inject]
        public void Construct(IInputService inputService) =>
            _inputService = inputService;

        void Start()
        {
            originalPosition = _camera.transform.localPosition;
        }

        private void Update()
        {
            if (_inputService.OnFireTap && ShakeNotPlaying())
                Shake();
        }

        private bool ShakeNotPlaying() =>
            _shakeCoroutine == null;

        private void Shake()
        {
            _shakeCoroutine = StartCoroutine(ShakeCoroutine());
        }

        private IEnumerator ShakeCoroutine()
        {
            float elapsed = 0f;

            while (elapsed < shakeDuration)
            {
                Vector3 shakeOffset = Random.insideUnitSphere * shakeAmount;

                _camera.transform.localPosition = originalPosition + shakeOffset;

                elapsed += Time.deltaTime;

                yield return null;
            }

            _camera.transform.localPosition = originalPosition;
            _shakeCoroutine = null;
        }
    }
}