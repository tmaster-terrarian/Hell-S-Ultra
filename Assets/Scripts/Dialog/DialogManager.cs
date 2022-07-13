using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text speakerName;
    public Text dialog;
    public Image speakerPortrait;
    public Text abstractDialog;
    public GameObject standardDialog;

    int currentIndex;
    Dialog currentDialog;
    static DialogManager instance;
    Animator animator;
    Image dialogBox;
    static PlayerController player;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            animator = gameObject.GetComponent<Animator>();
            dialogBox = gameObject.GetComponent<Image>();
        }

        player = GameManager.GetPlayer().GetComponent<PlayerController>();
    }

    void Update()
    {
        if(currentDialog != null)
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                ReadNext();
            }
        }
    }

    public static void StartDialog(Dialog dialog)
    {
        instance.animator.SetBool("Open", true);
        player.canInput = false;
        instance.currentIndex = 0;
        instance.currentDialog = dialog;
        instance.speakerName.text = "";
        instance.dialog.text = "";
        instance.abstractDialog.text = "";
        instance.dialogBox.color = new Color(1, 1, 1);

        instance.ReadNext();
    }

    public void ReadNext(float delayMult = 1)
    {
        if(currentIndex > currentDialog.GetLength0())
        {
            instance.animator.SetBool("Open", false);
            currentDialog = null;
            player.canInput = true;
            return;
        }
        DialogLine line = currentDialog.GetLine(currentIndex);
        speakerName.text = line.speaker.GetName();
        speakerPortrait.sprite = line.speaker.GetPortrait();
        // dialog.text = line.dialog;
        dialogBox.color = line.speaker.GetColor();
        if(line.speaker.IsAbstract())
        {
            standardDialog.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100);
            instance.StartCoroutine(TypeTextAbstract(line.dialog, delayMult));
        }
        else
        {
            standardDialog.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            instance.StartCoroutine(TypeTextStandard(line.dialog, delayMult));
        }
        currentIndex++;
    }

    IEnumerator TypeTextStandard(string text, float delayMult = 1)
    {
        dialog.text = "";
        bool complete = false;
        int i = 0;

        while(!complete)
        {
            dialog.text += text[i];
            yield return new WaitForSeconds(0.01f * delayMult);
            i++;
            if(i == text.Length)
            {
                complete = true;
            }
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                complete = true;
                break;
            }
        }
    }

    IEnumerator TypeTextAbstract(string text, float delayMult = 1)
    {
        abstractDialog.text = "";
        bool complete = false;
        int i = 0;

        while(!complete)
        {
            abstractDialog.text += text[i];
            yield return new WaitForSeconds(0.01f * delayMult);
            i++;
            if(i == text.Length)
            {
                complete = true;
            }
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                complete = true;
                break;
            }
        }
    }
}
