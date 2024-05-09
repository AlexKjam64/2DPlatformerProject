using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScore : MonoBehaviour
{
    [Header("Objects")]
    public CharacterController2D controller;
    public AudioSource coinSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ScoreManager.scoreMan.IncreaseScore(1);
            controller.Heal();
            coinSound.Play();
            Destroy(this.gameObject);
        }
    }
}
