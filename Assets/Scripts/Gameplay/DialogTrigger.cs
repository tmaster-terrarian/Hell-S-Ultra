using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    public bool persistant;
    bool hasTriggered = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(!hasTriggered)
        {
            if(persistant)
            {
                hasTriggered = true;
            }

            DialogManager.StartDialog(dialog);
        }
    }
}
