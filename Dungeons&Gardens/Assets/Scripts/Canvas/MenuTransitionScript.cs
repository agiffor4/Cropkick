using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTransitionScript : MonoBehaviour
{
    public GameObject cklogo;
    public GameObject creditspanel;
    public GameObject settingspanel;
    public bool transitioning = false;
    private MainMenuScript mmscript;
    [SerializeField] private CanvasGroup canvasG;
    [SerializeField] private GameObject menuCameraTarget;
    [SerializeField] private Transform defaultTarget;
    [SerializeField] private Transform creditsTarget;
    [SerializeField] private Transform settingsTarget;
    [SerializeField] private GameObject eventmanager;

    public int currSet;
    public int nextSet;
    // Start is called before the first frame update
    void Start()
    {
        currSet = 0;
        nextSet = 0;
        mmscript = gameObject.GetComponent<MainMenuScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transitioning == true)
        {
            eventmanager.SetActive(false);
            canvasG.alpha -= 2 * Time.deltaTime;

        }else if(transitioning == false && canvasG.alpha != 1)
        {
            canvasG.alpha += 1 * Time.deltaTime;
        }

        if(canvasG.alpha > 1)
        {
            canvasG.alpha = 1;
        }

       
    }

    public void MoveCameraTarget(string type)
    {
       
        if(type == "Credits")
        {
            if (CheckSelf(1) == true)
            {
                transitioning = true;
                menuCameraTarget.transform.position = new Vector2(creditsTarget.position.x, creditsTarget.position.y);
                StartCoroutine(TransitionToCredits());
                currSet = 1;
            }
            else
            {
                Debug.Log("HERE HERE HERE");
            }
        } else if (type == "Settings")
        {
            if (CheckSelf(2) == true)
            {
                transitioning = true;
                menuCameraTarget.transform.position = new Vector2(settingsTarget.position.x, settingsTarget.position.y);
                StartCoroutine(TransitionToSettings());
                currSet = 2;
            }
           
        }
        else if (type == "Main")
        {          
            if (CheckSelf(0) == true)
            {
                transitioning = true;
                menuCameraTarget.transform.position = new Vector2(defaultTarget.position.x, defaultTarget.position.y);
                StartCoroutine(TransitionToMain());
                currSet = 0;
            }          
        }
        else
        {
            Debug.Log("ERROR: MOVECAMERATARGET STRING NAME NOT FOUND/ INCORRECT");
        }
        //transitioning = false;
    }

    public bool CheckSelf(int selectedvalue)
    {
        nextSet = selectedvalue;
        if(currSet == nextSet)
        {    
            return false;
        }
        else
        {

            return true;
        }
        
    }

    public void ResetMenuType()
    {
        cklogo.SetActive(false);
        creditspanel.SetActive(false);
        settingspanel.SetActive(false);
    }

    public IEnumerator TransitionToCredits()
    {
        yield return new WaitForSeconds(2f);
        ResetMenuType();
        creditspanel.SetActive(true);
        transitioning = false;
        eventmanager.SetActive(true);
    }

    public IEnumerator TransitionToMain()
    {
        yield return new WaitForSeconds(1.2f);
        ResetMenuType();
        cklogo.SetActive(true);
        transitioning = false;
        eventmanager.SetActive(true);
    }

    public IEnumerator TransitionToSettings()
    {
        yield return new WaitForSeconds(1.2f);
        ResetMenuType();
        settingspanel.SetActive(true);
        transitioning = false;
        eventmanager.SetActive(true);
    }
}
