using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTargetCam : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> targets;
    public Vector3 offset;
    private Vector3 velocity;
    public float smoothtime = 5f;

    public float minZoom = 2f;
    public float maxZoom = 6f;
    public float zoomLimiter = 10f;

    private GameObject abilityTarget;
    private bool abilitycut;

    public Camera cam;

    public void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }
    void LateUpdate()
    {
        if (targets.Count == 0)
        {
            while (abilitycut == true)
            {
                AbilityZoom();
            }          
                return;            
        }
          
        Move();
        Zoom();
       
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance() / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
        //Debug.Log(GetGreatestDistance());
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothtime);
    }

    void AbilityZoom()
    {
        transform.position = GetAbilityTarget(abilityTarget);
        cam.orthographicSize = 2;
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        
        }
        return bounds.size.x;
    }
    Vector3 GetCenterPoint()
    {
        if(targets.Count == 1)
        {
            return targets[0].position;
        }
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
            
        }

        return bounds.center;
    }

    public void Begin(float foreignfloat, GameObject foreigntarget)
    {
        StartCoroutine(AbilityCutTime(foreignfloat, foreigntarget));
    }

    Vector3 GetAbilityTarget(GameObject targetplayer)
    {
        targetplayer.GetComponent<Transform>();
        return targetplayer.transform.position;
    }

    public IEnumerator AbilityCutTime(float abilityLength, GameObject targetPlayer)
    {
        abilitycut = true;
        abilityTarget = targetPlayer;
        yield return new WaitForSeconds(10.0f);
        abilitycut = false;
    }
}
