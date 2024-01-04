using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameOverHandler gameOverHandler;
    public void Crash(){
        gameOverHandler.EndGame();
        gameObject.SetActive(false);
    } 
}
