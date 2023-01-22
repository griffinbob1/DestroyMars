using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackScript : MonoBehaviour
{
    [SerializeField]
    private GameObject end;
    [SerializeField]
    private GameObject Canvas;
    private WinLose WinLoseScript;
    [SerializeField]
    private GameObject player;
    private MovementScript movementScript;
    private float endLocation;

    private float offset = 5f;
    private float maxTrackSpeed = 6f;
    [SerializeField]
    private float currTrackSpeed = 0f;
    private float movementSpeed = 5f;
    
    void Start()   
    {
        endLocation = end.transform.position.y - offset;
        WinLoseScript = Canvas.GetComponent<WinLose>();
        movementScript = player.GetComponent<MovementScript>();
    }

    void Update()
    {
        //Increases speed as the player plays up to the max speed
        if (movementScript.started == true)
        {
            if (currTrackSpeed < maxTrackSpeed)
            {
                currTrackSpeed = currTrackSpeed + Time.deltaTime;
                //After that increase, if it overflows, correct it
                if (currTrackSpeed > maxTrackSpeed)
                {
                    currTrackSpeed = maxTrackSpeed;
                }
            }
        }

        //Actually moves the player
        if (WinLoseScript.lost == false)
        {
            if (transform.position.y < endLocation)
            {
                //Debug.Log(currTrackSpeed / maxTrackSpeed);
                transform.Translate(new Vector2(0, currTrackSpeed / maxTrackSpeed) * movementSpeed * Time.deltaTime);
            }else{
                player.transform.Translate(new Vector2(0, currTrackSpeed / maxTrackSpeed) * movementSpeed * Time.deltaTime);
            }
        }
    }
}
