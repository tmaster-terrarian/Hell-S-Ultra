using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool confirmation = false;
    Scene scene;
    GameObject player;
    public Text quitText = null;
    public Sprite hitboxSprite;

    bool showHitboxes = false;
    SpriteRenderer[] spriteRenderers;
    SpriteRenderer[] spriteRenderers2;
    BoxCollider[] colliders;
    BoxCollider2D[] colliders2D;

    void Awake()
    {
        scene = SceneManager.GetActiveScene();

        Application.targetFrameRate = 60;

        spriteRenderers = new SpriteRenderer[999999];
        spriteRenderers2 = new SpriteRenderer[999999];
    }

    void Start()
    {
        int n = 0; //Used for 3D Box Colliders. Set to an offset for special values.
        int m = 1; //Used for 2D Box Colliders. Set to an offset for special values. [0] = playerHitbox

        colliders = transform.GetComponentsInChildren<BoxCollider>();
        colliders2D = transform.GetComponentsInChildren<BoxCollider2D>();
        SpriteRenderer playerHitbox = GetPlayer().transform.GetChild(2).GetComponent<SpriteRenderer>();
        spriteRenderers2[0] = playerHitbox;

        foreach (BoxCollider col in colliders)
        {
            GameObject go = new GameObject();
            SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer>();
            go.transform.parent = col.transform;
            go.transform.position = col.bounds.center;
            go.transform.rotation = col.transform.rotation;
            spriteRenderer.sprite = hitboxSprite;
            spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            spriteRenderer.sortingOrder = 100;
            spriteRenderer.color = new Color(1, 0, 0, 0);
            if(col.isTrigger)
            spriteRenderer.color = new Color(1, 0, 1, 0);
            spriteRenderer.size = new Vector2(col.size.x, col.size.y);
            spriteRenderers[n] = spriteRenderer;
            n++;
        }
        foreach (BoxCollider2D col in colliders2D)
        {
            GameObject go = new GameObject();
            SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer>();
            go.transform.parent = col.transform;
            go.transform.position = col.bounds.center;
            go.transform.rotation = col.transform.rotation;
            spriteRenderer.sprite = hitboxSprite;
            spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            spriteRenderer.sortingOrder = 100;
            spriteRenderer.color = new Color(1, 0, 0, 0);
            if(col.isTrigger)
            {
                spriteRenderer.color = new Color(1, 0, 1, 0);
            }
            spriteRenderer.size = new Vector2(col.size.x, col.size.y);
            spriteRenderers2[m] = spriteRenderer;
            m++;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && confirmation == false)
        {
            if(quitText != null)
                quitText.text = "quit game???????";
            confirmation = true;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && confirmation == true)
        {   
            #if UNITY_EDITOR
            if(quitText != null)
                quitText.text = "dwafaswdfsafasf";
            confirmation = false;
            #else
            if(quitText != null)
                quitText.text = "kys";
            Application.Quit();
            #endif
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(scene.buildIndex);
        }

        if(Input.GetKeyDown(KeyCode.F1))
        {
            if(showHitboxes == true)
            {
                ToggleHitboxes(false);
                showHitboxes = false;
            }
            else
            {
                ToggleHitboxes(true);
                showHitboxes = true;
            }
        }
    }

    public void SetHitBoxColors(Color color)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i] != null)
            {
                spriteRenderers[i].color = color;
            }
        }
    }

    public void ToggleHitboxes(bool value)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i] != null)
            {
                if(value == true)
                spriteRenderers[i].color = new Color(spriteRenderers[i].color.r, spriteRenderers[i].color.g, spriteRenderers[i].color.b, 1);
                else
                spriteRenderers[i].color = new Color(spriteRenderers[i].color.r, spriteRenderers[i].color.g, spriteRenderers[i].color.b, 0);
            }
            if(colliders2D[i] != null)
            {
                if(value == true)
                spriteRenderers2[i].color = new Color(spriteRenderers2[i].color.r, spriteRenderers2[i].color.g, spriteRenderers2[i].color.b, 1);
                else
                spriteRenderers2[i].color = new Color(spriteRenderers2[i].color.r, spriteRenderers2[i].color.g, spriteRenderers2[i].color.b, 0);
            }
        }
    }

    public static GameObject GetPlayer()
    {
        return GameObject.FindGameObjectWithTag("player");
    }
}
