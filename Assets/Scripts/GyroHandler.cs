using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroHandler : MonoBehaviour
{
    [SerializeField]
    Play playManager;
    public bool gyroUp, gyroDown;

    public float rotationRateY;
    public float tiltThreshold;
    public float tiltTime;

    bool first;

    Gyroscope gyro;

    // Start is called before the first frame update
    void Start()
    {

        gyro = Input.gyro;
        gyro.enabled = true;
        gyroUp = gyroDown = false;
        first = true;
    }

    void FixedUpdate() 
    {
        //get y rotation
        rotationRateY = (int) gyro.rotationRateUnbiased.y;
        //print(rotationRateY); //debug

        StartCoroutine(main());

        if (Input.GetKeyDown(KeyCode.UpArrow))
            StartCoroutine(nextCard(false));
        if (Input.GetKeyDown(KeyCode.DownArrow))
            StartCoroutine(nextCard(true));
    }

    //check for conclusive gyro signals & message Play.cs accordingly
    IEnumerator main()
    {
        //singleton-esque contraption
        if (!first) {
            yield break;
        }
        first = false;


        if (rotationRateY > tiltThreshold) { //skip
            yield return new WaitForSeconds(tiltTime);
            if (rotationRateY > -tiltThreshold) {
                print("UP!"); //debug
                gyroUp = true;
                StartCoroutine(nextCard(false));
            }
        }
        if (rotationRateY < -tiltThreshold) { //correct guess
            yield return new WaitForSeconds(tiltTime);
            if (rotationRateY < tiltThreshold) {
                print("DOWN!"); //debug
                gyroDown = true;
                StartCoroutine(nextCard(true));
            }
        }

        gyroUp = gyroDown = false;

        //reset running status
        first = true;
    }


    //convert messy gyro signals into conclusive boolean outputs
    IEnumerator nextCard(bool success) 
    {  
        gyroUp = gyroDown = false;
        playManager.nextCard(success);
        yield return new WaitForSeconds(2);
    }


    public static Vector3 ToEulerAngles(Quaternion q) //thank you Sha !!! https://stackoverflow.com/a/70462919
    {
        Vector3 angles = new Vector3();

        // roll / x
        float sinr_cosp = 2 * (q.w * q.x + q.y * q.z);
        float cosr_cosp = 1 - 2 * (q.x * q.x + q.y * q.y);
        angles.x = (float)Mathf.Atan2(sinr_cosp, cosr_cosp);

        // pitch / y
        float sinp = 2 * (q.w * q.y - q.z * q.x);
        if (Mathf.Abs(sinp) >= 1)
        {
            angles.y = (float) (Mathf.PI / 2 * Mathf.Sign(sinp));
        }
        else
        {
            angles.y = (float)Mathf.Asin(sinp);
        }

        // yaw / z
        float siny_cosp = 2 * (q.w * q.z + q.x * q.y);
        float cosy_cosp = 1 - 2 * (q.y * q.y + q.z * q.z);
        angles.z = (float)Mathf.Atan2(siny_cosp, cosy_cosp);

        return angles;
    }

}
