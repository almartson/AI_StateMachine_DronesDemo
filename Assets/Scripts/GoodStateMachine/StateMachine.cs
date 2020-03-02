using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    private Dictionary<Type, BaseState> _availableStates;

    public BaseState CurrentState { get; private set; }
    public event Action<BaseState> OnStateChanged;

#if UNITY_EDITOR
    /// <summary>
    ///  This is just to show and DEBUG:
    /// </summary>
    [Tooltip("This is just to show and DEBUG.")]
    public/* DroneState*/ string _myDroneStateForDebug;
#endif



    private void Update()
    {

        // Check to see if our 'Current State' is: NULL
        //
        if (CurrentState == null)
        {
            // Set the initial one, by Default.
            //
            this.CurrentState = this._availableStates.Values.First();


//#if UNITY_EDITOR
//            // Get the TYPE for Debuging
//            //
//            this._myDroneStateForDebug = this.CurrentState.GetType().AssemblyQualifiedName;
//#endif

        }//End if (CurrentState == null)


        // Get the TYPE of the NEXT STATE.
        // ..NOTE: If the STATE is not changing this time (i.e.: The State NOW = NEXT STATE); then: .Tick()  will return NULL (see bellow)
        //
        var nextState = this.CurrentState?.Tick();

        if ( (nextState != null) && (nextState != this.CurrentState?.GetType()) )
        {

            // SWITCH the STATE:
            //
            this.SwitchToNewState( nextState );

        }//End if ( (nextState != null)...

    }//End Method


    #region Other Methods

    /// <summary>
    /// Sets the (A.I.) STATE we want for our Character.
    /// </summary>
    /// <param name="states"></param>
    public void SetStates(Dictionary<Type, BaseState> states)
    {
        this._availableStates = states;

    }//End Method



    /// <summary>
    /// Transition from on STATE to ANOTHER.
    /// You can define here what BEHAVIOUR you want during every Transition... or Even define EACH TRANSITION-BEHAVIOUR specifically... (Recommendation: Do it via INYECTION)
    /// Inyect during: OnStateEnter()
    /// Inyect during: OnStateExit().......
    /// </summary>
    /// <param name="nextState"></param>
    private void SwitchToNewState( Type nextState )
    {
        // Find the State based on the Type:
        //
        this.CurrentState = this._availableStates[ nextState ];


#if UNITY_EDITOR
        // Get the TYPE for Debuging
        //
        this._myDroneStateForDebug = this.CurrentState.GetType().AssemblyQualifiedName;
#endif


        //
        // Invoke 'OnStateChanged' Method, passing 'CurrentState' State:
        //
        OnStateChanged?.Invoke(this.CurrentState);

    }//End Method


    #endregion Other States

}
