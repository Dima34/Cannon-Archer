using System.Collections;
using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

public class CannonRecoilAnim : MonoBehaviour
{
    [SerializeField] private Vector3 _moveDistance;
    [SerializeField] private float _speed;
    private Vector3 _startPos;
    private IInputService _inputService;
    private Coroutine _animCoroutine;

    [Inject]
    public void Construct(IInputService inputService) =>
        _inputService = inputService;

    private void Update()
    {
        if (_inputService.OnFireTap && AnimNotRunning()) 
            _animCoroutine = StartCoroutine(Recoil());
    }

    private bool AnimNotRunning() =>
        _animCoroutine == null;

    IEnumerator Recoil()
    {
        _startPos = transform.localPosition;
        Vector3 endPos = transform.localPosition + _moveDistance;

        float timeElapsed = 0;
        while (timeElapsed <= 1)
        {
            timeElapsed += _speed * Time.deltaTime;
            transform.localPosition = Vector3.Lerp(_startPos, endPos, timeElapsed);
            yield return null;
        }
        
        while (timeElapsed >= 0)
        {
            timeElapsed -= _speed * Time.deltaTime;
            transform.localPosition = Vector3.Lerp(_startPos, endPos, timeElapsed);
            yield return null;
        }

        _animCoroutine = null;
    }
}