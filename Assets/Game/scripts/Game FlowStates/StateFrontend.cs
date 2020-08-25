using UnityEngine;

namespace TinyBitTurtle
{
    public sealed class StateFrontend : GameFlowCtrl.GameFlowState
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            AudioCtrl.Instance.playSoundEvent.Invoke("frontEndMusic");

            AudioCtrl.Instance.enabled = true;
        }
    }
}