using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogLine
{
    public Speaker speaker;
    [TextArea] public string dialog;
}
