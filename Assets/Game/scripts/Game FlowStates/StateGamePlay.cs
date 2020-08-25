using UnityEngine;

namespace TinyBitTurtle
{
    public sealed class StateGamePlay : GameFlowCtrl.GameFlowState
    {
        public override  void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            AudioCtrl.Instance.playSoundEvent.Invoke("playMusic");

            GameCtrl.Instance.Init();

            DungeonModel.Instance.GenerateDungeon();

            //GameCtrl.Instance.Setup();

            AudioCtrl.Instance.enabled = true;
        }
    }
}
