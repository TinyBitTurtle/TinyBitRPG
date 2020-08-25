using UnityEngine;

namespace TinyBitTurtle
{
    public enum State
    {
        none,
        used,
        open
    }

    public class ProximityObject : MonoBehaviour
    {
        public State state;
        [HideInInspector]
        public Vector3 pos;

        void Start()
        {
            // cache the pos for further processing
            pos = gameObject.transform.position;

            // make the proximity system aware of this obj
            if(state == State.none)
                ProximityCtrl.Instance.Register(this);
        }
    }
}