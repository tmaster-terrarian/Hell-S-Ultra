using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSi.Utility;

public class Room : MonoBehaviour
{
    public string roomName = "room0";
    public string music = "";

    GameObject player;

    void Start()
    {
        player = GameManager.GetPlayer();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 8)
        {
            if(music != "")
            {
                SFX.PlayMus(music);
            }
        }
    }
}
