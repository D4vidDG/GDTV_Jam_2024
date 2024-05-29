using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Coin : MonoBehaviour
{
    [SerializeField] float maxLifetime;
    [SerializeField] float timeToBlink;

    public AudioClip clip;
    BlinkAlpha blinkEffect;
    float timer;

    public static Action OnCoinCollected;

    private void Awake()
    {
        blinkEffect = GetComponent<BlinkAlpha>();
    }

    private void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (maxLifetime < timer)
        {
            blinkEffect.Stop();
            Destroy(this.gameObject);
        }

        if (timeToBlink < timer)
        {
            blinkEffect.Trigger();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
            OnCoinCollected?.Invoke();
            Destroy(this.gameObject);
        }
    }

}
