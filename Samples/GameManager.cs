using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will hold all necessary values of the game
/// </summary>
public class GameManager : MonoBehaviour
{

    #region Singleton & Awake

    private static GameManager _instance;

    public static GameManager Instance => _instance;

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


    public int TotalNotesInLevel { get; private set; }

    public int CurrentConsumedNotes { get; private set; }

    public Action OnLevelFinished;

    void Start()
    {
        Robot.Instance.OnNoteConsumed += AddNote;

        //Fine All objects in the scene that are tagged as "Note"
        TotalNotesInLevel = GameObject.FindGameObjectsWithTag("Note").Length;
    }

    /// <summary>
    /// Add a new note to our current counter and Check if it was the last one
    /// Invoke a Level Finished Event
    /// </summary>
    public void AddNote()
    {
        CurrentConsumedNotes++;
        if (CurrentConsumedNotes >= TotalNotesInLevel)
        {
            OnLevelFinished?.Invoke();
        }

    }


    public void RemoveNote()
    {
        CurrentConsumedNotes--;
        if (CurrentConsumedNotes < 0) CurrentConsumedNotes = 0;
    }
}
