using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureManager : MonoBehaviour
{
    public static FurnitureManager _instance;

    // ---------------------------------------------------------------------------

    [SerializeField] private GameObject _bedPrefab;
    [SerializeField] private GameObject _bookshelfPrefab;
    [SerializeField] private GameObject _lBookshelfPrefab;
    [SerializeField] private GameObject _thinChairPrefab;
    [SerializeField] private GameObject _standardChairPrefab;
    [SerializeField] private GameObject _armChairPrefab;
    [SerializeField] private GameObject _couchPrefab;
    [SerializeField] private GameObject _lCouchPrefab;
    [SerializeField] private GameObject _lampPrefab;
    [SerializeField] private GameObject _nightStandPrefab;
    [SerializeField] private GameObject _deskPrefab;
    [SerializeField] private GameObject _lDeskPrefab;
    [SerializeField] private GameObject _rectangleTablePrefab;
    [SerializeField] private GameObject _roundTablePrefab;
    [SerializeField] private GameObject _squareTablePrefab;
    [SerializeField] private GameObject _wardrobePrefab;
    [SerializeField] private GameObject _twinWardrobePrefab;

    // ---------------------------------------------------------------------------

    private PlaceFurniture _placeFurnitureManager;

    private GameObject _selectedFurniture;
    private float _yPosition;

    private void Start()
    {
        _instance = this;
        _placeFurnitureManager = PlaceFurniture._instance;
        _selectedFurniture = null;
    }
    public void SetFurniture(string furniture)
    {
        _selectedFurniture = GetPrefab(furniture);
        //Debug.Log(_selectedFurniture);
        GameObject go = Instantiate(_selectedFurniture, new Vector3(0, _yPosition, 0), Quaternion.identity);
        go.tag = furniture;
        _placeFurnitureManager.SetFocusedFurniture(go, _yPosition);
    }

    private GameObject GetPrefab(string furniture)
    {
        switch (furniture)
        {
            case "BED":
                _yPosition = 0.7208573f;
                return _bedPrefab;
            case "BOOKSHELF1":
                _yPosition = 1.315745f;
                return _bookshelfPrefab;
            case "LBOOKSHELF2":
                _yPosition = 1.315745f;
                return _lBookshelfPrefab;
            case "THINCHAIR":
                _yPosition = 0.607114f;
                return _thinChairPrefab;
            case "STANDARDCHAIR":
                _yPosition = 0.577832f;
                return _standardChairPrefab;
            case "ARMCHAIR":
                _yPosition = 0.6858983f;
                return _armChairPrefab;
            case "COUCH":
                _yPosition = 0.6610652f;
                return _couchPrefab;
            case "LCOUCH":
                _yPosition = 0.6342078f;
                return _lCouchPrefab;
            case "LAMP":
                _yPosition = 0.7392887f;
                return _lampPrefab;
            case "NIGHTSTAND":
                _yPosition = 0.3606512f;
                return _nightStandPrefab;
            case "DESK":
                _yPosition = 0.7047516f;
                return _deskPrefab;
            case "LDESK":
                _yPosition = 0.4127157f;
                return _lDeskPrefab;
            case "RECTANGLETABLE":
                _yPosition = 0.4122286f;
                return _rectangleTablePrefab;
            case "ROUNDTABLE":
                _yPosition = 0.4299536f;
                return _roundTablePrefab;
            case "SQUARETABLE":
                _yPosition = 0.4389634f;
                return _squareTablePrefab;
            case "WARDROBE":
                _yPosition = 1.141306f;
                return _wardrobePrefab;
            case "TWINWARDDROBE":
                _yPosition = 1.284134f;
                return _twinWardrobePrefab;
        }
        return null;
    }

}
