using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Player player;
    private Text distanceText;
    
    private void Awake() {
        player = GameObject.Find("Player").GetComponent<Player>();
        distanceText  = GameObject.Find("DistanceText").GetComponent<Text>();
    }


    private void Update() {
        int distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance + " m";
    }
}
