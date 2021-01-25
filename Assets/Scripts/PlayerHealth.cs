using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] int health = 3;
    [SerializeField] int healthDecrease = 1;
    [SerializeField] Text healthText;

    [SerializeField] AudioClip playerDamageSFX;




    void Start()
    {
        healthText.text = health.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().PlayOneShot(playerDamageSFX); //if it is repeated one time in the game
        health = health - healthDecrease;
        healthText.text = health.ToString();
        if (health < 1)
        {
            Invoke("StartDeathSequence",1f);
        }
    }

    private void StartDeathSequence()
    {
        SceneManager.LoadScene(0);
    }
}
