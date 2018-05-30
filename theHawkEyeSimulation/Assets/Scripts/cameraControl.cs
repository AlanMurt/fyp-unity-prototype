using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour {

    public Transform Camera1, Camera2, Ball;
    Camera cam1, cam2;
    // Use this for initialization

    GameObject ourGuess;
    void Start() {
        //ourGuess = GameObject.CreatePrimitive(PrimitiveType.Sphere);


        cam1 = Camera1.gameObject.GetComponent<Camera>();
        cam2 = Camera2.gameObject.GetComponent<Camera>();
    } 

    // Update is called once per frame
    void Update() {



        Vector2 simulatedPositionOnScreenFromCam1 = cam1.WorldToScreenPoint(Ball.position);

        simulatedPositionOnScreenFromCam1 = new Vector2(Mathf.RoundToInt(simulatedPositionOnScreenFromCam1.x) + 5.0f, Mathf.RoundToInt(simulatedPositionOnScreenFromCam1.y) - 4.0f);


        Vector2 simulatedPositionOnScreenFromCam2 = cam2.WorldToScreenPoint(Ball.position);

        simulatedPositionOnScreenFromCam2 = new Vector2(Mathf.RoundToInt(simulatedPositionOnScreenFromCam2.x), Mathf.RoundToInt(simulatedPositionOnScreenFromCam2.y));


        Ray Camera1Ray = cam1.ScreenPointToRay(simulatedPositionOnScreenFromCam1);

        Ray Camera2Ray = cam2.ScreenPointToRay(simulatedPositionOnScreenFromCam2);

        Debug.DrawRay(Camera1Ray.origin, 100 * Camera1Ray.direction, Color.blue);


        Debug.DrawRay(Camera2Ray.origin, 100 * Camera2Ray.direction, Color.red);

        //ourGuess.transform.position = nearestPoint(Camera1Ray, Camera2Ray);

        //print(cam2.WorldToScreenPoint(Ball.position));
        print(nearestPoint(Camera1Ray, Camera2Ray));
    }

    Vector3 nearestPoint(Ray cam1, Ray cam2)
    {
        Vector3 D1, D2, D3, P1, P2;

        P1 = cam1.origin;
        P2 = cam2.origin;
        D1 = cam1.direction;
        D2 = cam2.direction;

        D3 = Vector3.Cross(D1, D2);

        Vector4 col1 = new Vector4(D1.x, -D2.x, D3.x, 0);
        Vector4 col2 = new Vector4(D1.y, -D2.y, D3.y, 0);
        Vector4 col3 = new Vector4(D1.z, -D2.z, D3.z, 0);
        Vector4 col4 = new Vector4(0, 0, 0, 1);



        Matrix4x4 m = new Matrix4x4(col1, col2, col3, col4);

        m = m.transpose;

        Matrix4x4 inv = m.inverse;

        Vector4 constants = new Vector4((P2 - P1).x, (P2 - P1).y, (P2 - P1).z, 0);
        Vector4 soln = inv * constants;

        return ((P1 + soln.x * D1) + (P2 + soln.y * D2)) / 2;




    }



}
