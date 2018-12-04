using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

    //[SerializeField] private Animator m_animator;
    //public float cameraDistance = 10.0f;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public bool ShowCursor;
    public float Sensitivity;
    Camera mycam;
 
    void Start()
    {
        mycam = GetComponent<Camera>();
        if(ShowCursor == false)
        {
            Cursor.visible = false;
        }
        /*
        if (isLocalPlayer)
        {
            Quaternion q = Quaternion.Euler(12.0f, 0, 0);
            Camera.main.transform.position = new Vector3(0.0f, 3.0f, -4f);//this.transform.position*10 - this.transform.forward * 20 + this.transform.up *10;
            Camera.main.transform.rotation = q;//LookAt(this.transform.position*20);
            Camera.main.transform.parent = this.transform;
            
            /*
            Vector3 temp = this.transform.position;
            Vector3 y_axis = new Vector3(0, 1, 0);
            Camera.main.transform.position = this.transform.position + y_axis;
            //Camera.main.transform.position = this.transform.position - this.transform.forward * 2 + (temp) * 1;
            Camera.main.transform.LookAt(this.transform.position);
            Camera.main.transform.parent = this.transform;
            
        }*/
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        } 
        float newRotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * Sensitivity;
        float newRotationX = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * Sensitivity;

        gameObject.transform.localEulerAngles = new Vector3(newRotationX, newRotationY, 0);
 
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E))
        {
            CmdFire();
        }

    }

    /* Command code is called on the Client but is run on the server */
    [Command]
    void CmdFire()
    {
        // Create the bullet from the prefab
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 30.0f;

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }
}
