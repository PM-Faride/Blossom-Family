using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetector : MonoBehaviour
{
    [SerializeField] private bool isLeft = false;
    private BoxCollider2D dCollider;
    // Start is called before the first frame update
    void Start()
    {
        dCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLeft)
        {
            if(collision.gameObject.tag == "Left")
            {
                DressUp.lCurrentPhoto = collision.gameObject.name;
                //if(collision.gameObject.name == "6")
                //Debug.Log(collision.gameObject.name);
                //DressUp.lCurrent = collision.gameObject;
            }
        }
        else
        {
            if (collision.gameObject.tag == "Right")
            {
                DressUp.rCurrentPhoto = collision.gameObject.name;
                //DressUp.rCurrent = collision.gameObject;
            }
        }
    }
}
