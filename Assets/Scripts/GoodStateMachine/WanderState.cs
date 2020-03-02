using System;
using UnityEngine;
/////using System.Linq;


/// <summary>
/// A.I. State.
/// It is a Child of the BASE: 'BaseState'
/// </summary>
public class WanderState : BaseState
{

    /// <summary>
    /// Nullable (?) Vector3. It is my Destination.
    /// To know its value you must use the .Value Getter Method.
    /// </summary>
    private Vector3? _myDestination;
    private float _stoppingDistance = 1f;
    private float _turnSpeed = 1f;
    private readonly LayerMask _layerMask = LayerMask.NameToLayer("Walls");
    private float _rayDistance = 4.5f;    //3.5f;
    private Quaternion _desiredRotation;
    private Vector3 _myDirection;
    private Drone _myDrone;

    /////Not used: public RaycastHit[] _myRaycastHits = new RaycastHit[10];

    private Quaternion _startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    private Quaternion _stepAngle = Quaternion.AngleAxis(5, Vector3.up);



    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="drone"></param>
    public WanderState(Drone drone) : base( drone.gameObject )
    {
        this._myDrone = drone;

    }//End Constructor


    /// <summary>
    /// Update the State.
    /// </summary>
    /// <returns></returns>
    public override Type Tick()
    {

        var chaseTarget = this.CheckForAggro();


        if (chaseTarget != null)
        {
            this._myDrone.SetTarget(chaseTarget);
            return typeof(ChaseState);

        }//End if (chaseTarget != null)
        else
        {

            if ((this._myDestination.HasValue == false) || (Vector3.Distance(this._transform.position, this._myDestination.Value) <= this._stoppingDistance))
            {

                // Look for a New Destination:
                //
                this.FindRandomDestination();

            }//End if ( (this._myDestination.HasValue == false)..        


            // Rotate Drone towards our TARGET:
            //
            this._transform.rotation = Quaternion.Slerp(this._transform.rotation, this._desiredRotation, Time.deltaTime * this._turnSpeed);


            // Draw our RAYS (Raycasting) again:
            //
            if (IsForwardBlocked())
            {
                // Just Rotate... (instead of moving):
                //
                this._transform.rotation = Quaternion.Lerp(this._transform.rotation, this._desiredRotation, 0.2f);


                //Debug.LogWarning("IsForwardBlocked:  It has passed ........IN THIS LINE......");

            }
            else
            {
                this._transform.Translate(Vector3.forward * Time.deltaTime * GameSettings.DroneSpeed);


                //Debug.Log("IsForwardBlocked: NOPEEEEEEE:  It has passed ........IN THIS LINE......");

            }//End if (IsForwardBlocked())


            Debug.DrawRay(this._transform.position, this._myDirection * this._rayDistance, this._myDrone._myTeamDebugRayColor);


            while (IsPathBlocked())
            {
                this.FindRandomDestination();
                ///Debug.Log("WALL!");

            }//End while (IsPathBlocked())


            return null;

        }//End else

    }//End Method


    #region Raycasts to find a way


    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(this._transform.position, this._transform.forward);
        return Physics.Raycast(ray, this._rayDistance);

    }//End Method


    private bool IsPathBlocked()
    {
        Ray ray = new Ray(this._transform.position, this._myDirection);
        return Physics.Raycast(ray, this._rayDistance);

    }//End Method


    #endregion Raycasts to find a way


    private void FindRandomDestination()
    {
        Vector3 testPosition = (this._transform.position + (this._transform.forward * 4f)) + new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0f, UnityEngine.Random.Range(-4.5f, 4.5f));

        this._myDestination = new Vector3(testPosition.x, 1f, testPosition.z);

        this._myDirection = Vector3.Normalize(this._myDestination.Value - this._transform.position);
        this._myDirection = new Vector3(this._myDirection.x, 0f, this._myDirection.z);
        this._desiredRotation = Quaternion.LookRotation(this._myDirection);

        ///Debug.Log("Got Direction");

    }//End Method  


    /// <summary>
    /// This Checks to see: if there is any Enemy in front of the Character.
    /// </summary>
    /// <returns></returns>
    private Transform CheckForAggro()
    {
        //float aggroRadius = 5f;

        RaycastHit hit;
        var angle = this._transform.rotation * this._startingAngle;
        var direction = angle * Vector3.forward;
        var pos = this._transform.position;

        for (var i = 0; i < 24; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, GameSettings.AggroRadius))
            {

                var drone = hit.collider.GetComponent<Drone>();

                if ((drone != null) && (drone.Team != this._myGameObject.GetComponent<Drone>().Team))
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return drone.transform;
                }
                else
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(pos, direction * GameSettings.AggroRadius, Color.white);
            }
            direction = this._stepAngle * direction;
        }

        return null;
    }

}
