using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using UnityEngine;

namespace TinyBitTurtle
{
    // handle retrieving the final stat value
    [CreateAssetMenu(menuName = "Character/new Stats")]
    public class CharacterStatSettings : ScriptableObject
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

        protected readonly List<CharacterStatModifer> statModifiers;
        public readonly ReadOnlyCollection<CharacterStatModifer> StatModifiers;

        public enum StatModType
        {
            Flat = 100,
            PercentAdd = 200,
            PercentMult = 300,
        }

        public class CharacterStatModifer
        {
            public readonly float Value;
            public readonly StatModType Type;
            public readonly int Order;
            public readonly object Source;

            public CharacterStatModifer(float value, StatModType type, int order, object source)
            {
                Value = value;
                Type = type;
                Order = order;
                Source = source;
            }

            public CharacterStatModifer(float value, StatModType type) : this(value, type, (int)type, null) { }
            public CharacterStatModifer(float value, StatModType type, int order) : this(value, type, order, null) { }
            public CharacterStatModifer(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
        }

        public CharacterStatSettings()
        {
            statModifiers = new List<CharacterStatModifer>();
            //StatModifiers = CharacterStat.AsReadOnly();
        }

        public void Init()
        {
            // cache the radom stat #
            baseValue = (float)baseRange.Random;
        }

        public void AddModifier(CharacterStatModifer mod)
        {
            isDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
        }

        public bool RemoveModifier(CharacterStatModifer mod)
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

        protected int CompareModifierOrder(CharacterStatModifer a, CharacterStatModifer b)
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
            CharacterStatModifer mod = null;

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
