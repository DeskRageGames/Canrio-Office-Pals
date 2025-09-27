using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{

    public Placeable placeable;
    [SerializeField] Camera cam;

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
    void Start()
    {
        if (cam==null && !TryGetComponent<Camera>(out cam))
        {
            Debug.LogError("Object Placer Requires a camera");
        }
        HandlePlacement(placeable);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            placeable.FinishPlace();
        }
    }

    private void FixedUpdate()
    {
        if(placeable==null)
        {
            enabled = false;
        }
        Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit info);
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

    void HandlePlacement(Placeable placeMe)
    {
        placeable = placeMe;
        placeable.OnPlaceObject.AddListener(FinishedPlacement);
        enabled = true;
    }

    void FinishedPlacement()
    {
        placeable = null;
    }
}
