using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class WobbleEffect : MonoBehaviour
{
    public Material _wobbleEffectMaterial;
    private bool  _wobbleEffectActive = false;
    private float _frequency = 4f;
    private float _shift = 0f;
    private float _amplitude = 0f;
    private float _maxAmplitude = 0.05f;
    private float _shiftSpeed = 5f;
    private float _amplitudeSpeed = 0.025f;
    private float elapsedTime = 0f;
    private const float wobbleDuration = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _wobbleEffectMaterial);
    }

    private void SetFrequency(float frequency)
    {
        _wobbleEffectMaterial.SetFloat("_frequency", frequency);
    }

    private void SetShift(float shift)
    {
        _wobbleEffectMaterial.SetFloat("_shift", shift);
    }

    private void SetAmplitude(float amplitude)
    {
        _wobbleEffectMaterial.SetFloat("_amplitude", amplitude);
    }

    public void StartWobble()
    {
        if (!_wobbleEffectActive)
        {
            _wobbleEffectActive = true;
            StartCoroutine(WobbleCoroutine());
        }
    }

    public void StopWobble()
    {
        _wobbleEffectActive = false;
    }

    private IEnumerator WobbleCoroutine()
    {
        
        SetFrequency(_frequency);

        while (_amplitude < _maxAmplitude)
        {
            if (_wobbleEffectActive)
            {
                SetAmplitude(_amplitude);
                SetShift(_shift);

                _amplitude += _amplitudeSpeed * Time.deltaTime;
                _shift += _shiftSpeed * Time.deltaTime;
                _shift %= Mathf.PI * 2f;

                elapsedTime += Time.deltaTime;
                Debug.Log(elapsedTime);

                yield return null;
            }
            else
            {
                break;
            }
        }
        
        if (_wobbleEffectActive)
        {
            _amplitude = _maxAmplitude;
            SetAmplitude(_amplitude);
        }

        while (_wobbleEffectActive)
        {
            SetShift(_shift);
            _shift += _shiftSpeed * Time.deltaTime;
            _shift %= Mathf.PI * 2f;

            elapsedTime += Time.deltaTime;
            Debug.Log(elapsedTime);
            yield return null;
        }

        while (_amplitude > 0f)
        {
            if (!_wobbleEffectActive)
            {
                SetAmplitude(_amplitude);
                SetShift(_shift);

                _amplitude -= _amplitudeSpeed * Time.deltaTime;
                _shift += _shiftSpeed * Time.deltaTime;
                _shift %= Mathf.PI * 2f;

                yield return null;
            }
            else
            {
                break;
            }
        }

        if (!_wobbleEffectActive && elapsedTime > wobbleDuration)
        {
            _shift = 0f;
            _amplitude = 0f;
            SetAmplitude(_amplitude);
            SetShift(_shift);
            elapsedTime = 0f;

            enabled = false;
        }
    }
}
