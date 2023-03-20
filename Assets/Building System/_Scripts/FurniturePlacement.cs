using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurniturePlacement : MonoBehaviour
{
    GameObject _objToMove;
    [SerializeField] private GameObject _objToPlace;
    private GameObject _parent;

    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _lastPosY;
    Vector3 _mousePos;

    private Renderer _rend;
    [SerializeField] private Material _matGrid, _matDefault;

    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _objToMove = this.gameObject;
        _parent = GameObject.Find("Environment/Furnitures");

        _rend = GameObject.Find("Ground").GetComponent<Renderer>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        _mousePos = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(_mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _mask))
        {
            int posX = (int)Mathf.Round(hit.point.x);
            int posZ = (int)Mathf.Round(hit.point.z);

            _objToMove.transform.position = new Vector3(posX, _lastPosY, posZ);
            _rend.material = _matGrid;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject go = Instantiate(_objToPlace, _objToMove.transform.position, Quaternion.identity, _parent.transform);
            go.tag = this.gameObject.tag;
            Destroy(this.gameObject);

            _rend.material = _matDefault;
        }
    }
}
