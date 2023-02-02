using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class OptionObject : MonoBehaviour
{
    [SerializeField] UnityEvent<bool> _onStateChange;
    [SerializeField] UnityEvent _onActionFired;

    public void OnStateChange(bool isSelected)
    {
        _onStateChange?.Invoke(isSelected);
    }

    public void OnActionFired()
    {
        _onActionFired?.Invoke();
    }
}
