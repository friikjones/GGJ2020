using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{

    public Text scoreText;

    void Start()
    {
        scoreText.text = SessionData.machinesSaved.ToString()+"/12";
    }

}
