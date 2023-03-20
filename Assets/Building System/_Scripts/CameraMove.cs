using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Button _zoomButton;
    [SerializeField] private Image _handButtonImage;
    [SerializeField] private Image _zoomButtonImage;
    [SerializeField] private Slider _zoomSlider;
    [SerializeField] private float _moveSpeed = 0.000000000000000000000000000000000000000000000001f;

    [SerializeField] private GameObject _furnitureOptionsPanel;
    [SerializeField] private GameObject _zoomPanel;

    [SerializeField] private Animator _leftSidePanelAnimator;

    private Vector3 _origin;
    private Vector3 _difference;
    private Vector3 _resetCamera;

    //private Vector2 _lastMousePosition;

    private bool _drag = false;
    private bool _inDragMode = false;
    private bool _inZoomMode = false;

    private Color _pink = new Color32(221, 165, 182, 255);
    private Color _blue = new Color32(63, 106, 138, 255);

    private PlaceFurniture _placeFurnitureManager;

    public static CameraMove _instance;

    private void Awake()
    {
        _instance = this;    
    }

    private void Start()
    {
        _resetCamera = _cameraPivot.position;
        _placeFurnitureManager = PlaceFurniture._instance;
    }
 

    private void Update()
    {
        //Debug.Log("Input.touchCount == 1 results in: " + (Input.touchCount == 1));
        //Debug.Log("Dragmode is " + _inDragMode);

        if (Input.GetMouseButtonDown(0) && _inDragMode)
        {
            Debug.Log("Calculating difference");
            _difference = Input.mousePosition - Camera.main.transform.position;
            Debug.Log("Difference is: " + _difference);
            if (_drag == false)
            {
                _drag = true;
                _origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                _drag = false;
            }

            if (_drag)
            {
                Debug.Log("Setting new position");
                Vector3 position = _cameraPivot.position;
                position.x = _origin.x - _difference.x;
                position.y = _origin.y - _difference.y;

                _cameraPivot.position = position;
            }
        }
    }

    public void ResetCamera()
    {
        _cameraPivot.position = _resetCamera;
    }

    public void SetDragMode()
    {
        _inDragMode = !_inDragMode;
        if (_inZoomMode)
        {
            SetZoom();
        }
        _placeFurnitureManager.StopRayCasts();

        if (_inDragMode)
        {
            _handButtonImage.color = _pink;
        }
        else
        {
            _handButtonImage.color = _blue;
        }
    }

    public void SetZoom()
    {
        _inZoomMode = !_inZoomMode;

        if (_inDragMode)
        {
            _handButtonImage.color = _blue;
            _inDragMode = false;
        }

        if (_inZoomMode)
        {
            _zoomButtonImage.color = _pink;
            _furnitureOptionsPanel.SetActive(false);
            _zoomPanel.SetActive(true);
        }
        else
        {
            _zoomButtonImage.color = _blue;
        }
        _zoomButton.interactable = false;

        _leftSidePanelAnimator.SetTrigger("Display");
        StartCoroutine(WaitForAnim());

        IEnumerator WaitForAnim()
        {
            yield return new WaitForSeconds(0.5f);
            _zoomButton.interactable = true;
            if (!_inZoomMode)
            {
                _furnitureOptionsPanel.SetActive(true);
                _zoomPanel.SetActive(false);
            }
        }
    }

    public bool isZoomOn()
    {
        return _inZoomMode;
    }
}
