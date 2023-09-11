using Infrastructure.Services.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Infrastructure.Logic
{
    public class ShootControl : MonoBehaviour, IPointerClickHandler
    {
        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService) =>
            _inputService = inputService;

        private void FixedUpdate() =>
            _inputService.SetFireTap(false);

        public void OnPointerClick(PointerEventData eventData) =>
            _inputService.SetFireTap(true);
    }
}