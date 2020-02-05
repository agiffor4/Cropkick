using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectFarm()
    {
        SceneManager.LoadScene("PrototypeFarmStage");
    }

    public void selectRiver()
    {
        print("Unfinished! Please be patient!");
    }

    public void selectCrystal()
    {
        SceneManager.LoadScene("PrototypeCrystalStage");
    }

    public void selectVolcano()
    {
        print("Unfinished! Please be patient!");
    }



  
}
