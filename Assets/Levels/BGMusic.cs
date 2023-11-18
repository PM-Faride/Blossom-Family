using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    [SerializeField] private AudioClip readySound;
    [SerializeField] private AudioClip goSound;
    [SerializeField] private AudioClip bgSound;
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
        audioSrc.clip = readySound;
        audioSrc.Play();
        yield return new WaitForSeconds(audioSrc.clip.length);
        audioSrc.clip = goSound;
        audioSrc.Play();
        yield return new WaitForSeconds(audioSrc.clip.length);
        audioSrc.loop = true;
        audioSrc.clip = bgSound;
        audioSrc.Play();
    }

    //void PlaySound()
    //{
    //    audioSrc = GetComponent<AudioSource>();
    //    audioSrc.clip = audioClip;
    //    audioSrc.Play();
    //}

}
