using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnStateEnter : StateMachineBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private bool isLoopAnim;

    private AudioSource src;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        src = animator.GetComponent<AudioSource>();
        src.Stop();
        if (!isLoopAnim)
        {
            int arrayIndex = Random.Range(0, clips.Length);
            src.PlayOneShot(clips[arrayIndex]);
        }
        else
        {
            int arrayIndex = Random.Range(0, clips.Length);
            src.loop = true;
            src.clip = clips[arrayIndex];
            src.Play();
        }

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isLoopAnim) return;
        if(src.isPlaying)return;
        int arrayIndex = Random.Range(0, clips.Length);
        src.clip = clips[arrayIndex];
        src.Play();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!isLoopAnim)return;
        src.loop = false;
        src.clip = null;
    }
}
