using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractNodeScript : MonoBehaviour
{
    [SerializeField] private float Nodelifespan;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Cleaner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Cleaner()
    {
        yield return new WaitForSeconds(Nodelifespan);
        Destroy(gameObject);
    }
}
