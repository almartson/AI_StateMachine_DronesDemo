using System;
using UnityEngine;

public class AttackState : BaseState
{

    private float _attackReadyTimer;
    private Drone _myDrone;



    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="drone"></param>
    public AttackState(Drone drone) : base(drone.gameObject)
    {
        this._myDrone = drone;

    }//End Constructor


    public override Type Tick()
    {

        // Check: to see if we have a TARGET.
        //
        if (this._myDrone.Target == null)
        {
            // We have to WANDER:
            //
            return typeof(WanderState);

        }//End if (this._myDrone.Target..


        // If we do have a TARGET:
        // 1- COUNT: Time to REACT and Shoot (simulating: Reaction Speed)
        //
        this._attackReadyTimer -= Time.deltaTime;

        if (this._attackReadyTimer <= 0f)
        {
            Debug.Log("Attack!");
            this._myDrone.FireWeapon();

        }//End if (this._attackReadyTimer <= 0f)

        return null;

    }//End Method

}
