using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score\n" + Player.instance.Score.ToString();
    }
}
