using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    [SerializeField] AudioClip _navigationClip, _actionClip;

    List<OptionObject> _optionList;

    int index;
    int maxIndex;

    void Awake()
    {
        index = 0;

        OptionListInit();

        maxIndex = _optionList.Count - 1;
    }

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        _optionList[0].OnStateChange(isSelected: true);
    }

    void OptionListInit() {
        _optionList = new List<OptionObject>();

        for (int i = 0; i < transform.childCount; i++) {
            OptionObject optionObject =
                transform.GetChild(i).GetComponent<OptionObject>();

            if (optionObject != null)
                _optionList.Add(optionObject);
        }
    }

    void Update() =>
        HandleInputs();

    void HandleInputs() {
        HandleActionInput();

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            HandleNavigationInput(KeyCode.UpArrow);
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            HandleNavigationInput(KeyCode.DownArrow);

        _optionList[index].OnStateChange(isSelected: true);
    }

    void HandleNavigationInput(KeyCode _currentKey) {
        AudioManager.PlayAudio(_navigationClip);

        _optionList[index].OnStateChange(isSelected: false);

        if (_currentKey == KeyCode.DownArrow || _currentKey == KeyCode.W) 
            index = index == maxIndex ? 0 : index + 1;
        else
            index = index == 0 ? maxIndex : index - 1;
    }

    void HandleActionInput() {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            AudioManager.PlayAudio(_actionClip);

            _optionList[index].OnActionFired();
        }
    }
}
