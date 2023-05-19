using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
   [SerializeField] private float depth = 1;

    private Player player;
    
    private void Awake() {
        
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void FixedUpdate() {
         float realVelocity = player.velocity.x / depth;

        Vector2 pos = transform.position;

        pos.x -= realVelocity * Time.fixedDeltaTime;
        if(pos.x <= -25)
            pos.x = 80;
        transform.position = pos;
    }
}
