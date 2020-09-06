using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TinyBitTurtle
{
    public enum Mode
    {
        Normal,
        Alt,

        Max
    };

    [System.Serializable]
    public class IntEvent : UnityEvent<int> { }

    public class Actor : MonoBehaviour
    {
        private enum Direction
        {
            FadeIn,
            FadeOut,

            FadeMax
        }

        public ActorSettings characterSettings;
        public ActorCtrl control;
        
        // per instance
        private class Stats : Dictionary<string, ActorStatSettings> { }
        private readonly Stats stats = new Stats();
        [HideInInspector]
        public Mode mode = Mode.Normal;
        [HideInInspector]
        public string characterSettingsNormalModeName;
        public GameObject aggroCircle;

        // events
        public IntEvent HPChange;
        public IntEvent energyChange;
        public IntEvent XPChange;
        public UnityEvent ModeChange;

        // local cache
        [HideInInspector]
        public Animator animator;

        void Start()
        {
            if (HPChange == null)
                HPChange = new IntEvent();

            HPChange.AddListener(HPChanged);

            if (ModeChange == null)
                ModeChange = new UnityEvent();

            ModeChange.AddListener(ModeChanged);

            animator = gameObject.GetComponent<Animator>();
        }

        public void onSwitchCharacter(string characterSettingsName)
        {
            if (animator == null)
            {
                return;
            }

            // sound feedback
            AudioCtrl.Instance.playSoundEvent.Invoke("re-spawn");

            // FSM change
            animator.StopPlayback();
            animator.SetBool("mode", true);

            // character stat swithing
            SetCharacterSettings(characterSettingsName);

            // callback for mode
            ModeChange.Invoke();
        }

        protected virtual void HPChanged(int amount)
        {
            
        }

        public void ModeChanged()
        {
            if(mode == Mode.Alt)
            {
                StartCoroutine(VisionBW(Direction.FadeIn));
            }
            else if(mode == Mode.Normal)
            {
                StartCoroutine(VisionBW(Direction.FadeOut));
            }
        }

        private static IEnumerator VisionBW(/*Material material,*/ Direction direction)
        {
            // set variables based on the direction
            bool fadeIn = direction == Direction.FadeIn;
            float step = fadeIn ? 1 * (1f / ImageEffects.Instance.duration) : -1 * (1f / ImageEffects.Instance.duration);
            float fadeTarget = fadeIn ? 1.0f : 0;
            float fadeValue = fadeIn ? 0 : 1.0f;

            if (fadeIn)
            {
                while (fadeValue < fadeTarget)
                {
                    fadeValue += Time.deltaTime * step;
                    ImageEffects.Instance.intensity = fadeValue;
                    yield return null;
                }
            }
            else
            {
                while (fadeValue > fadeTarget)
                {
                    fadeValue += Time.deltaTime * step;
                    ImageEffects.Instance.intensity = fadeValue;
                    yield return null;
                }

               
            }

            ImageEffects.Instance.intensity = fadeTarget;
        }

        public void Init()
        {
            // to dictionary
            characterSettings.Init(stats);

            control.Init();
        }

        public void Setup()
        {
            control.Setup();
        }

        // the character gets dmg by another
        public void GetHit(Actor attacher)
        {
            // formula for now dmg = st
            float amount = attacher.GetStat("STR");

            HPChange.Invoke((int)amount);
        }

        public void ChangeHP(int amount)
        {
            // add/remove to the HP
            ActorStatSettings.ActorStatModifer characterStatModifer = new ActorStatSettings.ActorStatModifer(amount, ActorStatSettings.StatModType.Flat, 0, this);
            stats["HP"].AddModifier(characterStatModifer);

            if (GetStat("HP") <= 0)
                Die();
        }

        public void ReachNextLevel(int level)
        {
            // add +1 to the level
            ActorStatSettings.ActorStatModifer characterStatModifer = new ActorStatSettings.ActorStatModifer(level, ActorStatSettings.StatModType.Flat, 0, this);
            stats["LEVEL"].AddModifier(characterStatModifer);

            // reset XPs to 0
            stats["XP"].RemoveAllModifiersFromSource(this);
        }

        public void GainXPs(int amount)
        {
            // add +1 to the level
            ActorStatSettings.ActorStatModifer characterStatModifer = new ActorStatSettings.ActorStatModifer(amount, ActorStatSettings.StatModType.Flat, 0, this);
            stats["XP"].AddModifier(characterStatModifer);
        }

        public int GetLevel()
        {
            int level = (int)stats["LEVEL"].getValue();

            return level;
        }

        public int GetRemainingXPs()
        {
            int XPs = (int)stats["XP"].getValue();

            return XPs;
        }

        public float GetStat(string statName)
        {
            return stats[statName].getValue();
        }

        public void onMove(Actor character)
        {
            if (control == null)
                return;

            control.onMove(character);
        }

        public void EmitPuff()
        {
            if (control == null)
                return;

            control.EmitPuff();
        }

        public void SetCharacterSettings(string characterSettingdName)
        {
            // cache the settings name
            if(mode == Mode.Normal)
                characterSettingsNormalModeName = characterSettingdName;

            characterSettings = Resources.Load<ActorSettings>("Actor/" + characterSettingdName);

            // set all the anims specific to that character
            Animator animator = GetComponent<Animator>();
            if (animator)
                animator.runtimeAnimatorController = characterSettings.AnimatorOverrideController;
        }

        public void SetControl(ActorCtrl characterCtrl)
        {
            control = characterCtrl;
        }

        // player dies...
        private void Die()
        {
        }

        private void Update()
        {
            if(mode == Mode.Alt)
            {
                StartCoroutine(VisionBW(Direction.FadeIn));

                aggroCircle.transform.localScale = new Vector3(characterSettings.aggroRange, characterSettings.aggroRange, 1);
                aggroCircle.SetActive(true);
            }
            else if (mode == Mode.Normal)
            {

                StartCoroutine(VisionBW(Direction.FadeOut));

                aggroCircle.transform.localScale = Vector3.one;
                aggroCircle.SetActive(false);
            }
        }
    }
}