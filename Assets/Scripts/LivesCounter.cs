using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesCounter : MonoBehaviour
{
    [SerializeField] Text livesText;
    [SerializeField] string livesPreText = "Remaining Lives:";

    public void setLives(int lives)
    {
        livesText.text = livesPreText + " " + lives;
    }
}
