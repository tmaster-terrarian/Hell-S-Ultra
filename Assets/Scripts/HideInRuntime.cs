using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInRuntime : MonoBehaviour
{
    void Awake()
    {
        SpriteRenderer sr;
        if(gameObject.TryGetComponent<SpriteRenderer>(out sr))
        {
            sr.enabled = false;
        }
    }
}
