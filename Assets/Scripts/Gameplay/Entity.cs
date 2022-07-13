using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using CSi.Utility;

public class Entity : MonoBehaviour
{
    public float hp;
    public float hpMax;

    bool isPlayer;

    Scene scene;

    PlayerController playerController;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        playerController = GameManager.GetPlayer().GetComponent<PlayerController>();
    }

    void Update()
    {
        hp = Mathf.Clamp(hp, 0, hpMax);
        if(hp <= 0)
        {
            Die();
        }
    }

    public void SetIsPlayer(bool value)
    {
        this.isPlayer = true;
    }

    public void Hurt(float value)
    {
        this.hp -= value;
    }

    public void Heal(float value)
    {
        this.hp += value;
        if(this.hp > this.hpMax)
        {
            this.hp = this.hpMax;
        }
    }

    public void Die()
    {
        if(isPlayer)
        {
            SFX.Play("ovumelDeath00", 0, false);
            DeathText.OnPlayerDie();
            playerController.canInput = false;
            playerController.gameObject.SetActive(false);
        }
        else
        Destroy(gameObject);
    }
}
