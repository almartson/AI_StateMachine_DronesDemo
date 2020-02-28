using System;
using UnityEngine;

public class ChaseState : BaseState
{

    private Drone _myDrone;



    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="drone"></param>
    public ChaseState(Drone drone) : base(drone.gameObject)
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
        // 1- We LOOK AT IT!
        // 2- We snap-out-of-it (i.e.: Move Forward towards the TARGET, at the DRONE SPEED)
        //
        this._transform.LookAt(this._myDrone.Target);
        this._transform.Translate(Vector3.forward * Time.deltaTime * GameSettings.DroneSpeed);

        // Check:  To see if we are on in 'Attack Range'.
        //
        var distance = Vector3.Distance(this._transform.position, this._myDrone.Target.transform.position);
        //
        if (distance <= GameSettings.AttackRange)
        {

            // We are in ATTACK RANGE: Return the: 'AttackState'
            //
            return typeof(AttackState);

        }//End if (distance <= GameSettings.AttackRange)

        return null;

    }//End Method

    
}
