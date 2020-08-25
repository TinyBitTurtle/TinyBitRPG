using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public partial class FSM : MonoBehaviour
{
    protected Animator animator;

    /// <summary>
    /// default state
    /// </summary>
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    
}