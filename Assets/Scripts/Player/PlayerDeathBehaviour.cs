using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.FindWithTag("UICanvas").GetComponent<GameOverScreen>().EnableGameOverScreen(stateInfo.length);
    }
}
