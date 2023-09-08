using Infrastructure.Services.Input;
using Infrastructure.Services.StaticData;
using StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform _playerRoot;
        [SerializeField] private Transform _barrel;
        private IInputService _inputService;
        private IStaticDataService _staticDataService;

        private float _horizontalMovementSpeed;
        private float _minBarrelAngle;
        private float _maxBarrelAngle;

        [Inject]
        public void Construct(IInputService inputService, IStaticDataService staticDataService)
        {
            _inputService = inputService;
            PlayerStaticData playerData = staticDataService.GetPlayerData();

            InitializeHorizontalMovement(playerData);
            InitializeVerticalMovement(playerData);
        }

        private void InitializeHorizontalMovement(PlayerStaticData playerData) =>
            _horizontalMovementSpeed = playerData.RotationSpeed;

        private void InitializeVerticalMovement(PlayerStaticData playerData)
        {
            _minBarrelAngle = playerData.MinBarrelAngle;
            _maxBarrelAngle = playerData.MaxBarrelAngle;
        }

        private void Update()
        {
            ApplyVerticalAngle();
            ApplyHorizontalRotation();
        }

        private void ApplyVerticalAngle()
        {
            float verticalAngle =
                -Helpers.PercentageToLimitedRange(_inputService.VerticalPercentage, _minBarrelAngle, _maxBarrelAngle);
            _barrel.localRotation = Quaternion.Euler(verticalAngle, 0, 0);
        }

        private void ApplyHorizontalRotation()
        {
            float rotAngle = _inputService.XAxis * _horizontalMovementSpeed * Time.deltaTime;
            _playerRoot.AddRotation(rotAngle, _playerRoot.up);
        }
    }
}