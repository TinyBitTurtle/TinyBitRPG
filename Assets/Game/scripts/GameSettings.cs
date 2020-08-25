using UnityEngine;
using System;

namespace TinyBitTurtle
{
    [CreateAssetMenu]
    public partial class GameSettings
    {
        [Serializable]
        public class EnemySet
        {
            public int level = 0;
            public IntRange enemiesPerRoom = new IntRange(0, 0);
            public SpawnedEnemy[] Enemies;
        }

        [Serializable]
        public class SpawnedEnemy
        {
            public int probability = 100;
            public GameObject enemy;
        }

        [Serializable]
        public class ObjectSet
        {
            public int level = 0;
            public int torchesPerRoom = 0;
            public IntRange lightsPerRoom = new IntRange(0, 0);
            public int potionPerRoom = 0;
            public int chestPerRoom = 0;
            [Header("Torches")]
            public float padding = 0;
            public float spacing = 0;
        }

        [Serializable]
        public class TileSet
        {
            public GameObject[] floorTiles;
            public GameObject[] wallTiles;                            // An array of wall tile prefabs.
            public GameObject[] outerWallTiles;                       // An array of outer wall tile prefabs. 
        }

        [Header("Enemy Sets")]
        public EnemySet[] enemiesSets;
        [Header("Object Sets")]
        public ObjectSet[] objectSet;
        [Header("EXP")]
        [Space(10)]
        public AnimationCurve EXPCurve;
        public int EXPBase = 0;
        public int EXPGrowth = 1;
        public float EXPMapping = 1;
        public int levelMax = 1;

        public int GetNextLevel(float EXP)
        {
            return (int)Mathf.Clamp(EXPCurve.Evaluate((EXP / (EXPBase + EXPGrowth))), 0, levelMax) * levelMax;
        }

        public int GetEXP(float evaluation)
        {
            return (int)(evaluation * EXPMapping);
        }
    }
}