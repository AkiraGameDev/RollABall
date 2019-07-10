using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public GameObject playerObj;
    public Text narratorT;
    public int pipcount;
    public float stopwatch;
    public GameObject[] pips = new GameObject[10];

    private PlayerController playerScript;
    private bool jumpStart;
    private bool upStart;

    void Awake(){
        playerObj = GameObject.Find("Player");
        playerScript = playerObj.GetComponent<PlayerController>();
        jumpStart = false;
        upStart = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        stopwatch = Time.time;
        StartCoroutine("Commence");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pipcount = playerScript.GetCount();
        if(pipcount == 1 && !jumpStart){
            jumpStart = true;
            stopwatch = Time.time;
            StartCoroutine("JumpTime");
        }
        if(pipcount == 3 && !upStart){
            upStart = true;
            stopwatch = Time.time;
            StartCoroutine("UpTime");
        }
    }

    IEnumerator Commence(){
        while(Time.time <= 3.55f){
            if(Time.time > 0.5f){narratorT.text = "GET";}
            if(Time.time > 1.0f){narratorT.text = "THOSE";}
            if(Time.time > 1.5f){narratorT.text = "PIPS";}
            if(Time.time > 2.0f){narratorT.text = "DONT";}
            if(Time.time > 2.5f){narratorT.text = "ASK";}
            if(Time.time > 3.0f){narratorT.text = "QUESTIONS";}
            if(Time.time > 3.5f){
                narratorT.text = "";
                pips[0].SetActive(true);
            }
            yield return null;
        }

    }
    IEnumerator JumpTime(){
        while(GetTime() <= 3.6f){
            if(GetTime() > 0.5f){narratorT.text = "LETS";}
            if(GetTime() > 1.0f){narratorT.text = "HOPE";}
            if(GetTime() > 1.5f){narratorT.text = "YOUR";}
            if(GetTime() > 2.0f){narratorT.text = "SPACE";}
            if(GetTime() > 2.5f){narratorT.text = "BAR";}
            if(GetTime() > 3.0f){narratorT.text = "WORKS";}
            if(GetTime() > 3.5f){
                narratorT.text = "";
                pips[1].SetActive(true);
                pips[2].SetActive(true);
            }
            yield return null;
        }

    }
    IEnumerator UpTime(){
        while(GetTime() <= 3.6f){
            if(GetTime() > 0.5f){narratorT.text = "PATIENCE";}
            if(GetTime() > 1.0f){narratorT.text = "IS";}
            if(GetTime() > 1.5f){narratorT.text = "KEY";}
            if(GetTime() > 2.0f){narratorT.text = "PRESS Q";}
            if(GetTime() > 2.5f){narratorT.text = "TO";}
            if(GetTime() > 3.0f){narratorT.text = "BREAK";}
            if(GetTime() > 3.5f){
                narratorT.text = "";
                pips[3].SetActive(true);
                pips[4].SetActive(true);
            }
            yield return null;
        }

    }
    IEnumerator FastTime(){
        while(GetTime() <= 3.6f){
            if(GetTime() > 0.5f){narratorT.text = "READY";}
            if(GetTime() > 1.0f){narratorT.text = "TO";}
            if(GetTime() > 1.5f){narratorT.text = "RUN?";}
            if(GetTime() > 2.0f){narratorT.text = "PRESS SHIFT";}
            if(GetTime() > 2.5f){narratorT.text = "TO";}
            if(GetTime() > 3.0f){narratorT.text = "SPRINT";}
            if(GetTime() > 3.5f){narratorT.text = "RED = BAD";}
            yield return null;
        }

    }

    float GetTime(){
        return Time.time - stopwatch;
    }
}
