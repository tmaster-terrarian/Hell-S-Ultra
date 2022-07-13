using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathText : MonoBehaviour
{
    static GameObject go;

    void Start()
    {
        go = gameObject;
        go.SetActive(false);
    }

    public static void OnPlayerDie()
    {
        Debug.Log("Player died");
        go.SetActive(true);
    }
}
