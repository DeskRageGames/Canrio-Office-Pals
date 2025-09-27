using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] bool run = false;
#endif

    
    public static float tileScale = 3f;

    public bool isOccupied;
    [SerializeField] Tile[] _tiles;
    //tiles[0] is forwards, tiles[1] is rightwards, tiles[2] is backwards, tiles[3] is leftwards (or North East South West if you think as I do)
    public Tile[] tiles
    {
        get
        {
            if (_tiles != null && _tiles.Length == 4)
                return _tiles;
            SetupTiles();
            return _tiles;
        }
    }

    /// <summary>
    /// Can an object that is x units wide by z units long fit on this spot? Assuming this spot is a corner.
    /// Rotations make x or z negative
    /// </summary>
    /// <param name="x">Tile Size of the object in the x direction, can be negative</param>
    /// <param name="z">Tile Size of the object in the z direction, can be negative</param>
    /// <returns></returns>
    public bool CanPlaceObject(int x, int z)
    {
        if (x == 0 || z == 0)
            return false;
        int xDirection = x < 0 ? 3 : 1;
        int zDirection = z < 0 ? 2 : 0;
        x = Mathf.Abs(x);
        z = Mathf.Abs(z);
        Debug.Log(x);
        Debug.Log(z);
        for (int i = 0; i < x; i++)
        {
            Tile xTile = GetTileInLine(xDirection, i);
            if(xTile == null || xTile.isOccupied)
            {
                return false;
            }
            for(int j = 0; j < z; j++)
            {
                Tile zTile = xTile.GetTileInLine(zDirection, j);
                if (zTile == null || zTile.isOccupied)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public Tile GetTileInLine(int direction, int count)
    {
        Tile target = this;
        for(int i = 0; i < count; i++)
        {
            if (target != null)
                target = target.tiles[direction];
            else
                break;
        }
        return target;
    }

    public static int StringDirectionToIntDirection(string direction)
    {
        if (direction.Length < 1)
            return -1;
        char first = direction.ToLower()[0];
        switch (first)
        {
            case 'n':
            case 'f':
                return 0;
            case 'e':
            case 'r':
                return 1;
            case 's':
            case 'b':
                return 2;
            case 'w':
            case 'l':
                return 3;
            default:
                return -1;
        }
    }

    void SetupTiles()
    {
        _tiles = new Tile[4];
        Collider[] hits = Physics.OverlapSphere(transform.position + Vector3.forward * tileScale, 0.01f);
        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent<Tile>(out _tiles[0]))
                break;
        }
        hits = Physics.OverlapSphere(transform.position + Vector3.right * tileScale, 0.01f);
        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent<Tile>(out _tiles[1]))
                break;
        }
        hits = Physics.OverlapSphere(transform.position + Vector3.back * tileScale, 0.01f);
        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent<Tile>(out _tiles[2]))
                break;
        }
        hits = Physics.OverlapSphere(transform.position + Vector3.left * tileScale, 0.01f);
        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent<Tile>(out _tiles[3]))
                break;
        }
    }

    internal void PlaceObject(int x, int z)
    {
        if (x == 0 || z == 0)
            return;
        int xDirection = x < 0 ? 3 : 1;
        int zDirection = z < 0 ? 2 : 0;
        x = Mathf.Abs(x);
        z = Mathf.Abs(z);
        for (int i = 0; i < x; i++)
        {
            Tile xTile = GetTileInLine(xDirection, i);
            if (xTile == null || xTile.isOccupied)
            {
                Debug.LogError("Attempted Illegal Placement");
                return;
            }
            for (int j = 0; j < z; j++)
            {
                Tile zTile = xTile.GetTileInLine(zDirection, j);
                if (zTile == null || zTile.isOccupied)
                {
                    Debug.LogError("Attempted Illegal Placement");
                    return;
                }
                zTile.isOccupied = true;
            }
            xTile.isOccupied = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (run)
        {
            run = false;
            SetupTiles();
        }
#endif
    }
}
