using System;
using UnityEngine;

public class SequenceCtrl<T> : MonoBehaviour
{
    public Sequence[] sequences;
    
    [Serializable]
    public class Sequence
    {
        public float offset;
        public float delay;
        public Action<T> preFunc;
        public Action<T> func;
        public int size;
    }

    public void AddItem()
    {

    }

    public void RemoveItem()
    {

    }

    public void Reset()
    {

    }
}
