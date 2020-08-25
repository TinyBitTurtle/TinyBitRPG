using UnityEngine;

namespace TinyBitTurtle
{
    public abstract class CharacterCtrl : MonoBehaviour
    {
        protected Character character;
        public Spawner puffFXSpawner;

        public virtual void Awake()
        {
            GameObject GO = GameObject.FindGameObjectWithTag("Player");
            if(GO)
                character = GO.GetComponent<Character>();
        }

        void Start()
        {
            // don't collide with enemies
            Physics2D.IgnoreLayerCollision(8, 15);
        }

        public virtual void Init()
        {
           // character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        }

        public virtual void Setup()
        {
            //animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        }

        public virtual void onMove(Character character)
        {
        }

        public virtual void EmitPuff()
        {
        }
    }
}