using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TargetsLeftText : MonoBehaviour
{

    public int targetsLeft;
    public Text text;
    public Text gameOverText1;
    public Text gameOverText2;
    public GameObject gameOverUI;

    void Start(){
        targetsLeft = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(targetsLeft <= 0){

            gameOverText1.text = "CONGRATULATIONS!";
            gameOverText2.text = "YOU WON!";
            gameOverUI.SetActive(true);

        }
        else{
            text.text = "Targets Left: " + targetsLeft.ToString();
        }
        
    }
}
