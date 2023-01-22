using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Squiggy : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite first;
    [SerializeField]
    private Sprite second;
    private float currTime;
    private float timeTilSquiggle = 0.225f;

    //So not really sure but I read that you are not supposed to use animators on the canvas and I was 
    //having a lil trouble with it so instead I am doing this, very cool.

    void Update()
    {
        currTime = currTime + Time.deltaTime;
        if (currTime > timeTilSquiggle)
        {
            if (image.sprite == first){
            image.sprite = second;
            }else{
            image.sprite = first;
            }

            currTime = 0;
        }
    }
}
