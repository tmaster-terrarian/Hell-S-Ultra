using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aarthificial.Reanimation;

public class BenboAnim : MonoBehaviour
{
    Reanimator reanimator;

    void Start()
    {
        reanimator = GetComponent<Reanimator>();
        reanimator.enabled = true;
    }
}
