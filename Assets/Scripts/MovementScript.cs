using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField]
    private bool moving = false;
    [SerializeField]
    //Current progress on speed, increases as button is held
    private float rampup = 0f;
    //The amount needed to hit max rampup, increasing means the player must hold longer for max speed
    private float maxRampup = 0.7f;
    //How quickly the player loses speed upon not pressing any buttons
    private float rampdownSpeed = 2.75f;
    //Bonus speed when changing directions, ex. rampup is negative and you are now trying to make it positive
    private float rampAround = 2.5f;
    //How far left & right the player can move
    private float maxRange = 9f;
    //How fast the player can move
    private float movementSpeed = 9f;
    public bool started = false;

    [SerializeField]
    private GameObject Canvas;
    private WinLose WinLoseScript;
    [SerializeField]
    private GameObject Intro;
    private Intro IntroScript;
    [SerializeField]
    private AudioSource playerSource;

    void Start()
    {
        WinLoseScript = Canvas.GetComponent<WinLose>();
        IntroScript = Intro.GetComponent<Intro>();
    }

    void FixedUpdate()
    {
        //~~~~~~~~~~~~~~~~~~~~~Using Keys & increasing rampup~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        moving = false;
        if (WinLoseScript.won == false && WinLoseScript.lost == false)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (IntroScript.done == true)
                {
                    started = true;
                    StartCoroutine(IntroScript.Leave());
                }

                moving = true;
                if (Mathf.Abs(rampup - Time.deltaTime) < maxRampup){
                    rampup = rampup - Time.deltaTime;
                    if (rampup > 0)
                    {
                        rampup = rampup - (rampAround * Time.deltaTime);
                    }
                }else{
                    if (rampup > 0){
                        rampup = rampup - (rampAround * Time.deltaTime);
                    }else
                    {
                    rampup = -maxRampup;
                    }
                }
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (IntroScript.done == true)
                {
                    started = true;
                    StartCoroutine(IntroScript.Leave());
                }

                moving = true;
                if (Mathf.Abs(rampup) < maxRampup){
                    rampup = rampup + Time.deltaTime;
                    if (rampup < 0)
                    {
                        rampup = rampup + (rampAround * Time.deltaTime);
                    }
                }else{
                    if (rampup < 0)
                    {
                        rampup = rampup + (rampAround * Time.deltaTime);
                    }else{
                        rampup = maxRampup;
                    }
                }
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~Decreasing rampup upon not moving~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        if (moving == false && rampup != 0)
        {
            if (rampup > 0){
                if (rampup - (rampdownSpeed * Time.deltaTime) > 0){
                    rampup = rampup - (rampdownSpeed * Time.deltaTime);
                }else{
                    rampup = 0;
                }
            }else{
                if (rampup - (rampdownSpeed * Time.deltaTime) < 0){
                    rampup = rampup + (rampdownSpeed * Time.deltaTime);
                }else{
                    rampup = 0;
                }
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~Actually moving the character~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        //Checking to make sure we can add the expected amount of distance without going out of bounds
        if (Mathf.Abs(transform.position.x + (((rampup / maxRampup) * movementSpeed) * Time.deltaTime)) < maxRange)
        {
            transform.position = (new Vector3(transform.position.x + (((rampup / maxRampup) * movementSpeed) * Time.deltaTime), transform.position.y, transform.position.z));
        }
        else
        {
            rampup = 0;
            if(transform.position.x > 0)
            {
                transform.position = (new Vector3(maxRange, transform.position.y, transform.position.z));
            }else{
                transform.position = (new Vector3(-maxRange, transform.position.y, transform.position.z));
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~Fun lil roations~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        Quaternion target = Quaternion.Euler(0, 0, -((rampup / maxRampup) * 30f));
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * 5);

        //~~~~~~~~~~~~~~~~~~~~~Wind sound~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        if (WinLoseScript.won == false && WinLoseScript.lost == false)
        {
            playerSource.volume = Mathf.Abs(rampup) / maxRampup;
        }
        
        //~~~~~~~~~~~~~~~~~~~~~Debug~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        if (transform.position.x > maxRange || transform.position.x < -maxRange)
        {
            Debug.Log("Something broke lol");
        }
    }
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Damage")
        {
            WinLoseScript.StartLose();
        }
        if (other.tag == "Win")
        {
            WinLoseScript.StartWin();
            //Destroy(this.gameObject);
        }
    }
}
