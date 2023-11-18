using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Fruit : MonoBehaviour
{
    [SerializeField] private float fallingSpeed;
    [SpineAnimation] public string[] IdleAnimes;
    [SerializeField] private Transform destructionPoint;

    private SkeletonAnimation fruit;
    private int rnd;

    private void Awake()
    {
        fruit = GetComponent<SkeletonAnimation>();
        rnd = RandomNumber.IntRandomNumber(1, 0, IdleAnimes.Length - 1, false)[0];
        fruit.AnimationName = IdleAnimes[rnd];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * fallingSpeed * Time.fixedDeltaTime);
        if(transform.position.y < destructionPoint.position.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Basket"))
        {
            Destroy(gameObject);
        }
    }
}
