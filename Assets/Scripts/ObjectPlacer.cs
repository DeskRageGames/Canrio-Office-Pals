using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPlacer : MonoBehaviour
{
    static ObjectPlacer _instance;
    public static ObjectPlacer instance;


    public Placeable placeable;
    [SerializeField] Camera cam;

    [SerializeField] private GameObject bottomBar;

    bool targetChanged = false;
    Tile _target;
    Tile target
    {
        set
        {
            if(value != _target)
            {
                targetChanged = true;
                _target = value;
            }
        }
        get
        {
            return _target;
        }
    }
    public LayerMask tileLayer;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {

        
        cam = Camera.main;

        if (cam==null && !TryGetComponent<Camera>(out cam))
        {
            Debug.LogError("Object Placer Requires a camera");
        }
        if(placeable != null) HandlePlacement(placeable);
    }

    // Update is called once per frame
    void Update()
    {
        if(placeable == null) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ConfirmPlacement();
        }
        
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            Vector3 position = Vector3.zero;

            if (Input.GetMouseButtonDown(0))
            {
                position = Input.mousePosition;
            }
            else if (Input.touchCount > 0)
            {
                position = Input.GetTouch(0).position;
            }
            else
            {
                Debug.LogError("Input and no input detected, how?");
            }
            
            Physics.Raycast(cam.ScreenPointToRay(position), out RaycastHit info);
            
            if(info.collider == null) return;
            
            //Debug.DrawRay(cam.ScreenToWorldPoint(Input.mousePosition), cam.transform.forward*info.distance, Color.red, 2f);
            if(info.collider!=null && info.collider.TryGetComponent<Tile>(out Tile newTile))
            {
                target = newTile;
            }
            else
            {
                target = null;
            }
            if (targetChanged || target==null)
            {
                placeable.SetTarget(target);
                targetChanged = false;
            }
        }
    }

    public void Rotate()
    {
        placeable.Rotate();
    }

    public void Cancel()
    {
        Destroy(placeable.gameObject);
        bottomBar.SetActive(false);
        FindObjectOfType<ShopMenu>().Refund();
    }

    public void HandlePlacement(Placeable placeMe)
    {
        placeable = placeMe;
        placeable.OnPlaceObject.AddListener(FinishedPlacement);
        bottomBar.SetActive(true);
        enabled = true;

    }

    void FinishedPlacement()
    {
        placeable = null;
        bottomBar.SetActive(false);
    }

    public void ConfirmPlacement()
    {
        placeable.FinishPlace();
    }
}
