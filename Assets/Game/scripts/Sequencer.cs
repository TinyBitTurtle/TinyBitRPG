using UnityEngine;
using System;
using UnityEngine.Events;

public class Sequencer : MonoBehaviour
{
    public Sequence[] Sequences;

    [Serializable]
    public struct Sequence
    {
        public String name;
        public Segment[] segments;

    }

    [Serializable]
    public struct Segment
    {
        public float wait;
        public UnityEvent action;
    }
}
