using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerMovement : MonoBehaviour
{

    public float initialMaxSpeed = 100f;
    public float maxSpeed;
    public float acceleration = 10f;
    public float speedIncreaseRate = 0.01f;

    private const float GRAVITY = 40f;

    public float maxTilt;
    private float tilt;
    private Vector3 gravityDirection;
    private Vector3 cameraDirection; // same as gravity direction to opposite side
    private bool gameOver;
    private float simulatedAxis;

    //public Quaternion defaultLightRotation = new(0.4f, -0.23f, 0.11f, 0.88f);

    private Rigidbody rb;
    private AudioSource audioSource;
    private CameraScript cam;
    //[SerializeField] private GameObject worldLight;
    private UIController ui;

    [SerializeField] private AudioClip fallingSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        cam = transform.parent.GetComponentInChildren<CameraScript>();
        ui = transform.parent.GetComponentInChildren<UIController>();

        maxSpeed = initialMaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y < -1 && !gameOver)
        {
            StartCoroutine(GameOver());
        }

        // ANDROID TAP CONTROL
#if UNITY_ANDROID

        TapControls();

#endif

        /*
        if (transform.position.y < -500){
            Time.timeScale = 0;
        }
        */

        maxSpeed += speedIncreaseRate * Time.deltaTime;

        GravityControl();
        //LightControl();
    }


    void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(Vector3.forward * acceleration * Time.fixedDeltaTime);
        }
    }

    void GravityControl()
    {
        // Tilt from keys or sim. axis from phone taps
#if UNITY_ANDROID
        tilt = maxTilt * simulatedAxis;
#else
        tilt = maxTilt * Input.GetAxis("Horizontal");
#endif
        gravityDirection = Quaternion.Euler(0, 0, tilt) * Vector3.down;         // rotates direction by "tilt" degrees
        //cameraDirection = Quaternion.Euler(cam.camTilt, 0, tilt * 0.5f) * Vector3.down;         // tilts camera to simulate it being static while scenario moves
        cameraDirection = Quaternion.Euler(cam.camTilt, 0, 0) * Vector3.down;         // tilts camera to simulate it being static while scenario moves

        cam.transform.up = -cameraDirection; // tilt camera 
        cam.transform.Rotate(new Vector3(0, 0, tilt * 0.5f));

        Physics.gravity = gravityDirection * GRAVITY;
    }

    /*
        void LightControl(){
            worldLight.transform.rotation = defaultLightRotation;
            worldLight.transform.Rotate(0, 0, tilt * 0.5f);
        }
    */

    private IEnumerator GameOver()
    {
        gameOver = true;
        audioSource.PlayOneShot(fallingSound);
        yield return new WaitForSeconds(1);

        Time.timeScale = 0;
        ui.GameOver();
    }

    private void TapControls()
    {
        // IF SCREEN IS BEING TOUCHED
        if (Input.GetMouseButton(0))
        {
            // LEFT
            if (Input.mousePosition.x < Screen.width / 3)
            {
                // INCREASE SIMULATED AXIS, WITHOUT EXCEEDING 1
                simulatedAxis = Mathf.Max(-1, simulatedAxis - 5 * Time.deltaTime);
                return;
            }
            // RIGHT
            else if (Input.mousePosition.x > 2 * Screen.width / 3)
            {
                simulatedAxis = Mathf.Min(1, simulatedAxis + 5 * Time.deltaTime);
                return;
            }
        }
        // CENTER or NONE
        if (simulatedAxis > 0)
        {
            simulatedAxis = Mathf.Max(0, simulatedAxis - 5 * Time.deltaTime);
        }
        else
        {
            simulatedAxis = Mathf.Min(0, simulatedAxis + 5 * Time.deltaTime);
        }
    }


}
