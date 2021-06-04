using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class InteractablesRaycast : MonoBehaviour
{

    //Create Single Access Reference for Global Access
    #region Singleton & Awake
    private static InteractablesRaycast _instance;

    public static InteractablesRaycast Instance => _instance;

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


    private RaycastHit hit;
    private Ray ray;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float TimeToSleep = 30f;
    private float sleepTimer=0;

    private Camera m_camera;

    public Camera Camera { get; set; }

    private IInteractable currentInteractable;

    void Start()
    {
        UpdateCamera();
        CameraScripts.Instance.OnCameraChanged += UpdateCamera;
    }

    /// <summary>
    /// GetAccess to the currently active camera
    /// Gets Called when <see cref="CameraScripts.Instance.OnCameraChanged"/> event
    /// </summary>
    void UpdateCamera()
    {
        m_camera = CameraScripts.Instance.CurrentCamera;
    }

    void Update()
    {
        //Single Input Start Interaction
        if (Input.GetMouseButtonDown(0))
        {
            sleepTimer = 0;
            if (GameManager.Instance.CurrentConsumedNotes >0)
            {
                Robot.Instance.GetComponent<Animator>().SetTrigger("WakeUp");
            }
            if (CheckForInteraction()) currentInteractable.OnStartInteraction();
        }

        //Constant Object Interaction (For future Use)
        sleepTimer += Time.smoothDeltaTime;
        if (sleepTimer >= TimeToSleep)
        {
            Robot.Instance.GetComponent<Animator>().SetTrigger("GoToSleep");
        }
        if (!Input.GetMouseButton(0)) return;
        sleepTimer = 0;
        if (CheckForInteraction()) currentInteractable.Interact();

    }

    /// <summary>
    /// Send a ray forwards from the camera
    /// if and object is found in the Raycast Interactables layer we get its interface
    /// and Interact with it.
    /// </summary>
    /// <returns></returns>
    bool CheckForInteraction()
    {
        ray = m_camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 150, Color.red);
        if (!Physics.Raycast(ray, out hit, Mathf.Infinity, mask)) return false;
        if (!hit.collider.TryGetComponent<IInteractable>(out currentInteractable)) return false;
        if (!currentInteractable.CanInteract) return false;
        return !currentInteractable.IsInteracting;
    }

}
