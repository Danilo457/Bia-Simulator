using UnityEngine;

public class OptionBehaviourCursor : MonoBehaviour, IOptionBehaviour
{
    [SerializeField] RectTransform _cursor;

    [SerializeField] Vector2 _anchoredPosition;

    public void OnStateChange(bool state)
    {
        if (state) {
            _cursor.transform.SetParent(transform, true);

            _cursor.anchoredPosition = _anchoredPosition;
        }
    }
}
