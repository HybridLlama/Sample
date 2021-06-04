using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Robot : MonoBehaviour, IInteractable
{

    //Create Single Access Reference for Global Access
    #region Singleton & Awake
    private static Robot _instance;

    public static Robot Instance => _instance;

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

    //Animation Hashes
    private readonly int anim_wakeUp = Animator.StringToHash("WakeUp");
    private readonly int anim_dance = Animator.StringToHash("Dance");

    private Animator robotAnimator;

    [Header("AUDIO")] 
    [SerializeField] private AudioClip[] audio_sleep;
    [SerializeField] private AudioClip[] audio_asleep;
    [SerializeField] private AudioClip[] audio_idle;
    [SerializeField] private AudioClip[] audio_sadIdle;
    [SerializeField] private AudioClip[] audio_wakeUp;
    [SerializeField] private AudioClip audio_dance;
    private AudioSource src;


    //This event is fired when a note is Consumed by the Robot;
    public Action OnNoteConsumed;

    public bool CanInteract { get; private set; } = false;

    public bool IsInteracting { get; private set; }

    void Start()
    {
        robotAnimator = GetComponent<Animator>();
        src = GetComponent<AudioSource>();
    }


    public void Interact()
    {

    }

    public void OnStartInteraction()
    {
        OnNoteConsumed?.Invoke();

        //Play the appropriate animation based on currentNotes
        switch (GameManager.Instance.CurrentConsumedNotes)
        {
            case 1:
                robotAnimator.ResetTrigger(anim_wakeUp);
                robotAnimator.SetTrigger(anim_wakeUp);
                break;
            default:
                robotAnimator.ResetTrigger(anim_dance);
                robotAnimator.SetTrigger(anim_dance);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Note")) return;
        OnStartInteraction();
        other.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Note"))return;
        Interact();
    }

}
