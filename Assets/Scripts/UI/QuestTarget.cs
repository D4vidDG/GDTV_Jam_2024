using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTarget : MonoBehaviour
{
    [SerializeField] GameObject indicator;

    public void ShowIndicator(bool enable)
    {
        indicator.SetActive(enable);
    }
}
