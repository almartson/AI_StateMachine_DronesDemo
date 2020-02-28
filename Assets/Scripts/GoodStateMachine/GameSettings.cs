using UnityEngine;

public class GameSettings : MonoBehaviour
{

    [SerializeField] private float _droneSpeed = 2f;
    /// <summary>
    /// Setter
    /// </summary>
    public static float DroneSpeed => Instance._droneSpeed;


    [SerializeField] private float _aggroRadius = 4f;
    /// <summary>
    /// Setter
    /// </summary>
    public static float AggroRadius => Instance._aggroRadius;


    [SerializeField] private float _attackRange = 3f;
    /// <summary>
    /// Setter
    /// </summary>
    public static float AttackRange => Instance._attackRange;


    [SerializeField] private GameObject _myDroneProjectilePrefab;

    /// <summary>
    /// Static Reference to this very Static Class.
    /// It makes it possible to: Get the INSTANCE with some SYNTATIC SUGAR ( i.e.: WITHOUT using writting 'GameSettings.Instance.Attribute....blahBlah'. 
    /// .....The Syntax will be: 'GameSttings.AttributeName' ).
    /// </summary>
    public static GameObject DroneProjectilePrefab => Instance._myDroneProjectilePrefab;


    public static GameSettings Instance { get; private set; }


    // Awake is called before Start
    void Awake()
    {

        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;

    }//End Method

    //// Start is called before the first frame update
    //void Start()
    //{
    //}//End Method


}
