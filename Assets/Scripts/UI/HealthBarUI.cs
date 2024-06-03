using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField][Range(0f, 100f)] float minPercentageToBlink;
    [SerializeField] float hearthSpeedIncrement;

    BlinkColor blinkEffect;
    Health playerHealth;
    UIBar bar;
    Animator hearthAnimator;

    private void Awake()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        bar = GetComponentInChildren<UIBar>();
        blinkEffect = bar.GetComponentInChildren<BlinkColor>();
        hearthAnimator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        playerHealth.OnAttacked.AddListener(UpdateBar);
        playerHealth.OnHealed.AddListener(UpdateBar);
    }

    private void OnDisable()
    {
        playerHealth.OnAttacked.RemoveListener(UpdateBar);
        playerHealth.OnHealed.RemoveListener(UpdateBar);
    }

    void UpdateBar(float damage)
    {
        float healthPercentage = playerHealth.GetHealthPercentage();
        bar.SetFraction(healthPercentage / 100f);
        if (healthPercentage < minPercentageToBlink)
        {
            blinkEffect.Trigger();
            hearthAnimator.speed = hearthSpeedIncrement;
        }
        else
        {
            hearthAnimator.speed = 1;
            blinkEffect.Stop();
        }
    }
}
