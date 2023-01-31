using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Dialogue
{
    public Talker _talker;

    [TextArea(5, 10)] public string[] messages;
}
