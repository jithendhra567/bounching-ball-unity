using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour{
    bool gameOver = false;
    public bool isStarted = false;
    public void EndGame(){
        isStarted = false;
        if(!gameOver){
            gameOver = true;
            Debug.Log("Game Over");
            Invoke("Restart", 0.5f);
        }
    }

    void Restart(){
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
