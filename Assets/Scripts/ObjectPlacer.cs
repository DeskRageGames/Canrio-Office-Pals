using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPlacer : MonoBehaviour
{
    static ObjectPlacer _instance;
    public static ObjectPlacer instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = Camera.main.gameObject.AddComponent<ObjectPlacer>();
            }
            return _instance;
        }
    }


    public Placeable placeable;
    [SerializeField] Camera cam;
    [SerializeField] public UnityEvent OnFinishPlacing;
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

    public void Rotate()
    {
        placeable.Rotate();
    }

    public void Cancel()
    {
        Destroy(placeable.gameObject);
        OnFinishPlacing.Invoke();
    }

    public void HandlePlacement(Placeable placeMe)
    {
        placeable = placeMe;
        placeable.OnPlaceObject.AddListener(FinishedPlacement);
        enabled = true;
    }

    void FinishedPlacement()
    {
        placeable = null;
        OnFinishPlacing.Invoke();
    }
}
