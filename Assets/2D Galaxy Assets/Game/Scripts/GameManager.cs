using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    [SerializeField]
    public GameObject player;

    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    private void Update()
    {
        if(gameOver == true)
        {
            if(Input.GetKeyUp(KeyCode.Space))
            {
                Instantiate(player,Vector3.zero,Quaternion.identity);
                gameOver = false;
                _uiManager.HideTitleScreen();
            }
        }    
    }
}
