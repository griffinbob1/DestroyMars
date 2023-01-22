using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI timerText;
    [SerializeField]
    private GameObject player;
    private MovementScript movementScript;
    [SerializeField]
    private GameObject Canvas;
    private WinLose WinLoseScript;
    private float time = 0;

    void Start()   
    {
        movementScript = player.GetComponent<MovementScript>();
        WinLoseScript = Canvas.GetComponent<WinLose>();
        timerText.text = "";
    }

    void Update()
    {
        if (movementScript.started == true)
        {
            if (WinLoseScript.won == false && WinLoseScript.lost == false)
            {
                time = time + Time.deltaTime;
                timerText.text = Mathf.Floor(time).ToString();
            }else{
                timerText.text = "";
            }
        }
    }
}
