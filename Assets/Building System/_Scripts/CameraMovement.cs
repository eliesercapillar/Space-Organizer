using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement _instance;

    private bool _handToolSelected;
    [SerializeField] private bool _isMoving;
    [SerializeField] private float _movementSpeed;

    private Vector3 _previousFrameMousePos;
    private Vector3 _mouseDelta;

    private Vector3 _defaultPos;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _isMoving = false;
        _defaultPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsMouseHovering._mouseOverUIElement)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _previousFrameMousePos = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                _mouseDelta = Input.mousePosition - _previousFrameMousePos;

                _previousFrameMousePos = Input.mousePosition;
            }
            if (_handToolSelected && Input.GetMouseButton(0))
            {
                _isMoving = true;
            }
            else
            {
                _isMoving = false;
            }
        }
    }

    private void LateUpdate()
    {
        if (_isMoving)
        {
            Vector3 position = transform.right * (_mouseDelta.x * -_movementSpeed);
            position += transform.up * (_mouseDelta.y * -_movementSpeed);
            transform.position += position * Time.deltaTime;
        }
    }

    public void SetHandTool(bool b)
    {
        _handToolSelected = b;
    }

    public void ResetCamera()
    {
        this.transform.position = _defaultPos;
        Camera.main.orthographicSize = 5;
    }
}
