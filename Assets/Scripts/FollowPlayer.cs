using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowPlayer : MonoBehaviour{
    public float smoothSpeed = 0.02f;
    public float speed = 5f;

    Text taptostart;

    private void Start() {
        taptostart = GameObject.Find("PLAY").GetComponent<Text>();
    }
    void Update(){
        if(FindObjectOfType<GameManager>().isStarted) {
            transform.position += Vector3.forward * speed * Time.deltaTime;
            taptostart.enabled = false;
        }
    }
}
