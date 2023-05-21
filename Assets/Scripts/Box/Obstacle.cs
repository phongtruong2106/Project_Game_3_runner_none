using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Player player;

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<Player>(); //find gameObject for name Player , laster Get Component to the Player 
    }

    private void FixedUpdate() {
        Vector2 pos =  transform.position;

        pos.x -= player.velocity.x * Time.fixedDeltaTime;
        if(pos.x < -100)
        {
            Destroy(gameObject); //if box smaller -100 ,destroy the object
        }

        transform.position = pos;
    }
}
