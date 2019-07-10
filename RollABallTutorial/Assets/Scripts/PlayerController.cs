using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    
    public Text countText;
    public Text winText;
    public Text livesText;
    public GameObject Trail;
    public float sprintSpeed;
    public float normalSpeed;
    public float jumpHeight;
    public float breakSpeed;

    private Rigidbody rb;
    private Transform tf;
    private TrailRenderer trailRenderer;
    private bool isJumping;
    private bool canJump;
    private float stamina;
    private float speed;
    private float airSprintSpeed;
    private float airNormalSpeed;
    private float curSprintSpeed;
    private float curNormalSpeed;
    private int count;
    private int staminaDepletion;
    private int staminaRegen;
    private int lives;
    private int hitnum;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        trailRenderer = Trail.GetComponent<TrailRenderer>();
        count = 0;
        stamina = 100;
        staminaDepletion = 25;
        staminaRegen = 50;
        airNormalSpeed = normalSpeed/2;
        airSprintSpeed = sprintSpeed/2;
        curSprintSpeed = sprintSpeed;
        curNormalSpeed = normalSpeed;
        isJumping = false;
        canJump = true;
        lives = 3;
        SetCountText();
        SetLivesText();
    }

    void Update() {
        if (Input.GetKey("escape"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);;
        if(Input.GetKey("left shift") && stamina > 0){
            speed = curSprintSpeed;
            trailRenderer.emitting = true;
            stamina -= Time.deltaTime * staminaDepletion;
        }
        else if(!Input.GetKey("left shift") || stamina <= 0){
            speed = curNormalSpeed;
            trailRenderer.emitting = false;

            if(stamina < 100 && !Input.GetKey("left shift")) {
                stamina += Time.deltaTime * staminaRegen;
            }
        }
        if(Input.GetKey("q") && !isJumping){rb.AddForce(rb.velocity*-1*breakSpeed);}
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float jumpAxis = Input.GetAxis("Jump");

        Vector3  movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        Vector3 jumpVector = new Vector3 (0.0f, jumpAxis, 0.0f);

        rb.AddForce(movement * speed);


        if(canJump && jumpAxis > 0){
            curNormalSpeed = airNormalSpeed;
            curSprintSpeed = airSprintSpeed;
            rb.AddForce(jumpVector * jumpHeight);
            isJumping = true;
            canJump = false;
        }
        //if(canJump && isJumping && jumpAxis >0){
            //rb.AddForce(movement+jumpVector * jumpHeight);
            //canJump = false;
        //}
    }

    void OnCollisionEnter(Collision collision){
        rb.AddForce(collision.impulse*17);
        if(Physics.Raycast(tf.position, Vector3.down,1.0f)){
            curNormalSpeed = normalSpeed;
            curSprintSpeed = sprintSpeed;
            canJump = true;
            isJumping = false;
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Pick Up")){
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        if(other.gameObject.CompareTag("Enemy")){
            other.gameObject.SetActive(false);
            lives--;
            SetLivesText();
        }
        if(other.gameObject.CompareTag("DeathBox")){
            lives--;
            tf.position = new Vector3(0.0f, 0.7f, 0.0f);
            SetLivesText();
        }
    }

    void SetCountText() {
        countText.text = "Pips Collected: " + count.ToString();
        if (count >= 12) {
            winText.text = "You monster...";
        }
    }
    void SetLivesText() {
        livesText.text = "Lives Left: " + lives.ToString();
        if (lives == 0) {
            this.gameObject.SetActive(false);
            winText.text = "Jeez, you're bad at this...";
        }
    }

    public int GetCount() {
        return this.count;
    }
}
