using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float cropAge;
    [SerializeField] private bool readytoHarvest;

    public Sprite spriteEmpty;
    public Sprite spriteSeeded;
    public Sprite spriteGrowthLV1;
    public Sprite spriteGrowthLV2;
    public Sprite spriteGrowthLV3;

    public GameObject rendPlotObject;
    public SpriteRenderer rendPlotSprite;
    private Sprite currPlotSprite;

    public GameObject droppedcrops;
    public int cropyield;
    public Transform spawnpoint;


    [SerializeField] private bool growthtimer;
    [SerializeField] private bool isWatered;
    [SerializeField] private int minYield = 2;
    [SerializeField] private int maxYield = 5;


    //public PlotCropAsset plotAsset;

    void Start()
    {
        spawnpoint.position = transform.position;
        rendPlotSprite = rendPlotObject.GetComponent<SpriteRenderer>();
        readytoHarvest = false;
        growthtimer = false;
        isWatered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWatered == true)
        {
            gameObject.tag = ("PlotW");
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            CheckPlotState();
        }

        if (growthtimer == true)
        {
            cropAge += 1 * Time.deltaTime;
            //Debug.Log(cropAge);
        }

        if (cropAge >= 5.0f && cropAge <= 9.9)
        {
            if (isWatered == false)
            {
                growthtimer = false;
            }
            plotState = PlotCropState.Young;
            CheckPlotState();
        }
        if (cropAge >= 10.0f && cropAge <= 14.9)
        {
            plotState = PlotCropState.Medium;
            CheckPlotState();
        }
        if (cropAge >= 20.0f)
        {
            plotState = PlotCropState.Ready;
            CheckPlotState();
        }

    }


    public enum PlotCropState
    {
        Empty,
        Seeded,
        Young,
        Medium,
        Ready,
    }


    public PlotCropState plotState = PlotCropState.Empty;
    // public Sprite GetCropSprite()
    // {
    // if (asset == null)
    //     return null;

    public void CheckPlotState()
    {


        switch (plotState)
        {
            case PlotCropState.Empty:
                readytoHarvest = false;
                growthtimer = false;
                isWatered = false;
                cropAge = 0;
                rendPlotSprite.sprite = spriteEmpty;
                gameObject.tag = "PlotE";
                //return spriteEmpty;
                break;
            case PlotCropState.Seeded:
                growthtimer = true;
                rendPlotSprite.sprite = spriteSeeded;
                gameObject.tag = "PlotS";
                //   return spriteSeeded;
                break;
            case PlotCropState.Young:
                rendPlotSprite.sprite = spriteGrowthLV1;
                gameObject.tag = "PlotUW";
                // return spriteGrowthLV1;
                break;
            case PlotCropState.Medium:
                rendPlotSprite.sprite = spriteGrowthLV2;
                // gameObject.tag = "PlotLV2";
                // return spriteGrowthLV2;
                break;
            case PlotCropState.Ready:
                readytoHarvest = true;
                rendPlotSprite.sprite = spriteGrowthLV3;
                gameObject.tag = "PlotR";
                break;
                // return spriteGrowthLV3;
        }



        //   Debug.LogError("Something is null");
        //  return spriteEmpty;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "InteractNode")
        {
            if (plotState == PlotCropState.Empty)
            {
                plotState = PlotCropState.Seeded;
                CheckPlotState();
            }
            else if (readytoHarvest == true)
            {
                for (int i = 0; i < Random.Range(minYield, maxYield); i++)
                {
                    Instantiate(droppedcrops, gameObject.transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0), Quaternion.identity);
                }
                plotState = PlotCropState.Empty;
                CheckPlotState();
            }
        }

        if (other.tag == "InteractNodeS")
        {
            plotState = PlotCropState.Seeded;
            CheckPlotState();
        }

        if (other.tag == "InteractNodeW")
        {
            if (isWatered == false)
            {
                isWatered = true;
                growthtimer = true;
                cropAge += 1;
                CheckPlotState();
            }

        }

        if (other.tag == "InteractNodeH")
        {
            if (readytoHarvest == true)
            {
                for (int i = 0; i < Random.Range(minYield, maxYield); i++)
                {
                    Instantiate(droppedcrops, gameObject.transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0), Quaternion.identity);
                }
                plotState = PlotCropState.Empty;
                CheckPlotState();
            }
        }

    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "GrowthRadius" && growthtimer == true)
        {
            cropAge += 0.1f;
        }

    }
}

