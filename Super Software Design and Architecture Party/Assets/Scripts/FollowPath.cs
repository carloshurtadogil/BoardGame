using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class FollowPath : NetworkBehaviour
{
    #region Enums
    public enum MovementType  //Type of Movement
    {
        MoveTowards,
        LerpTowards
    }
    #endregion //Enums

    #region Public Variables
    public MovementType Type = MovementType.MoveTowards; // Movement type used
    public GameObject pathObject;
    public MovementPath MyPath; // Reference to Movement Path Used
    public float Speed = 1; // Speed object is moving
    public float MaxDistanceToGoal = .1f; // How close does it have to be to the point to be considered at point
    public GameObject[] costumes;
    public Material[] skins;
    public GameObject backupCam;
    public GameObject[] spawnPoints;
    #endregion //Public Variables

    #region Private Variables
    [SyncVar]
    private int newAmount;
    private IEnumerator<Transform> pointInPath; //Used to reference points returned from MyPath.GetNextPathPoint
    private Animator animator;
    private int spaces = 0;
    private int current;
    private int movingTo;
    private bool canMove;
    private bool isTurn = true;
    private Vector3 camPos;
    private bool one;
    private CardGenerator cg;
    private GameMaster gm;
    #endregion //Private Variables

    // (Unity Named Methods)
    #region Main Methods
    void Start()
    {
        newAmount = 0;
        if (isLocalPlayer)
        {
            cg = GameObject.FindWithTag("Card Generator").GetComponent<CardGenerator>();
            //gameObject.transform.position = MyPath.PathSequence[0].transform.position;
            animator = GetComponent<Animator>();
            Debug.Log("Player Position: " + gameObject.transform.position);

            //Make sure there is a path assigned
            if (MyPath == null)
            {
                pathObject = GameObject.FindGameObjectWithTag("MyPath");
                MyPath = pathObject.GetComponent<MovementPath>();
                //Debug.LogError("Movement Path cannot be null, I must have a path to follow.", gameObject);
                //return;
            }

            //Sets up a reference to an instance of the coroutine GetNextPathPoint
            pointInPath = MyPath.GetNextPathPoint();
            //Get the next point in the path to move to (Gets the Default 1st value)
            pointInPath.MoveNext();

            //Make sure there is a point to move to
            if (pointInPath.Current == null)
            {
                Debug.LogError("A path must have points in it to follow", gameObject);
                return; //Exit Start() if there is no point to move to
            }

            //Set the position of this object to the position of our starting point
            transform.position = pointInPath.Current.position;
            //Draw();
            movingTo = MyPath.movingTo;
            current = movingTo - 1;
            camPos = new Vector3(0.0f, 1.0f, -1.5f);
        }
    }

     

    //Update is called by Unity every frame
    public void Update()
    {
        if (isLocalPlayer) {
            //Camera.main.transform.position = camPos;
            //Debug.Log("Camera Pos: " +  Camera.main.transform.position);
            if (spaces > 0 && isTurn && canMove)
            {
                //gameObject.transform.rotation = Quaternion.Euler(0.0f, gameObject.transform.rotation.y, 0.0f);
                //Validate there is a path with a point in it
                if (pointInPath == null || pointInPath.Current == null)
                {
                    return; //Exit if no path is found
                }

                if (Type == MovementType.MoveTowards) //If you are using MoveTowards movement type
                {
                    //Move to the next point in path using MoveTowards
                    transform.position =
                        Vector3.MoveTowards(transform.position,
                                            pointInPath.Current.position,
                                            Time.deltaTime * Speed);
                    animator.Play("run", 1);
                    if (MyPath.movingTo == 2 || MyPath.movingTo == 40) //|| MyPath.movingTo == 6 || MyPath.movingTo == 16)
                    {
                        //TurnTo(MyPath.movingTo);
                        RotateTo(-90.0f);
                        //gameObject.transform.rotation = Quaternion.Euler(0.0f, gameObject.transform.rotation.y, 0.0f);
                    }
                    if (MyPath.movingTo == 6)
                    {
                        RotateTo(-156.0f);
                    }
                    if (MyPath.movingTo == 16)
                    {
                        RotateTo(118.0f);
                    }
                    if (MyPath.movingTo == 24)
                    {
                        RotateTo(52.0f);
                    }
                    if (MyPath.movingTo == 32)
                    {
                        RotateTo(-18.0f);
                    }
                }
                else if (Type == MovementType.LerpTowards) //If you are using LerpTowards movement type
                {
                    //Move towards the next point in path using Lerp
                    transform.position = Vector3.Lerp(transform.position,
                                                        pointInPath.Current.position,
                                                        Time.deltaTime * Speed);
                }

                //Check to see if you are close enough to the next point to start moving to the following one
                //Using Pythagorean Theorem
                //per unity suaring a number is faster than the square root of a number
                //Using .sqrMagnitude 
                var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
                if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal) //If you are close enough
                {
                    pointInPath.MoveNext(); //Get next point in MovementPath
                }
                //The version below uses Vector3.Distance same as Vector3.Magnitude which includes (square root)
                /*
                var distanceSquared = Vector3.Distance(transform.position, pointInPath.Current.position);
                if (distanceSquared < MaxDistanceToGoal) //If you are close enough
                {
                    pointInPath.MoveNext(); //Get next point in MovementPath
                }
                */
                movingTo = MyPath.movingTo;
                //Debug.Log("MovingTo: " + movingTo);
                if (one)
                {
                    spaces++;
                    one = false;
                }
                if (current != (movingTo - 1))
                {
                    current = movingTo - 1;
                    Debug.Log("Space: " + (current));
                    spaces--;
                }

            }
            else
            {
                canMove = false;
            }
        }

    }
    #endregion //Main Methods

    //(Custom Named Methods)
    #region Utility Methods 

    public void TurnTo(int space) 
    {
        Vector3 dir = MyPath.PathSequence[2].transform.position - gameObject.transform.position;
        float step = 3.0f * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, dir, step, 0.0f);
        gameObject.transform.rotation = Quaternion.LookRotation(newDir);
    }

    public void RotateTo(float y)
    {
        gameObject.transform.rotation = Quaternion.Euler(0.0f, y, 0.0f);
    }

    public void Draw()
    {
        StartCoroutine(cg.Draw());
        spaces = cg.getValue();
        if(spaces == 1) {
            one = true;
        }
        Debug.Log("Moving " + spaces + " Spaces");
        //canMove = true;
    }

    public int ScanField() {
        return GameObject.FindGameObjectsWithTag("Player").Length;
    }

    [Command]
    public void CmdSpawn(int c, int pos)
    {
        Debug.Log("CmdSpawn param is " + c);
        SkinnedMeshRenderer r = gameObject.transform.GetComponentInChildren<SkinnedMeshRenderer>();
        Material[] mats = { skins[c], skins[c+1] };
        r.materials = mats;
        Vector3 spawn = spawnPoints[pos].transform.position;
        gameObject.transform.position = new Vector3(spawn.x, gameObject.transform.position.y, spawn.z);
    }

    #endregion //Utility Methods

    //Coroutines run parallel to other fucntions
    #region Coroutines

    public IEnumerator Waiting() {
        yield return new WaitForSeconds(5);
        if (isLocalPlayer) {
            int count = ScanField();
            Debug.Log("Found " + count + " Players");
            /*
            Debug.Log("Count Returned "+ count);
            if (count == 1)
            {
                CmdSpawn(0, 0);
            }

            if (count == 2)
            {
                CmdSpawn(2, 1);
            }

            if (count == 3)
            {
                CmdSpawn(4, 2);
            }*/
        }

    }


    #endregion //Coroutines
}
