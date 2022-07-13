using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTest : MonoBehaviour
{
    public Dialog dialog;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            DialogManager.StartDialog(dialog);
        }
    }
}
