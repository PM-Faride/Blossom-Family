using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTheGame : MonoBehaviour
{

    [SerializeField] private AudioClip readySound;
    [SerializeField] private AudioClip goSound;
    [SerializeField] private AudioClip bgSound;
    [SerializeField] private GameObject[] ready;
    [SerializeField] private GameObject[] go;
    [SerializeField] private GameObject[] game;
    //[SerializeField] private Component[]
    //[SerializeField] private bool startTheGame = false;
    //private AudioSource readyGoSound;
    private AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        //audioSrc.clip = readySound;
        StartCoroutine(PlayMusic());
        //audioSrc.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator PlayMusic()
    {
        for (int i = 0; i < ready.Length; i++)
        {
            ready[i].SetActive(true);
        }
        audioSrc.clip = readySound;
        audioSrc.Play();
        yield return new WaitForSeconds(audioSrc.clip.length);
        for (int i = 0; i < ready.Length; i++)
        {
            ready[i].SetActive(false);
        }
        //ready.SetActive(false);
        for (int i = 0; i < go.Length; i++)
        {
            go[i].SetActive(true);
        }
        //go.SetActive(true);
        audioSrc.clip = goSound;
        audioSrc.Play();
        yield return new WaitForSeconds(audioSrc.clip.length);
        //go.SetActive(false);
        for (int i = 0; i < go.Length; i++)
        {
            go[i].SetActive(false);
        }
        audioSrc.loop = true;
        audioSrc.clip = bgSound;
        audioSrc.Play();
        for (int i = 0; i < game.Length; i++)
        {
            game[i].SetActive(true);
        }
    }
}
