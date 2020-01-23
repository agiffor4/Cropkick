using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchTimerScript : MonoBehaviour
{
    public float currCountdownValue;
    public Text canvasTimer;
    public GameManagerScript gmscript;
    
    // Start is called before the first frame update
    void Start()
    {
        currCountdownValue = gmscript.FullMatchTime;
        gmscript = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currCountdownValue <= 0)
        {
            gmscript.GameOver();
            print("GameOver");
        }
    }

    public IEnumerator StartCountdown()
    {

        while (currCountdownValue > 0)
        {
          //  Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
            canvasTimer.text = "" + currCountdownValue;
        }
    }


    public void TimerStart()
    {
        currCountdownValue = gmscript.FullMatchTime;
        StartCoroutine(StartCountdown());
    }

    public void TimerReset()
    {
        currCountdownValue = gmscript.FullMatchTime;
    }

    public void TimerStop()
    {
        StopCoroutine(StartCountdown());
            
    }
}
