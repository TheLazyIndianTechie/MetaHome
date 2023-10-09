using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmoteBehaviour : StateMachineBehaviour
{
    private TextMeshProUGUI _activityLog;
    private string _playerName;

    [SerializeField]
    private string currentEmote;
    

    private void Awake()
    {
        //_activityLog = GameObject.Find("t_ActivityLog").GetComponent<TextMeshProUGUI>();
        //_playerName = GameObject.FindGameObjectWithTag("Player").name;

        

    }


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        string activityMessage = "<color=#90EE90>" + _playerName + "</color> " +currentEmote + " someone";
        Debug.Log(activityMessage);

        //Let's update the activity log by appending the message.
        //_activityLog.text += "<br>" + activityMessage;


        //string s = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        //Debug.Log("Whoooo it is working? " + s);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
