using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyBitTurtle
{
    public class Spawner : EZObjectPool
    {
        // delegate callback
        public delegate void SpawnCallback(GameObject newGameObject);

        public string spawnSound;
        public float delay;
        [Range( 0,1)]
        public float variance;

        protected GameObject newGameObject;

        public void Init()
        {
            if (instatiateOnInit)
            {
                ObjectList = new List<GameObject>(PoolSize);
                AvailableObjects = new List<GameObject>(PoolSize);
                InstantiatePool();
            }
        }

        public virtual void spawnAction(SpawnCallback callback, Vector3 pos)
        {
            // spawn and positionned newly object
            TryGetNextObject(transform.position, transform.rotation, out newGameObject);
            newGameObject.transform.localPosition = pos;

            // do callback
            callback?.Invoke(newGameObject);

            // play spawn sound
            if (spawnSound != "")
                AudioCtrl.Instance.playSoundEvent.Invoke(spawnSound);
        }

        public void spawn(SpawnCallback callback, Vector3 pos)
        {
            // wait X seconds before spawning
            if (delay != 0)
            {
                StartCoroutine(delayedSpawn(callback, spawnAction, delay, pos));
            }
            // spawm immediately
            else
            {
                spawnAction(callback, pos);
            }
        }

        private static IEnumerator delayedSpawn(SpawnCallback callback, Action<SpawnCallback, Vector3> spawnAction, float duration, Vector3 pos)
        {
            // wait
            yield return new WaitForSecondsRealtime(duration);

            // spawn
            spawnAction(callback, pos);
        }
    }
}