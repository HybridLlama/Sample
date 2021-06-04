using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{

    #region Singleton & Awake

    private static CameraScripts _instance;

    public static CameraScripts Instance => _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    private bool canSwitch = true;
    [SerializeField] private float switchCooldown;

    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
    public Camera cam4;

    public Action OnCameraChanged;

    public Camera CurrentCamera { get; private set; }

    void ActivateCam(Camera camToActivate)
    {
        if(camToActivate == CurrentCamera)return;
        if(!canSwitch) return;
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(false);
        cam3.gameObject.SetActive(false);
        cam4.gameObject.SetActive(false);
        camToActivate.gameObject.SetActive(true);
        CurrentCamera = camToActivate;
        StartCoroutine(CameraCooldown());
        OnCameraChanged?.Invoke();
    }

    void Start()
    {
        CurrentCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("1Key"))
        {
            ActivateCam(cam1);
        }
        if (Input.GetButtonDown("2Key"))
        {
            ActivateCam(cam2);
        }
        if (Input.GetButtonDown("3Key"))
        {
            ActivateCam(cam3);
        }
        if (Input.GetButtonDown("4Key"))
        {
            ActivateCam(cam4);
        }
    }

    IEnumerator CameraCooldown()
    {
        canSwitch = false;
        yield return new WaitForSeconds(switchCooldown);
        canSwitch = true;
    }
}
