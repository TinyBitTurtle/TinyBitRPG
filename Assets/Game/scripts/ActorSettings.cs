using UnityEngine;
using System.Collections.Generic;
using System;

 namespace TinyBitTurtle
{
    // gather all the stats
    [CreateAssetMenu(menuName = "Actor/new Actor")]
    public class ActorSettings : ScriptableObject
    {
        public float aggroRange = 0;
        [Header("Animations")]
        public AnimatorOverrideController AnimatorOverrideController;
        public List<NodeUI> listStats;

        [Serializable]
        public struct NodeUI
        {
            public string key;
            public ActorStatSettings value;
        }
        
        public void Init(Dictionary<string, ActorStatSettings> stats)
        {
            // go from List to dictionary for quick retrieval
            foreach (NodeUI node in listStats)
            {
                stats.Add(node.key, node.value);
            }

            // add the internal "level" and 'xp" stats
            ActorStatSettings characterStatSettings = new ActorStatSettings();
            characterStatSettings.baseRange = new IntRange(1, 1);
            stats.Add("LEVEL", characterStatSettings);

            characterStatSettings = new ActorStatSettings();
            characterStatSettings.baseRange = new IntRange(0, 0);
            stats.Add("XP", characterStatSettings);

            // initialize each stat
            foreach (KeyValuePair<string, ActorStatSettings> keyValuePair in stats)
            {
                keyValuePair.Value.Init();
            }
        }
    }
}