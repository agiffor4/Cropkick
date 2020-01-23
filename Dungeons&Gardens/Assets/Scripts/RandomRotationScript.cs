using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotationScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, Random.Range(-180, 180));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
