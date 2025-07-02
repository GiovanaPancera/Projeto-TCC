using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulsar : MonoBehaviour
{
    public float pulseScale = 1.2f;  // O tamanho máximo do pulso
    public float pulseSpeed = 1f;    // Velocidade do pulso
    private Vector3 originalScale;
    public bool isPulsing = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPulsing = true;
            StartCoroutine(PulseEffect());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPulsing = false;
            StopAllCoroutines();
            transform.localScale = originalScale;  // Reseta para o tamanho original
        }
    }

    System.Collections.IEnumerator PulseEffect()
    {
        while (isPulsing)
        {
            float timer = 0f;
            while (timer < 1f && isPulsing)
            {
                transform.localScale = Vector3.Lerp(originalScale, originalScale * pulseScale, Mathf.PingPong(timer * pulseSpeed, 1));
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}

