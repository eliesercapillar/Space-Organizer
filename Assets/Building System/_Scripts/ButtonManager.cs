using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager _instance;

    [SerializeField] private RectTransform _leftSidePanel;
    [SerializeField] private RectTransform _rightSidePanel;

    [SerializeField] private GameObject _furnitureOptionsPanel;
    [SerializeField] private GameObject _furnitureTypesPanel;
    [SerializeField] private GameObject _toolTypesPanel;

    [SerializeField] private Animator _leftSidePanelAnimator;
    [SerializeField] private Animator _rightSidePanelAnimator;

    // ---------------------------------------------------------------------------

    [SerializeField] private Image _furnitureButtonImage;
    [SerializeField] private Image _toolsButtonImage;
    [SerializeField] private Button _furnitureButton;
    [SerializeField] private Button _toolsButton;

    [SerializeField] private Button _loadButton;
    [SerializeField] private Image _loadButtonImage;

    private bool _isFurnitureDisplayed;
    private bool _isToolsDisplayed;
    private bool _isLoadMenuDisplayed;

    // ---------------------------------------------------------------------------

    [SerializeField] private GameObject _loadScreenPanel;

    private Color _grey = new Color32(77, 94, 114, 255);
    private Color _blue = new Color32(63, 106, 138, 255);
    private Color _beige = new Color32(241, 230, 193, 255);
    private Color _yellow = new Color32(242, 204, 140, 255);
    private Color _pink = new Color32(221, 165, 182, 255);

    // ---------------------------------------------------------------------------

    [SerializeField] private Button _zoomButton;
    [SerializeField] private Image _zoomButtonImage;
    [SerializeField] private Slider _zoomSlider;

    [SerializeField] private GameObject _zoomPanel;

    private bool _inZoomMode;

    // ---------------------------------------------------------------------------
    [SerializeField] Image _handToolButtonImage;
    [SerializeField] Button _handToolButton;

    private bool _inHandMode;
    private ChangeCameras _cameraManager;
    private PlaceFurniture _furniturePlacer;

    // ---------------------------------------------------------------------------

    
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _zoomSlider.value = 5;
        _isFurnitureDisplayed = false;
        _isToolsDisplayed = false;
        _isLoadMenuDisplayed = false;
        _inZoomMode = false;
        _inHandMode = false;
        _cameraManager = ChangeCameras._instance;
        _furniturePlacer = PlaceFurniture._instance;
    }

    private void Update()
    {
        if (_inZoomMode)
        {
            Camera.main.orthographicSize = _zoomSlider.value;
        }
    }

    public void ZoomButton()
    {
        _inZoomMode = !_inZoomMode;

        if (_inZoomMode)
        {
            _zoomButtonImage.color = _pink;
            _furnitureOptionsPanel.SetActive(false);
            _zoomPanel.SetActive(true);

            if (_inHandMode)
            {
                _handToolButtonImage.color = _blue;
                _inHandMode = !_inHandMode;
                _cameraManager.GetCurrentCamera().SetHandTool(false);
                _furniturePlacer.StopRayCasts();
            }
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

    public void HandButton()
    {
        _inHandMode = !_inHandMode;

        if (_inHandMode)
        {
            _handToolButtonImage.color = _pink;
            _cameraManager.GetCurrentCamera().SetHandTool(true);
            _furniturePlacer.StopRayCasts();
            if (_inZoomMode)
            {
                ZoomButton();
            }
        }
        else
        {
            _handToolButtonImage.color = _blue;
            _cameraManager.GetCurrentCamera().SetHandTool(false);
            _furniturePlacer.StopRayCasts();
        }
    }

    public void SaveButton()
    {
        Debug.Log("Saved!");
    }

    public void LoadButton()
    {
        if (_isLoadMenuDisplayed)
        {
            _loadScreenPanel.SetActive(false);
            _loadButtonImage.color = _blue;

            _furnitureButton.interactable = true;
            _toolsButton.interactable = true;
        }
        else
        {
            _loadScreenPanel.SetActive(true);
            _loadButtonImage.color = _pink;

            _furnitureButton.interactable = false;
            _toolsButton.interactable = false;
        }
        _isLoadMenuDisplayed = !_isLoadMenuDisplayed;
    }

    public void CloseLoadScreen()
    {
        _loadScreenPanel.SetActive(false);
        _loadButtonImage.color = _blue;
        _isLoadMenuDisplayed = false;
        _furnitureButton.interactable = true;
        _toolsButton.interactable = true;
    }

    public void CameraButton()
    {
        Debug.Log("Camera!");
    }

    public void OrganizeButton()
    {
        Debug.Log("Organized!");
    }

    public void FurnitureButton()
    {
        _loadButton.interactable = false;
        _furnitureTypesPanel.SetActive(true);
        _toolTypesPanel.SetActive(false);
        if (_inHandMode)
        {
            HandButton();
        }    
        if (_isLoadMenuDisplayed)
        {
            _loadScreenPanel.SetActive(false);
            _loadButtonImage.color = _blue;
        }
        if (_isFurnitureDisplayed)
        {
            //Debug.Log("Furniture Was Currently Displayed. Now Hiding.");
            // Turn off the panels
            _furnitureButtonImage.color = _blue;
            _leftSidePanelAnimator.SetTrigger("Display");
            _rightSidePanelAnimator.SetTrigger("Display");
            _toolsButton.interactable = false;
            _furnitureButton.interactable = false;
            _loadButton.interactable = true;
            StartCoroutine(WaitForAnim());
        }
        else
        {
            //Debug.Log("Furniture Wasnt Currently Displayed. Now Displaying.");
            // Turn on the panels
            _furnitureButtonImage.color = _pink;
            if (_isToolsDisplayed)
            {
                _toolsButtonImage.color = _blue;
                _toolTypesPanel.SetActive(false);
                _isToolsDisplayed = false;
            }
            else
            {
                _rightSidePanelAnimator.SetTrigger("Display");
            }
            if (_inZoomMode)
            {
                _inZoomMode = false;
                _zoomButtonImage.color = _blue;
                _zoomPanel.SetActive(false);
                _furnitureOptionsPanel.SetActive(true);
            }
            else
            {
                _leftSidePanelAnimator.SetTrigger("Display");
            }
            _furnitureTypesPanel.SetActive(true);
            //_leftSidePanelAnimator.SetTrigger("Display");
            _toolsButton.interactable = false;
            _furnitureButton.interactable = false;
            StartCoroutine(WaitForAnim());
        }

        IEnumerator WaitForAnim()
        {
            yield return new WaitForSeconds(0.5f);
            if (_isFurnitureDisplayed)
            {
                _furnitureTypesPanel.SetActive(false);
            }
            _toolsButton.interactable = true;
            _furnitureButton.interactable = true;
            _isFurnitureDisplayed = !_isFurnitureDisplayed;
        }
    }

    public void ToolsButton()
    {
        _loadButton.interactable = false;
        _furnitureTypesPanel.SetActive(false);
        _toolTypesPanel.SetActive(true);
        if (_inZoomMode)
        {
            ZoomButton();
        }
        if (_isLoadMenuDisplayed)
        {
            _loadScreenPanel.SetActive(false);
            _loadButtonImage.color = _blue;
        }
        if (_isToolsDisplayed)
        {
            //Debug.Log("Tools Was Currently Displayed. Now Hiding.");
            // Turn off the panels
            if (_inHandMode)
            {
                HandButton();
            }

            _toolsButtonImage.color = _blue;
            _rightSidePanelAnimator.SetTrigger("Display");
            _toolsButton.interactable = false;
            _furnitureButton.interactable = false;
            _loadButton.interactable = true;
            StartCoroutine(WaitForAnim());
        }
        else
        {
            //Debug.Log("Tools Wasnt Currently Displayed. Now Displaying.");
            // Turn on the panels
            _toolsButtonImage.color = _pink;
            if (_isFurnitureDisplayed)
            {
                _furnitureButtonImage.color = _blue;
                _furnitureTypesPanel.SetActive(false);
                _isFurnitureDisplayed = false;
                _leftSidePanelAnimator.SetTrigger("Display");
            }
            else
            {
                _rightSidePanelAnimator.SetTrigger("Display");
            }
            _toolTypesPanel.SetActive(true);
            _toolsButton.interactable = false;
            _furnitureButton.interactable = false;
            StartCoroutine(WaitForAnim());
        }


        IEnumerator WaitForAnim()
        {
            yield return new WaitForSeconds(0.5f);
            if (_isToolsDisplayed)
            {
                _toolTypesPanel.SetActive(false);
            }
            _toolsButton.interactable = true;
            _furnitureButton.interactable = true;
            _isToolsDisplayed = !_isToolsDisplayed;
        }
    }

    public void InteractableButtons(bool enable)
    {
        if (enable)
        {
            _furnitureButton.interactable = true;
            _toolsButton.interactable = true;
            if (!_isFurnitureDisplayed && !_isToolsDisplayed)
            {
                _loadButton.interactable = true;
            }
        }
        else
        {
            _furnitureButton.interactable = false;
            _toolsButton.interactable = false;
            _loadButton.interactable = false;
        }
    }

    public bool IsFurnitureOpen()
    {
        return _isFurnitureDisplayed;
    }
}
