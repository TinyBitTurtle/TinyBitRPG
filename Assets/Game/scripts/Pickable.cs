using System.Collections;
using UnityEngine;

namespace TinyBitTurtle
{
    public class Pickable : MonoBehaviour
    {
        public ParticleSystem pickupFX;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            // add sound FX
            AudioCtrl.Instance.playSoundEvent.Invoke("pickup");

            // add particle FX
            pickupFX.Play();

            StartCoroutine(Pickup(this));
        }

        protected void SetState()
        {
            gameObject.GetComponent<ProximityObject>().state = State.used;
        }

        private static IEnumerator Pickup(Pickable pickable)
        {
            // wait
            yield return new WaitForSecondsRealtime(1.0f);

            pickable.SetState();
            pickable.gameObject.SetActive(false);
        }
    }
}