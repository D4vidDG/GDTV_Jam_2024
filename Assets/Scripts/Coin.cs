using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float maxLifetime;
    [SerializeField] float timeToBlink;

    BlinkAlpha blinkEffect;
    float timer;

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

}