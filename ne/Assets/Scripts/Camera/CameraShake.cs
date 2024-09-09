using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;  // Kameranýn Transform bileþeni
    public float shakeDuration = 0f;   // Shake süresi
    public float shakeMagnitude = 0.1f; // Shake þiddeti
    public float dampingSpeed = 1.0f;   // Shake'in yavaþlama hýzý
    public float shakeTimer;

    private Vector3 initialPosition;    // Kameranýn ilk pozisyonu

    private void OnEnable()
    {
        initialPosition = cameraTransform.localPosition;  // Baþlangýç pozisyonunu kaydet
    }

    private void Update()
    {
        if (shakeDuration > 0) 
        {
            cameraTransform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else 
        {
            shakeDuration = 0f;
            cameraTransform.localPosition = initialPosition;
        }
    }

    // Kamera shake fonksiyonu
    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}