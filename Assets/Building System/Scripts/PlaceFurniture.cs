using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceFurniture : MonoBehaviour
{
    public static PlaceFurniture _instance;

    [SerializeField] private GameObject _objToMove;
    [SerializeField] private GameObject _objToPlace;

    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _lastPosY;

    private Vector3 _mousePos;
    private Camera _camera;

    //[SerializeField] private Renderer _rend;
    //[SerializeField] private Material _matGrid, matDefault;

    void Awake()
    {
        _instance = this;
        _camera = Camera.main;
    }

    void Start()
    {
        
    }

    void Update()
    {
        _mousePos = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(_mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _mask))
        {
            int posX = (int) Mathf.Round(hit.point.x);
            int posZ = (int) Mathf.Round(hit.point.z);

            _objToMove.transform.position = new Vector3(posX, _lastPosY, posZ);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(_objToPlace, _objToMove.transform.position, Quaternion.identity);
        }
    }
}
