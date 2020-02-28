using System;
using UnityEngine;

/// <summary>
/// This class will be the PARENT (BASE CLASS) to all CHILDREN (DERIVED), which will really IMPLEMENT its Methods (via the OVERRIDE command), such as:   TICK()  // State Change.
/// </summary>
public abstract class BaseState
{

    protected GameObject _myGameObject;
    protected Transform _transform;



    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="gameObject"></param>
    public BaseState(GameObject gameObject)
    {
        this._myGameObject = gameObject;
        this._transform = this._myGameObject.transform;

    }//End Constructor



    /// <summary>
    /// Abstract Method to be IMPLEMENTED inside each (derived) Child Class
    /// </summary>
    /// <returns></returns>
    public abstract Type Tick();


    ///// <summary>
    ///// Not necessary to be implemented in CHild (Derived) Classes
    ///// </summary>
    //public virtual void OnStateEnter() { }
    //public virtual void OnStateExit() { }

}
