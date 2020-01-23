using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10;
    private Rigidbody2D bullet;

    // Start is called before the first frame update
    void Start()
    {
        bullet = gameObject.GetComponent<Rigidbody2D>();
        bullet.velocity = transform.right * bulletSpeed;
        
    }

    // Update is called once per frame
    void Update()
    {
       // bullet.AddForce(Vector2.left * bulletSpeed * Time.deltaTime);
    }
}
