using Infrastructure.Services.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Infrastructure.Logic
{
    public class PowerControl :MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _percentageLabel;
        
        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService) =>
            _inputService = inputService;

        private void Start()
        {
            SetSliderPercentage();

            _slider.onValueChanged.AddListener(SetPercentage);
        }

        private void SetSliderPercentage() =>
            SetPercentage(_slider.value);

        private void SetPercentage(float percentage)
        {
            SetInputPercentage(percentage);
            SetPercentageLabelValue(percentage);
        }

        private void SetInputPercentage(float percentage) =>
            _inputService.SetVerticalPercentage(percentage);

        private void SetPercentageLabelValue(float percentage) =>
            _percentageLabel.text = percentage.ToString();
    }
}