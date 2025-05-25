using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    [SerializeField]
    public Image livesImageDisplay;
    public int score;
    public Text scoreText;
    public GameObject titleScree;
   public void UpdateLives(int currentLives)
    {
        Debug.Log("Player lives: " + currentLives);
        livesImageDisplay.sprite = lives[currentLives]; 
    }
    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }
    public void ShowTitleScreen()
    {
        titleScree.SetActive(true);
    }
    public void HideTitleScreen()
    {
        titleScree.SetActive(false);
        scoreText.text = "Score: ";
    }
}
