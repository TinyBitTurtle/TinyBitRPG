using System.Collections;
using UnityEngine;

namespace TinyBitTurtle
{
    public class AICtrl : CharacterCtrl
    {
        public Transform playerTransform;

        public enum state
        {
            Idle,
            Patrol,
            Attack
        }

        private state currentState;
        private Vector2Int startPatrol;
        private Vector2Int endPatrol;

        // Use this for initialization
        void Start()
        {
            currentState = state.Idle;

            //if (playerTransform == null)
            {
                //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }

            StartCoroutine(AIFSM());
        }

        IEnumerator AIFSM()
        {
            while (true)
            {
                yield return StartCoroutine(currentState.ToString());
            }
        }

        IEnumerator Idle()
        {
            Debug.Log("enter Idle");

            while (currentState == state.Idle)
            {
                if (PlayerInRange())
                    currentState = state.Attack;

                yield return null;
            }

            Debug.Log("leaving Idle");
        }

        IEnumerator Patrol()
        {
            Debug.Log("enter Patrol");

            while (currentState == state.Patrol)
            {
                if (PlayerInRange())
                    currentState = state.Attack;

                yield return null;
            }

            Debug.Log("leaving Patrol");
        }

        IEnumerator Attack()
        {
            Debug.Log("enter Attack");

            while (currentState == state.Attack)
            {
                if (!PlayerInRange())
                    currentState = state.Idle;

                yield return null;
            }

            Debug.Log("leaving Attack");
        }

        private bool PlayerInRange()
        {
            //return ((playerTransform.position - transform.position).sqrMagnitude) < (AggroRadius * AggroRadius);
            return false;
        }

        private void GeneratePatrol(Room room)
        {
            //startPatrol.x = transform.position.x;
            startPatrol.y = Random.Range(0, room.dim.y);

            endPatrol.x = Random.Range(0, room.dim.x);
            endPatrol.y = Random.Range(0, room.dim.y);
        }
    }
}