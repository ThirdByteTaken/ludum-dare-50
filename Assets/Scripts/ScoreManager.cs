using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static int Score;

    public void Start()
    {
        SetScoreText(Score);
    }

    [SerializeField]
    TMPro.TMP_Text txt_Score;
    public void SetScoreText(int value)
    {
        txt_Score.text = "Total Asteroids Destroyed-" + value;
    }
}
