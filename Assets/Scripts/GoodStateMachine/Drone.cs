using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    ///// <summary>
    ///// Tool to Debug this State Machine
    ///// </summary>
    //[Tooltip("Tool to Debug this State Machine")]
    //public bool _thisIsDebugTime = false;


    /// <summary>
    /// It is the TYPE of CHARACTER the Drone is: Blue, or RED (Good or Bad?)
    /// </summary>
    [SerializeField] private Team _team;

    /// <summary>
    /// Laser beam to kill an enemy (it appears visually on screen).
    /// </summary>
    [SerializeField] private GameObject _laserVisual;

    /// <summary>
    /// Getter and Setter
    /// </summary>
    public Transform Target { get; private set; }

    /// <summary>
    /// Getter
    /// </summary>
    public Team Team => _team;

    /// <summary>
    /// State Machine Getter. 
    /// NOTE: It is NOT OPTIMIZED, because we should CACHE IT (it is permforming a 'GetComponent<>()' each time)
    /// </summary>
    public StateMachine StateMachine => GetComponent<StateMachine>();


    #region Other Attributes

    /// <summary>
    /// Getter
    /// </summary>
    [HideInInspector]
    public Color _myTeamDebugRayColor;  // It is initialized later // = Color.red;

    #endregion Other Attributes


    private void Awake()
    {
        this.InitializeStateMachine();


#if UNITY_EDITOR
        // Initialize Drone's Debug Ray Color
        //
        if (this.Team == Team.Red)
        {
            this._myTeamDebugRayColor = Color.red;

        }
        else    // Blue Tam
        {
            this._myTeamDebugRayColor = Color.blue;

        }//End else
#endif

    }//End Method



    #region My Methods

    /// <summary>
    /// Initializes the CHaracter's (DRONE in this case) GAMEOBJECTS' State Machine.
    /// </summary>
    private void InitializeStateMachine()
    {

        var states = new Dictionary<Type, BaseState>()
        {
            { typeof(WanderState), new WanderState( this ) },
            { typeof(ChaseState), new ChaseState( this ) },
            { typeof(AttackState), new AttackState( this ) }
        };

        GetComponent<StateMachine>().SetStates(states);

    }//End Method


    public void SetTarget( Transform target )
    {
        this.Target = target;

    }//End Method


    public void FireWeapon()
    {
        // Turn on: the LASER VISUAL:
        //
        this._laserVisual.transform.position = (this.Target.position + transform.position) / 2f;
        //
        // Aim: to the TARGET. Calculate the LONGITUDE of the Laser BEAM.
        //
        float distance = Vector3.Distance( this.Target.position, transform.position );
        //
        // It Kills the enemy!
        //
        this._laserVisual.transform.localScale = new Vector3( 0.1f, 0.1f, distance );
        this._laserVisual.SetActive(true);
        //
        // Turn it off.
        //
        StartCoroutine( this.TurnOffLaser() );

    }//End Method


    public IEnumerator TurnOffLaser()
    {
        yield return new WaitForSeconds( 0.25f );

        this._laserVisual.SetActive(false);

        if ( this.Target != null)
        {
            Destroy(Target.gameObject);

        }//End if ( this.Target != null)

    }//End Method


#endregion My Methods
}



public enum Team
{

    Red,
    Blue

}//End Enum
