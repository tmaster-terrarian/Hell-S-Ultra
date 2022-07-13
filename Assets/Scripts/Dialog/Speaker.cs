#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Speaker", menuName = "Dialog/New Speaker")]
public class Speaker : ScriptableObject
{
    [SerializeField] private string speakerName = "???";
    [SerializeField] private Sprite speakerPortrait;
    [SerializeField] private Color speakerColor;
    [SerializeField] private bool isAbstract;

    public string GetName()
    {
        return speakerName;
    }

    public Sprite GetPortrait()
    {
        return speakerPortrait;
    }

    public Color GetColor()
    {
        return speakerColor;
    }

    public bool IsAbstract()
    {
        return isAbstract;
    }
}
