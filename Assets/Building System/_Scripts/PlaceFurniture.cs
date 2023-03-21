using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlaceFurniture : MonoBehaviour
{
    public static PlaceFurniture _instance;

    [SerializeField] private GameObject _furnitureInFocus;
    [SerializeField] private Transform _parent;

    [SerializeField] private LayerMask _UImask;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _furnitureMask;
    [SerializeField] private float _lastPosY;

    private Renderer _rend;
    [SerializeField] private Material _matGrid, _matDefault;
    [SerializeField] private GameObject _gridOverlay;

    private string[] _furnitureTypes = {"BED", "BOOKSHELF1", "LBOOKSHELF2", "THINCHAIR", "STANDARDCHAIR", "ARMCHAIR", "COUCH", "LCOUCH", 
        "LAMP", "NIGHTSTAND", "DESK", "LDESK", "RECTANGLETABLE", "ROUNDTABLE", "SQUARETABLE", "WARDROBE", "TWINWARDDROBE"};

    private Vector3 _mousePos;
    private bool _wasLastObjectPlaced;
    private bool _stopRaycasts;

    private Stack<Vector3> vectorStack;
    private Stack<string> tagStack;
    private Vector3 possiblePush;
    private bool pushable;

    private Stack<Vector3> redoVectorStack;
    private Stack<string> redoTagStack;

    // --------------------------------------------------------------------

    private GameObject _previousFurniture;
    private Vector3 _previousFurniturePos;
    private bool _previousFurniturePosStored;

    // ---------------------------------------------------------------------------

    private ButtonManager _buttonManager;

    // ---------------------------------------------------------------------------

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        //_rend = GameObject.Find("Ground").GetComponent<Renderer>();
        _gridOverlay.SetActive(false);
        //_rend.material = _matDefault;
        _furnitureInFocus = null;
        _wasLastObjectPlaced = false;
        _stopRaycasts = false;
        vectorStack = new Stack<Vector3>();
        tagStack = new Stack<string>();
        redoVectorStack = new Stack<Vector3>();
        redoTagStack = new Stack<string>();
        pushable = false;
        _previousFurniturePosStored = false;
        _buttonManager = ButtonManager._instance;
    }

    void Update()
    {
        if (!_stopRaycasts)
        {
            if (_furnitureInFocus != null)
            {
                _mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(_mousePos);
                RaycastHit hit;

                if (!Physics.Raycast(ray, out hit, Mathf.Infinity, _UImask) && Physics.Raycast(ray, out hit, Mathf.Infinity, _groundMask))
                {
                    int posX = (int)Mathf.Round(hit.point.x);
                    int posZ = (int)Mathf.Round(hit.point.z);

                    _furnitureInFocus.transform.position = new Vector3(posX, _lastPosY, posZ);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _mousePos = Input.mousePosition;
                    Ray ray = Camera.main.ScreenPointToRay(_mousePos);
                    RaycastHit hit;

                    //Debug.Log("Attempting Raycast");
                    if (!Physics.Raycast(ray, out hit, Mathf.Infinity, _UImask) && Physics.Raycast(ray, out hit, Mathf.Infinity, _furnitureMask))
                    {
                        Debug.Log("We hit " + hit.collider.tag);
                        foreach (string furnitureType in _furnitureTypes)
                        {
                            if (hit.collider.tag.Contains(furnitureType))
                            {
                                _previousFurniture = hit.collider.gameObject;
                                _previousFurniturePos = hit.collider.gameObject.transform.position;
                                _previousFurniturePosStored = true;
                                SetFocusedFurniture(hit.collider.gameObject, hit.collider.gameObject.transform.position.y);
                                break;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            //Debug.Log("Raycasts currently stopped.");
        }
    }

    public void SetFocusedFurniture(GameObject obj, float yPos)
    {
        if (!_wasLastObjectPlaced)
        {
            if (_previousFurniturePosStored && _previousFurniture != null)
            {
                _previousFurniture.transform.position = _previousFurniturePos;
                _previousFurniturePosStored = false;
            }
            else
            {
                if (_furnitureInFocus != null)
                {
                    DeleteObj();
                }
            }
        }
        //_rend.material = _matGrid;
        _gridOverlay.SetActive(true);
        _furnitureInFocus = obj;
        if (!_buttonManager.IsFurnitureOpen())
        {
            _buttonManager.FurnitureButton();
            StartCoroutine(WaitForAnim());
        }
        _buttonManager.InteractableButtons(false);
        _lastPosY = yPos;
        _wasLastObjectPlaced = false;

        IEnumerator WaitForAnim()
        {
            yield return new WaitForSeconds(0.52f);
            _buttonManager.InteractableButtons(false);
        }

    }

    public void PlaceObj()
    {
        if (_furnitureInFocus != null)
        {
            GameObject furniture = Instantiate(_furnitureInFocus, _furnitureInFocus.transform.position, _furnitureInFocus.transform.rotation, _parent);
            furniture.name = _furnitureInFocus.name;
            _wasLastObjectPlaced = true;
            DeleteObj();
        }
        else
        {
            Debug.Log("No furniture in focus.");
        }
    }

    public void RotateObj()
    {
        if (_furnitureInFocus != null)
        {
            _furnitureInFocus.transform.Rotate(0, 90, 0);
        }
        else
        {
            Debug.Log("No furniture in focus.");
        }
    }

    public void DeleteObj()
    {
        Destroy(_furnitureInFocus);
        _furnitureInFocus = null;
        _previousFurniture = null;
        //_rend.material = _matDefault;
        _gridOverlay.SetActive(false);
        _buttonManager.InteractableButtons(true);
    }

    public void StopRayCasts()
    {
        _stopRaycasts = !_stopRaycasts;
    }

    public void Undo()
    {
        if (vectorStack.Count == 0)
        {
            Debug.Log("Nothing in stack.");
        }
        else if (vectorStack.Count >= 1)
        {
            Vector3 vectorPopped = vectorStack.Pop();
            string tagPopped = tagStack.Pop();

            redoVectorStack.Push(vectorPopped);
            redoTagStack.Push(tagPopped);

            foreach (Transform go in _parent)
            {
                if (go.tag == tagPopped)
                {
                    go.position = vectorPopped;
                    break;
                }
            }
        }
    }

    public void Redo()
    {
        if (redoVectorStack.Count == 0)
        {
            Debug.Log("Nothing in stack.");
        }
        else if (redoVectorStack.Count >= 1)
        {
            Vector3 vectorPopped = redoVectorStack.Pop();
            string tagPopped = tagStack.Pop();

            vectorStack.Push(vectorPopped);
            tagStack.Push(tagPopped);

            foreach (Transform go in _parent)
            {
                if (go.tag == tagPopped)
                {
                    go.position = vectorPopped;
                    break;
                }
            }
        }
    }
}
