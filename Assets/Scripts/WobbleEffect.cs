using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

/// <summary>
/// This script is used to start the coroutine of the wobble effect 
/// when player eats or touches the poison mushroom.
/// </summary>
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

        float startTime = Time.time;

        // Start up wobble
        while (_amplitude < _maxAmplitude)
        {
            if (_wobbleEffectActive && Time.time - startTime < wobbleDuration)
            {
                SetAmplitude(_amplitude);
                SetShift(_shift);

                _amplitude += _amplitudeSpeed * Time.deltaTime;
                _shift += _shiftSpeed * Time.deltaTime;
                _shift %= Mathf.PI * 2f;

                Debug.Log(elapsedTime);

                yield return null;
            }
            else
            {
                break;
            }
        }
        
        // Maintaining wobble effect
        if (_wobbleEffectActive)
        {
            _amplitude = _maxAmplitude;
            SetAmplitude(_amplitude);
        }

        while (_wobbleEffectActive && Time.time - startTime < wobbleDuration)
        {
            SetShift(_shift);
            _shift += _shiftSpeed * Time.deltaTime;
            _shift %= Mathf.PI * 2f;

            Debug.Log(elapsedTime);
            yield return null;
        }

        while (_amplitude > 0f)
        {
            if (!_wobbleEffectActive || Time.time - startTime > wobbleDuration)
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

        // if (!_wobbleEffectActive && elapsedTime > wobbleDuration)
        // {
        //     _shift = 0f;
        //     _amplitude = 0f;
        //     SetAmplitude(_amplitude);
        //     SetShift(_shift);
        //     elapsedTime = 0f;

        //     enabled = false;
        // }

        _shift = 0;
        _amplitude = 0f;
        SetAmplitude(_amplitude);
        SetShift(_shift);

        _wobbleEffectActive = false;
        enabled = false;
    }

    // Reset the variables when the editor halts the excution.
    private void OnDisable()
    {
        _amplitude = 0f;
        _shift = 0f;

        SetAmplitude(_amplitude);
        SetShift(_shift);
    }
}
