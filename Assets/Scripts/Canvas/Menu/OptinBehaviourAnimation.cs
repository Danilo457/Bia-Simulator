using UnityEngine;

public class OptinBehaviourAnimation : MonoBehaviour, IOptionBehaviour
{
    [SerializeField] Animator _animator;

    const string KEY_STATE_ANIMATOR = "static";

    public void OnStateChange(bool state)
    {
        Time.timeScale = 1; // UnPause

        _animator.SetBool(KEY_STATE_ANIMATOR, !state);
    }
}
