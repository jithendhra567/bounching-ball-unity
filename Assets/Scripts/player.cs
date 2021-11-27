using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class player : MonoBehaviour{
    public Camera mainCamera;
    public float speed = 5f;
    public Rigidbody rb;
    public float jumpForce;
    public Vector3 jump;
    public bool isGrounded;
    public float sensitivity = .16f, clampDelta = 42f;
    public float bounds = 4.5f;
    private Vector3 lastMousePos;
    private Vector3 lastTouchPos;
    AudioSource audioData;

    // Update is called once per frame
    void OnCollisionStay(){
        isGrounded = true;
    }

    private void OnCollisionEnter(Collision other) {
        audioData = GetComponent<AudioSource>();
        if(other.gameObject.name!="ground" && other.gameObject.name!="helper"){
            Debug.Log("Collision"+other.gameObject.name);
            Vibration.Vibrate(20);
            if(FindObjectOfType<GameManager>().isStarted){
                FindObjectOfType<GameManager>().EndGame();
            }
        }
        else {
            audioData.volume = 4f;
            audioData.Play();
        }
    }

    void Update(){
        if(FindObjectOfType<GameManager>().isStarted) transform.position += Vector3.forward * speed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -bounds, bounds), transform.position.y, transform.position.z);
        if(isGrounded) {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate(){
        if (Input.GetMouseButtonDown(0)){
            lastMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0)){
            if(!FindObjectOfType<GameManager>().isStarted) FindObjectOfType<GameManager>().isStarted = true;
            Vector3 vector = lastMousePos - Input.mousePosition;
            lastMousePos = Input.mousePosition;
            vector = new Vector3(vector.x, 0, 0);
            Vector3 moveForce = Vector3.ClampMagnitude(vector, clampDelta);
            rb.AddForce(-moveForce * sensitivity - rb.velocity / 5f, ForceMode.VelocityChange);
        }
        if(Input.touches.Length>0){
            if(!FindObjectOfType<GameManager>().isStarted) FindObjectOfType<GameManager>().isStarted = true;
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved){
                rb.AddForce(transform.position.x + touch.deltaPosition.x * speed * sensitivity, 0, 0);
            }
        }
        rb.velocity.Normalize();
    }

}
