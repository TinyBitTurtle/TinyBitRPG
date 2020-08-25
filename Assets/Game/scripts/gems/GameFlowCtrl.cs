using UnityEngine;

public partial class GameFlowCtrl : FSM
{
    // register the transition delegate
    void OnEnable()
    {
        TinyBitTurtle.TransitionCtrl.OnMidFade += UISwitchPanel;
    }

    void OnDisable()
    {
        TinyBitTurtle.TransitionCtrl.OnMidFade -= UISwitchPanel;
    }

    public void UISwitchPanel(Object triggerName)
    {
        animator.ResetTrigger(triggerName.name);
        animator.SetTrigger(triggerName.name);
    }
}