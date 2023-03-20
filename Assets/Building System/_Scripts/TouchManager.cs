using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private Transform _prefab;

    [SerializeField] private PlayerInput _playerInput;
    private InputAction _touchPositionAction;
    private InputAction _touchPressAction;

    private Camera _camera;

    private void Awake()
    {
        _touchPositionAction = _playerInput.actions["TouchPosition"];
        _touchPressAction = _playerInput.actions["TouchPress"];

        _camera = Camera.main;
}

    private void OnEnable()
    {
        _touchPressAction.performed += TouchPressed;
    }

    private void OnDisable()
    {
        _touchPressAction.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        /*float value = context.ReadValue<float>();
        Debug.Log("The Value is: " + value); */

        Debug.Log(_touchPositionAction.ReadValue<Vector2>());
        /*
        Vector3 position = _camera.ScreenToWorldPoint(_touchPositionAction.ReadValue<Vector2>());
        position.z = _prefab.position.z;
        _prefab.transform.position = position; */

    }
}
