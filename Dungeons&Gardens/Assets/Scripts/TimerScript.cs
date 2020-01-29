
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float TimeD = 0;
    public float time { get; private set; } //you can accsess the current time of the timer via varName.time (read only)
    public float timeReset { get; private set; }//you can accsess the current Reset time of the timer via varName.timeReset (read only)

    public float[] TimerArray;
    public float[] TimerSetArray;
    public int timerMaxSize = 10;
    // Start is called before the first frame update
    void Start()
    {
        CheckTimers();
        TimerArray = new float[timerMaxSize];
        for (int i = 0; i < TimerArray.Length; i++)
        {
           // TimerArray[i] = 0;
          //  Debug.Log("Timer number " + i + " equals " + TimerArray[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < TimerArray.Length; i++)
        {
            if((TimerArray[i] != 0))
            {
                Count(i);
                if(TimerArray[i] <= 0)
                {
                    //send signal
                }
            }
        }

         bool Count(int arrayOrder)
        {
            TimerArray[arrayOrder] -= Time.deltaTime;
         
            if (TimerArray[arrayOrder] <= 0)
            {
                time = timeReset;
                return true;
            }
            return false;

        }

    }
    public bool CountDown(int arrayOrder)
    {
        Debug.Log("Time Start?");
        TimerArray[arrayOrder] -= Time.deltaTime;
        Debug.Log("Time Go?");
        if (TimerArray[arrayOrder] < 0)
        {

            TimerArray[arrayOrder] = timeReset;
            Debug.Log("Time Complete");
            return true;
        }
        return false;

    }

    public void CheckTimers()
    {
        for (int i = 0; i < TimerArray.Length; i++)
        {
            if (TimerArray[i] == 0)
            {
                //start timer i
                //TimerArray[i] = startvalue;
                // CountDown(i);
                FillTimer(i);
                Debug.Log("Timer " + i + " has started");
                
            }
            else
            {
                Debug.Log("Timer " + i + " is busy counting at " + TimerArray[i] + ", moving to timer" + (i + 1));
              
            
            }
        }
    }

    public void StartTimer(float timefromsource)
    {
        CheckTimers();
        
    }

    public void FillTimer(int timerOrder)
    {
     //   TimerArray(timerOrder) = 10;
    }
}



/*

    // must be declared in script to use via Timer varName = new Timer();
    //in Start() varName needs a default time set i.e. varName.SetTimer(float);
    //passing in anumber below zero to the constructor will result in the timer counting but never being true or resetting.
    public Timer(float resetTime = 1.0f)
    {
        timeReset = resetTime;
        time = timeReset;
    }
    bool isActive = false;
    public float TimeD = 0;
    public float time { get; private set; } //you can accsess the current time of the timer via varName.time (read only)
    public float timeReset { get; private set; }//you can accsess the current Reset time of the timer via varName.timeReset (read only)

    public void SetTimer(float resetTime)
    { //starts the timer from the max time and resets max time
        timeReset = resetTime;
        time = timeReset;
    }

    public void ReduceTimeRelative(float val)
    { //adjusts the current time without changing the reset time;
        time -= val;
    }

    public void AddTimeRelative(float val)//adjusts the current time without changing the reset time; i.e. extra time boost
    {
        time += val;
    }

    /*
    Used in an update loop in unity
    if(time.CountDown()){
        //dostuff
    }

    public bool CountDown()
    {
        time -= Time.deltaTime;
        TimeD = time;
        if (timeReset > 0 && time <= 0)
        {
            time = timeReset;
            return true;
        }
        return false;

    }

    public bool CountUp()
    {
        time += Time.deltaTime;

        if (timeReset > 0 && time >= timeReset)
        {
            time = 0;
            return true;
        }
        return false;

    }

    //varaition of above, if the timer should only count down once use this
    public bool CountDownAutoCheckBool()
    {
        if (isActive)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = timeReset;
                isActive = false;
                return true;
            }
        }
        return false;

    }

    public bool CountUpAutoCheckBool()
    {
        if (isActive)
        {
            time += Time.deltaTime;
            if (time >= timeReset)
            {
                time = 0;
                isActive = false;
                return true;
            }
        }
        return false;

    }

    //checks if the timer count down bool is true

    public bool ShouldCountDown()
    {
        return isActive;
    }
    //sets if the timer should count down
    public void SetTimerShouldCountDown(bool val)
    {
        isActive = val;
    }

    //sets the current time for the timer, mostly for loading from save purposes
    public void SetCurrentTime(float val)
    {
        time = val;
    }

    public void GetAsMinutesAndSecond(out int min, out int sec)
    {
        sec = (int)(time % 60);
        min = (int)((time - sec) / 60);
    }
    public string GetTimeAsString()
    {
        int m;
        int s;
        GetAsMinutesAndSecond(out m, out s);
        if (s < 10)
        {
            return m.ToString() + ":0" + s.ToString();
        }
        else
        {
            return m.ToString() + ":" + s.ToString();
        }

}
    }
*/


