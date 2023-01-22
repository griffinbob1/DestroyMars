using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLose : MonoBehaviour
{
    [SerializeField]
    private Image winText;
    [SerializeField]
    private Image loseText;

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioClip musicSound;
    [SerializeField]
    private AudioClip winSound;
    [SerializeField]
    private AudioClip loseSound;
    [SerializeField]
    private AudioSource playerSource;
    [SerializeField]
    private AudioClip explosionSound;
    [SerializeField]
    private GameObject block;

    public bool won = false;
    public bool lost = false;

    void Start()
    {
        block.gameObject.SetActive(false);
        StartCoroutine(StartMusic());
    }

    void Update()
    {
        if (won == true || lost == true){
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                SceneManager.LoadScene("Game");
            }
        }
    }

    IEnumerator StartMusic()
    {
        yield return new WaitForSeconds(0.8f);
        musicSource.Play();
        yield return new WaitForSeconds(1f);
        musicSource.loop = true;
        playerSource.volume = 0.25f;
        musicSource.clip = musicSound;
        musicSource.Play();
    }

    public void StartWin()
    {
        if (won == false){
            won = true;
            StartCoroutine(EnterWin());
        }
    }

    IEnumerator EnterWin()
    {
        musicSource.loop = false;
        playerSource.volume = 1;
        musicSource.clip = winSound;
        musicSource.Play();
        Boom();

        for(int i = 100; i > 0; i--)
        {
            winText.GetComponent<RectTransform>().localPosition = (new Vector2(winText.GetComponent<RectTransform>().localPosition.x, winText.GetComponent<RectTransform>().localPosition.y - ((i * i) / 1000)));
            yield return new WaitForSeconds(0.004f);
        }
    }

    public void StartLose()
    {
        if (lost == false){
            lost = true;
            StartCoroutine(EnterLose());
        }
    }

    IEnumerator EnterLose()
    {
        musicSource.loop = false;
        playerSource.volume = 1;
        musicSource.clip = loseSound;
        musicSource.Play();
        Boom();

        for(int i = 100; i > 0; i--)
        {
            loseText.GetComponent<RectTransform>().localPosition = (new Vector2(loseText.GetComponent<RectTransform>().localPosition.x, loseText.GetComponent<RectTransform>().localPosition.y - ((i * i) / 1000)));
            yield return new WaitForSeconds(0.004f);
        }
    }

    void Boom()
    {
        playerSource.loop = false;
        playerSource.volume = 0.3f;
        playerSource.clip = explosionSound;
        playerSource.Play();

        block.gameObject.SetActive(true);
    }
}
