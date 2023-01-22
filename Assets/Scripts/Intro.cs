using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public bool done = false;

    void Start()
    {
        StartCoroutine(Enter());
    }

    IEnumerator Enter()
    {
        for(int i = 100; i > 0; i--)
        {
            this.GetComponent<RectTransform>().localPosition = (new Vector2(this.GetComponent<RectTransform>().localPosition.x, this.GetComponent<RectTransform>().localPosition.y + ((i * i) / 1000)));
            yield return new WaitForSeconds(0.005f);
        }
        done = true;
    }

    public IEnumerator Leave()
    {
        for(int i = 0; i < 100; i++)
        {
            this.GetComponent<RectTransform>().localPosition = (new Vector2(this.GetComponent<RectTransform>().localPosition.x, this.GetComponent<RectTransform>().localPosition.y - ((i * i) / 1000)));
            yield return new WaitForSeconds(0.005f);
        }
    }
}
