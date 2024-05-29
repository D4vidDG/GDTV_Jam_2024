using UnityEngine;
using UnityEngine.UI;
using ExtensionMethods;

public class ReloadBar : MonoBehaviour
{
    [SerializeField] GameObject bar;
    [SerializeField] float sliderStartValue;
    [SerializeField] float sliderEndValue;

    PlayerController playerController;
    Slider slider;

    private void Awake()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        slider = GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        slider.value = sliderStartValue;
        bar.SetActive(false);
    }

    private void Update()
    {
        Weapon currentWeapon = playerController.currentWeapon;
        if (currentWeapon.IsReloading())
        {
            bar.SetActive(true);
            float reloadFraction = currentWeapon.GetReloadPercentage() / 100f;
            float sliderValue = MathExtensions.Remap(reloadFraction, 0f, 1f, sliderStartValue, sliderEndValue);
            slider.value = sliderValue;
        }
        else
        {
            slider.value = sliderStartValue;
            bar.SetActive(false);
        }
    }

}
