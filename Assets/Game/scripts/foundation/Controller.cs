using System.Collections;
using UnityEngine;

namespace TinyBitTurtle
{
    public class Controller<T> : SingletonMonoBehaviour<T> where T : MonoBehaviour
    {
        protected float waitTime { get; set; }

        private void Start()
        {
            if (waitTime != 0)
            {
                // convert from Hertz to ms
                waitTime = 1 / waitTime;

                StartCoroutine("Sample");
            }
        }

        protected virtual void TimedUpate() {}

        IEnumerator Sample()
        {
            while (true)
            {
                TimedUpate();

                yield return new WaitForSeconds(waitTime);
            }
        }
    }
}
