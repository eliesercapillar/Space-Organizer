using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
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

    private bool _isFurnitureDisplayed;
    private bool _isToolsDisplayed;

    // ---------------------------------------------------------------------------

    [SerializeField] private GameObject _loadScreenPanel;

    private Color _grey = new Color32(77, 94, 114, 255);
    private Color _blue = new Color32(63, 106, 138, 255);
    private Color _beige = new Color32(241, 230, 193, 255);
    private Color _yellow = new Color32(242, 204, 140, 255);
    private Color _pink = new Color32(221, 165, 182, 255);

    private void Awake()
    {
        _isFurnitureDisplayed = false;
        _isToolsDisplayed = false;
    }

    public void SaveButton()
    {

    }

    public void LoadButton()
    {
        _loadScreenPanel.SetActive(true;
    }

    public void CloseLoadScreen()
    {
        _loadScreenPanel.SetActive(false);
    }

    public void CameraButton()
    {

    }

    public void OrganizeButton()
    {

    }

    public void FurnitureButton()
    {
        if (_isFurnitureDisplayed)
        {
            Debug.Log("Furniture Was Currently Displayed. Now Hiding.");
            // Turn off the panels
            _furnitureButtonImage.color = _blue;
            _leftSidePanelAnimator.SetTrigger("Display");
            _rightSidePanelAnimator.SetTrigger("Display");
            _toolsButton.interactable = false;
            _furnitureButton.interactable = false;
            StartCoroutine(WaitForAnim());
        }
        else
        {
            Debug.Log("Furniture Wasnt Currently Displayed. Now Displaying.");
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
            _furnitureTypesPanel.SetActive(true);
            _leftSidePanelAnimator.SetTrigger("Display");
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
        if (_isToolsDisplayed)
        {
            Debug.Log("Tools Was Currently Displayed. Now Hiding.");
            // Turn off the panels
            _toolsButtonImage.color = _blue;
            _rightSidePanelAnimator.SetTrigger("Display");
            _toolsButton.interactable = false;
            _furnitureButton.interactable = false;
            StartCoroutine(WaitForAnim());
        }
        else
        {
            Debug.Log("Tools Wasnt Currently Displayed. Now Displaying.");
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
}
