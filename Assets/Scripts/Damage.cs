using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]
    private GameObject Canvas;
    private WinLose WinLoseScript;

    void Start()
    {
        WinLoseScript = Canvas.GetComponent<WinLose>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entered!");
        //~~~~~~~~~~~~~~~~~~~~~Damage collide~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        if (other.tag == "Player")
        {
            Debug.Log("Damaged!!!!!!");
            WinLoseScript.StartLose();
        }
    }
}
