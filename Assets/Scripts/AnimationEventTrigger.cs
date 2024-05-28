using System;
using UnityEngine;

public class AnimationEventTrigger : MonoBehaviour
{
    public Action OnEventTriggered;

    public void TriggerEvent()
    {
        OnEventTriggered?.Invoke();
    }
}
