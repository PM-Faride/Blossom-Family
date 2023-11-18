using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    [SerializeField] private Transform[] playerPos;
    [SerializeField] private GameObject player;

    private GameObject tmp;
    // Start is called before the first frame update
    void Start()
    {
        tmp = Instantiate(player, playerPos[0].position, playerPos[0].rotation);
        tmp.SetActive(true);
        tmp = Instantiate(player, playerPos[1].position, playerPos[1].rotation);
        tmp.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
