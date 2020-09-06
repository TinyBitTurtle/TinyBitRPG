using UnityEngine;

namespace TinyBitTurtle
{
    public abstract class ActorCtrl : MonoBehaviour
    {
        protected Actor character;
        public Spawner puffFXSpawner;

        public virtual void Awake()
        {
            GameObject GO = GameObject.FindGameObjectWithTag("Player");
            if(GO)
                character = GO.GetComponent<Actor>();
        }

        void Start()
        {
            GameObject GO = GameObject.FindGameObjectWithTag("Player");
            if (GO)
                character = GO.GetComponent<Actor>();

            // don't collide with enemies
            Physics2D.IgnoreLayerCollision(8, 15);
        }

        public virtual void Init()
        {
            // character = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();
            GameObject GO = GameObject.FindGameObjectWithTag("Player");
            if (GO)
                character = GO.GetComponent<Actor>();
        }

        public virtual void Setup()
        {
            //animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        }

        public virtual void onMove(Actor character)
        {
        }

        public virtual void EmitPuff()
        {
        }
    }
}