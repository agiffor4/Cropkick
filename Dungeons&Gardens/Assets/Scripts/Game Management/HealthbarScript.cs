using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour
{
    public Transform bar;
    // Start is called before the first frame update
    void Start()
    {
        Transform bar = transform.Find("Bar");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}
