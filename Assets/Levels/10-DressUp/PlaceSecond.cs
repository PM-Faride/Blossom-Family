using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSecond : MonoBehaviour
{
    [SerializeField] private Transform leftHatsPlace;
    [SerializeField] private Transform leftClothesPlace;
    [SerializeField] private Transform leftShoesPlace;
    [SerializeField] private Transform rightHatsPlace;
    [SerializeField] private Transform rightClothesPlace;
    [SerializeField] private Transform rightShoesPlace;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if(collision.gameObject.name == "LeftHats1" || collision.gameObject.name == "LeftHats2")
        //{
        //    Debug.Log(collision.gameObject.name);
        //    collision.gameObject.transform.position = leftHatsPlace.position;
        //}
        //if(collision.gameObject.name == "LeftClothes1" || collision.gameObject.name == "LeftClothes2")
        //{
        //    Debug.Log(collision.gameObject.name);
        //    collision.gameObject.transform.position = leftClothesPlace.position;
        //}
        //if (collision.gameObject.name == "LeftShoes1" || collision.gameObject.name == "LeftShoes2")
        //{
        //    Debug.Log(collision.gameObject.name);
        //    collision.gameObject.transform.position = leftShoesPlace.position;
        //}
        //if (collision.gameObject.name == "RightHats1" || collision.gameObject.name == "RightHats2")
        //{
        //    Debug.Log(collision.gameObject.name);
        //    collision.gameObject.transform.position = rightHatsPlace.position;
        //}
        //if (collision.gameObject.name == "RightClothes1" || collision.gameObject.name == "RightClothes2")
        //{
        //    Debug.Log(collision.gameObject.name);
        //    collision.gameObject.transform.position = rightClothesPlace.position;
        //}
        //if (collision.gameObject.name == "RightShoes1" || collision.gameObject.name == "RightShoes2")
        //{
        //    Debug.Log(collision.gameObject.name);
        //    collision.gameObject.transform.position = rightShoesPlace.position;
        //}
        if(collision.gameObject.name == "LeftMask")
        {
            if(gameObject.name == "LeftHats1" || gameObject.name == "LeftHats2")
            {
                transform.position = leftHatsPlace.position;
            }
            else if (gameObject.name == "LeftClothes1" || gameObject.name == "LeftClothes2")
            {
                transform.position = leftClothesPlace.position;
            }
            else if(gameObject.name == "LeftShoes1" || gameObject.name == "LeftShoes2")
            {
                transform.position = leftShoesPlace.position;
            }
        }
        if (collision.gameObject.name == "RightMask")
        {
            if (gameObject.name == "RightHats1" || gameObject.name == "RightHats2")
            {
                transform.position = rightHatsPlace.position;
            }
            else if (gameObject.name == "RightClothes1" || gameObject.name == "RightClothes2")
            {
                transform.position = rightClothesPlace.position;
            }
            else if(gameObject.name == "RightShoes1" || gameObject.name == "RightShoes2")
            {
                transform.position = rightShoesPlace.position;
            }
        }
    }
}
