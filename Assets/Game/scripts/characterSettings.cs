using UnityEngine;
using System.Collections.Generic;
using System;

 namespace TinyBitTurtle
{
    // gather all the stats
    [CreateAssetMenu(menuName = "Character/new Character")]
    public class CharacterSettings : ScriptableObject
    {
        public float aggroRange = 0;
        [Header("Animations")]
        public AnimatorOverrideController AnimatorOverrideController;
        public List<NodeUI> listStats;

        [Serializable]
        public struct NodeUI
        {
            public string key;
            public CharacterStatSettings value;
        }
        
        public void Init(Dictionary<string, CharacterStatSettings> stats)
        {
            // go from List to dictionary for quick retrieval
            foreach (NodeUI node in listStats)
            {
                stats.Add(node.key, node.value);
            }

            // add the internal "level" and 'xp" stats
            CharacterStatSettings characterStatSettings = new CharacterStatSettings();
            characterStatSettings.baseRange = new IntRange(1, 1);
            stats.Add("LEVEL", characterStatSettings);

            characterStatSettings = new CharacterStatSettings();
            characterStatSettings.baseRange = new IntRange(0, 0);
            stats.Add("XP", characterStatSettings);

            // initialize each stat
            foreach (KeyValuePair<string, CharacterStatSettings> keyValuePair in stats)
            {
                keyValuePair.Value.Init();
            }
        }
    }
}