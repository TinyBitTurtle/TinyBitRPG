using System.Collections.Generic;
using UnityEngine;

namespace TinyBitTurtle
{
    public class EnemySpawner : Spawner
    {
        public GameSettings gameSettings;

        private int level;

        private void getCurrentLevel()
        {
            level = 0;// GameCtrl.Instance.player.GetLevel();
        }

        public override void InstantiatePool()
        {
            ClearPool();

            AvailableObjects = new List<GameObject>(PoolSize);

            getCurrentLevel();

            for (int i = 0; i < PoolSize; i++) // iterate
            {
                // chose according to each prob probability a template
                int idx = Random.Range(1, 101);
                float AccProbability = 0.0f;
                for (int j = 0; j < gameSettings.enemiesSets[level].Enemies.Length; ++j)
                {
                    AccProbability += gameSettings.enemiesSets[level].Enemies[j].probability;

                    if (idx <= AccProbability)
                    {
                        Template = gameSettings.enemiesSets[level].Enemies[j].enemy;
                        break;
                    }
                }

                GameObject g = NewActiveObject();
                g.SetActive(false);
                ObjectList.Add(g);
                AvailableObjects.Add(g);
            }

            // turn off all templates
            for (int j = 0; j < gameSettings.enemiesSets[level].Enemies.Length; ++j)
            {
                gameSettings.enemiesSets[level].Enemies[j].enemy.SetActive(false);
            }
        }

        public override bool TryGetNextObject(Vector3 pos, Quaternion rot, out GameObject obj)
        {
            int lastIndex = AvailableObjects.Count - 1;

            if (AvailableObjects.Count > 0)
            {
                if (AvailableObjects[lastIndex] == null)
                {
                    Debug.LogError("EZObjectPool " + PoolName + " has missing objects in its pool! Are you accidentally destroying any GameObjects retrieved from the pool?");
                    obj = null;
                    return false;
                }

                AvailableObjects[lastIndex].transform.position = pos;
                AvailableObjects[lastIndex].transform.rotation = rot;
                AvailableObjects[lastIndex].SetActive(true);
                obj = AvailableObjects[lastIndex];
                AvailableObjects.RemoveAt(lastIndex);
                return true;
            }

            if (AutoResize)
            {
                GameObject g = NewActiveObject();
                g.transform.position = pos;
                g.transform.rotation = rot;
                ObjectList.Add(g);
                obj = g;
                return true;
            }
            else
            {
                obj = null;
                return false;
            }
        }

        public override void spawnAction(SpawnCallback callback, Vector3 pos)
        {
            base.spawnAction(callback, pos);

            //GameCtrl.Instance.player.control.animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        }
    }
}