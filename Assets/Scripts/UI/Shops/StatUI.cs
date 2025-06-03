using TMPro;
using UnityEngine;
public class StatUI : MonoBehaviour
{

    public WeaponStat targetStat;
    [SerializeField] TextMeshProUGUI title;
    public TextMeshProUGUI value;
    public TextMeshProUGUI increase;

    private void Start()
    {
        title.text = targetStat.ToString() + ":";
    }
}