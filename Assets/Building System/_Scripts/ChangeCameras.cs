using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameras : MonoBehaviour
{
    [SerializeField] GameObject[] _cameraParents;
    [SerializeField] Camera[] _cameras;

    private int _currentCamera;

    private void Start()
    {
        _cameraParents[1].SetActive(false);
        _cameraParents[2].SetActive(false);
        _cameraParents[3].SetActive(false);
        _cameras[1].enabled = false;
        _cameras[2].enabled = false;
        _cameras[3].enabled = false;

        _cameraParents[0].SetActive(true);
        _cameras[0].enabled = true;

        _currentCamera = 0;
    }

    public void CycleCameras()
    {
        _cameraParents[_currentCamera].SetActive(false);
        _cameras[_currentCamera].enabled = false;

        _currentCamera++;
        _currentCamera = _currentCamera % 4;

        _cameraParents[_currentCamera].SetActive(true);
        _cameras[_currentCamera].enabled = true;
    }

}
