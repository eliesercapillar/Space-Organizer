using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private GameObject _objToPlace;
    private GameObject _parent;

    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _lastPosY;
    Vector3 _mousePos;

    private Renderer _rend;
    [SerializeField] private Material _matGrid, _matDefault;

    private Camera _camera;
    private bool _isDragging;

    // Start is called before the first frame update
    void Start()
    {
        _parent = GameObject.Find("Environment/Furnitures");

        _rend = GameObject.Find("Ground").GetComponent<Renderer>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _isDragging = true;
            _mousePos = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(_mousePos);
            RaycastHit hit;

            Debug.Log("Attempting Raycast");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _mask))
            {
                Debug.Log("We hit " + hit.collider.tag);

                int posX = (int)Mathf.Round(hit.point.x);
                int posZ = (int)Mathf.Round(hit.point.z);

                _objToPlace.transform.position = new Vector3(posX, _lastPosY, posZ);
            }
        }
        else
        {
            _isDragging = false;
        }

        if (_isDragging)
        {
            _rend.material = _matGrid;
        }
        else
        {
            _rend.material = _matDefault;
        }
    }
}
