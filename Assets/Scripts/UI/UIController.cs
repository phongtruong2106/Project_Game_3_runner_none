using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private Player player;
    private Text distanceText;

    private GameObject results;
    private Text finalDistanceText;

    
    private void Awake() {
        player = GameObject.Find("Player").GetComponent<Player>();
        distanceText  = GameObject.Find("DistanceText").GetComponent<Text>();
        finalDistanceText = GameObject.Find("FinalDistanceText").GetComponent<Text>();

        results = GameObject.Find("Results");
        results.SetActive(false);

    }


    private void Update() {
        int distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance + " m";
         
         if(player.isDead)
         {
            results.SetActive(true);
            finalDistanceText.text = distance + " m";
         }
    }

    public void Quit(){
        SceneManager.LoadScene("Menu");
    }

    public void Retry()
    {
        SceneManager.LoadScene("lv1");
    }
}
