using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //for debugging
using UnityEngine.UI;

public class CarRacingMovement : MonoBehaviour
{
    public float acceleration = 8;
    public float turnSpeed = 5;
    Quaternion targetRotation;
    Rigidbody _rigidbody;
    Vector3 lastPosition;

    float _sideSlipAmount = 0;
    public float sideSlipAmount { get { return _sideSlipAmount; } }
    float _speed;
    public float Speed { get { return _speed; } }

    //for agent
    private int pointOfScreen;

    Vector2 S1 = new Vector2(0, Screen.height);
    Vector2 S2 = new Vector2(Screen.width / 2, Screen.height);
    Vector2 S3 = new Vector2(Screen.width, Screen.height);
    Vector2 S4 = new Vector2(0, Screen.height / 2);
    Vector2 S5 = new Vector2(Screen.width, Screen.height / 2);
    Vector2 S6 = new Vector2(0, 0);
    Vector2 S7 = new Vector2(Screen.width / 2, 0);
    Vector2 S8 = new Vector2(Screen.width, 0);

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        SetRotationPoint();
        SetSideSlip();

        // for testing
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            Debug.Log("Restart");
        }
    }


    void SetSideSlip()
    {
        Vector3 direction = transform.position - lastPosition;
        Vector3 movement = transform.InverseTransformDirection(direction);
        lastPosition = transform.position;
        _sideSlipAmount = movement.x;

    }

    void SetRotationPoint()
    {
        Ray ray;
        //for agent
        switch (pointOfScreen)
        {
            case 1:
                Debug.Log("1");
                ray = Camera.main.ScreenPointToRay(S1);
                break;
            case 2:
                Debug.Log("2");
                ray = Camera.main.ScreenPointToRay(S2);
                break;
            case 3:
                Debug.Log("3");
                ray = Camera.main.ScreenPointToRay(S3);
                break;
            case 4:
                Debug.Log("4");
                ray = Camera.main.ScreenPointToRay(S4);
                break;
            case 5:
                Debug.Log("5");
                ray = Camera.main.ScreenPointToRay(S5);
                break;
            case 6:
                Debug.Log("6");
                ray = Camera.main.ScreenPointToRay(S6);
                break;
            case 7:
                Debug.Log("7");
                ray = Camera.main.ScreenPointToRay(S7);
                break;
            case 8:
                Debug.Log("8");
                ray = Camera.main.ScreenPointToRay(S8);
                break;
            default:
                Debug.Log("Error");
                ray = Camera.main.ScreenPointToRay(S1);
                break;

        }


        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(0, rotationAngle, 0);
        }
    }

    void FixedUpdate()
    {
        //set the key input
        if (Input.GetKeyDown(KeyCode.Alpha1)) { pointOfScreen = 1; } // 
        if (Input.GetKeyDown(KeyCode.Alpha2)) { pointOfScreen = 2; } // 
        if (Input.GetKeyDown(KeyCode.Alpha3)) { pointOfScreen = 3; } // 
        if (Input.GetKeyDown(KeyCode.Q)) { pointOfScreen = 4; } // 
        if (Input.GetKeyDown(KeyCode.E)) { pointOfScreen = 5; } // 
        if (Input.GetKeyDown(KeyCode.A)) { pointOfScreen = 6; } // 
        if (Input.GetKeyDown(KeyCode.S)) { pointOfScreen = 7; } // 
        if (Input.GetKeyDown(KeyCode.D)) { pointOfScreen = 8; } // 



        _speed = _rigidbody.velocity.magnitude / 1000;
        float accelerationInput = acceleration * (Input.GetMouseButton(0) ? 1 : Input.GetMouseButton(1) ? -1 : 0) * Time.fixedDeltaTime;
        _rigidbody.AddRelativeForce(Vector3.forward * accelerationInput);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Mathf.Clamp(_speed, -1, 1) * Time.fixedDeltaTime);
    }
}

