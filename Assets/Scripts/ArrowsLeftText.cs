using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowsLeftText : MonoBehaviour
{
    
    public Bow bowScript;
    public Text arrowsLeftText;
    public Text gameOverText1;
    public Text gameOverText2;
    public GameObject gameOverUI;
    void Update()
    {

        if(bowScript.bowSettings.arrowCount <= 0){
                gameOverText1.text = "GAME OVER";
                gameOverText2.text = "YOU RAN OUT OF ARROWS";
                gameOverUI.SetActive(true);
        }
        else{
            arrowsLeftText.text = "Arrows Left: " + bowScript.bowSettings.arrowCount.ToString();
        }
    }
}
