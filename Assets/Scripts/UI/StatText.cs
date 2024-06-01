using TMPro;
using UnityEngine;
public class StatText : MonoBehaviour
{
    public WeaponStat stat;
    [SerializeField] TextMeshProUGUI title;
    public TextMeshProUGUI value;
    public TextMeshProUGUI diff;

    private void Start()
    {
        title.text = stat.ToString() + ":";
    }
}