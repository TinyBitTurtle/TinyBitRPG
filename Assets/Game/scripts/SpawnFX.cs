using System.Collections;
using UnityEngine;
using UnityEngine.Events;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace TinyBitTurtle
{
    public class SpawnFX : StateMachineBehaviour
    {
        public GameObject templateGO1;
        public GameObject templateGO2;

        private GameObject FX1;
        private GameObject FX2;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // create sprites
            FX1 = Instantiate(templateGO1, GameCtrl.Instance.player.gameObject.transform.position, Quaternion.identity);
            FX2 = Instantiate(templateGO2, GameCtrl.Instance.player.gameObject.transform.position, Quaternion.identity);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // stop/destroy sprites
            //GO1.SetActive(false);
            //GO2.SetActive(false);
        }
    }
}