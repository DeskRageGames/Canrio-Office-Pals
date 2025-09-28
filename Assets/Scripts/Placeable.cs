using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Placeable : MonoBehaviour
{
    internal UnityEvent OnPlaceObject;

    [SerializeField] int xTileSize = 1, zTileSize = 1;
    bool rotationTracker;
    [SerializeField] GameObject noPlace, yesPlace, placedPlace;
    Tile targetTile;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void SetTarget(Tile tile)
    {
        targetTile = tile;
        if (targetTile != null)
        {
            transform.position = targetTile.transform.position;
        }
        else
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Camera.main.transform.forward * 10f;
        }
        ValidateTilePlacement();
    }

    internal void Rotate()
    {
        transform.Rotate(transform.up, 90f);
        //z first

        int xSign = xTileSize / Mathf.Abs(xTileSize);
        int zSign = zTileSize / Mathf.Abs(zTileSize);

        int zStorage = zTileSize;
        zTileSize = Mathf.Abs(xTileSize) * zSign;
        xTileSize = Mathf.Abs(zStorage) * xSign;


        if (rotationTracker)
        {
            xTileSize *= -1;
        }
        else
        {
            zTileSize *= -1;
        }

        rotationTracker = !rotationTracker;
        ValidateTilePlacement();
    }

    internal void ValidateTilePlacement()
    {
        if (targetTile == null)
        {
            placedPlace.SetActive(false);
            noPlace.SetActive(true);
            yesPlace.SetActive(false);
            return;
        }
        if (targetTile.CanPlaceObject(xTileSize, zTileSize))
        {
            placedPlace.SetActive(false);
            noPlace.SetActive(false);
            yesPlace.SetActive(true);
            return;
        }
        placedPlace.SetActive(false);
        yesPlace.SetActive(false);
        noPlace.SetActive(true);
    }

    internal void FinishPlace()
    {
        if (yesPlace.activeSelf)
        {
            yesPlace.SetActive(false);
            placedPlace.SetActive(true);
            OnPlaceObject.Invoke();
            targetTile.PlaceObject(xTileSize, zTileSize);
            enabled = false;
        }
    }


}
