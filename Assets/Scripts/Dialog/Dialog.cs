#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog/New Dialog")]
public class Dialog : ScriptableObject
{
    [SerializeField] private DialogLine[] lines;
    
    public DialogLine GetLine(int index)
    {
        return lines[index];
    }

    public int GetLength0()
    {
        return lines.Length - 1;
    }

    public int GetLength1()
    {
        return lines.Length;
    }
}
