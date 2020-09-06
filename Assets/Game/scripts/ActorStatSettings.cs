using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using UnityEngine;

namespace TinyBitTurtle
{
    // handle retrieving the final stat value
    [CreateAssetMenu(menuName = "Actor/new Stats")]
    public class ActorStatSettings : ScriptableObject
    {
        public IntRange baseRange;
        private float baseValue;

        protected float lastBaseValue;
        protected bool isDirty = true;

        protected float finalValue;
        public virtual float getValue()
        {
            if (isDirty || lastBaseValue != baseValue)
            {
                lastBaseValue = baseValue;
                finalValue = CalculateFinalValue();
                isDirty = false;
            }
            return finalValue;
        }

        protected readonly List<ActorStatModifer> statModifiers;
        public readonly ReadOnlyCollection<ActorStatModifer> StatModifiers;

        public enum StatModType
        {
            Flat = 100,
            PercentAdd = 200,
            PercentMult = 300,
        }

        public class ActorStatModifer
        {
            public readonly float Value;
            public readonly StatModType Type;
            public readonly int Order;
            public readonly object Source;

            public ActorStatModifer(float value, StatModType type, int order, object source)
            {
                Value = value;
                Type = type;
                Order = order;
                Source = source;
            }

            public ActorStatModifer(float value, StatModType type) : this(value, type, (int)type, null) { }
            public ActorStatModifer(float value, StatModType type, int order) : this(value, type, order, null) { }
            public ActorStatModifer(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
        }

        public ActorStatSettings()
        {
            statModifiers = new List<ActorStatModifer>();
            //StatModifiers = CharacterStat.AsReadOnly();
        }

        public void Init()
        {
            // cache the radom stat #
            baseValue = (float)baseRange.Random;
        }

        public void AddModifier(ActorStatModifer mod)
        {
            isDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
        }

        public bool RemoveModifier(ActorStatModifer mod)
        {
            if (statModifiers.Remove(mod))
            {
                isDirty = true;
                return true;
            }
            return false;
        }

        public bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            for (int i = statModifiers.Count - 1; i >= 0; i--)
            {
                if (statModifiers[i].Source == source)
                {
                    isDirty = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                }
            }
            return didRemove;
        }

        protected int CompareModifierOrder(ActorStatModifer a, ActorStatModifer b)
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            return 0;
        }

        public float CalculateFinalValue()
        {
            float finalValue = baseValue;
            float sumPercentAdd = 0;
            ActorStatModifer mod = null;

            for (int i = 0; i < statModifiers.Count; i++)
            {
                mod = statModifiers[i];

                switch (mod.Type)
                {
                    case StatModType.Flat:
                        finalValue += mod.Value;
                        break;

                    case StatModType.PercentAdd:
                        sumPercentAdd += mod.Value;

                        if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                        {
                            finalValue *= 1 + sumPercentAdd;
                            sumPercentAdd = 0;
                        }
                        break;

                    case StatModType.PercentMult:
                        {
                            finalValue *= 1 + mod.Value;
                        }
                        break;
                }
            }
            return (float)Math.Round(finalValue, 4);
        }
    }
}
