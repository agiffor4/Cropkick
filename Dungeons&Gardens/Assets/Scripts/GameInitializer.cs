using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
   // public GlobalControl gcscript;

    int gp_player1char;
    int gp_player2char;
    int gp_player3char;
    int gp_player4char;
    int gp_playertotal;
    // Start is called before the first frame update
    void Start()
    {
       // gcscript = GameObject.FindWithTag("GControl").GetComponent<GlobalControl>();
        gp_player1char = GlobalControl.Instance.gc_player1char;
        gp_player2char = GlobalControl.Instance.gc_player2char;
        gp_player3char = GlobalControl.Instance.gc_player3char;
        gp_player4char = GlobalControl.Instance.gc_player4char;
        gp_playertotal = GlobalControl.Instance.gc_playertotal;
        checkValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkValues()
    {
        print(gp_player1char);
        print(gp_player2char);
        print(gp_player3char);
        print(gp_player4char);
        print(gp_playertotal);
    }
}
