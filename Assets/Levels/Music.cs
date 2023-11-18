using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClip;
    //[SerializeField] private AudioClip audioClip2;
    private AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        //audioSrc = GetComponent<AudioSource>();
        //audioSrc.clip = audioClip;
        //audioSrc.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(int audioIndex)
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = audioClip[audioIndex];
        audioSrc.Play();
    }
}
