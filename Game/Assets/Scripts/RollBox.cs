using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBox : MonoBehaviour {

    public float rotationPeriod = 0.3f;
    public float sideLength = 1f;

    bool isRotate = false;
    float directionX = 0;
    float directionZ = 0;

    Vector3 startPos;
    float rotationTime = 0;
    float radius;
    Quaternion fromRotation;
    Quaternion toRotation;

	// Use this for initialization
	void Start () {
        radius = sideLength * Mathf.Sqrt(2f) / 2f;	
	}
	
	// Update is called once per frame
	void Update () {
        float x = 0;
        float y = 0;

        x = Input.GetAxisRaw("Vertical");
        if (x == 0)
        {
            y = Input.GetAxisRaw("Horizontal"); 
        }

        if ((x != 0 || y != 0) && !isRotate)
        {
            directionX = -y;                                                             
            directionZ = x;                                                             
            startPos = transform.position;                                              
            fromRotation = transform.rotation;                                          
            transform.Rotate(directionZ * 90, 0, directionX * 90, Space.World);     
            toRotation = transform.rotation;                                            
            transform.rotation = fromRotation;                                          
            rotationTime = 0;                                                           
            isRotate = true;                                                            
        }

    }

    private void FixedUpdate()
    {
        if (isRotate)
        {

            rotationTime += Time.fixedDeltaTime;                                    
            float ratio = Mathf.Lerp(0, 1, rotationTime / rotationPeriod);          

            float thetaRad = Mathf.Lerp(0, Mathf.PI / 2f, ratio);                  
            float distanceX = (-directionX * radius * (Mathf.Cos(45f * Mathf.Deg2Rad) - Mathf.Cos(45f * Mathf.Deg2Rad + thetaRad)));     
            float distanceY = radius * (Mathf.Sin(45f * Mathf.Deg2Rad + thetaRad) - Mathf.Sin(45f * Mathf.Deg2Rad));                        
            float distanceZ = directionZ * radius * (Mathf.Cos(45f * Mathf.Deg2Rad) - Mathf.Cos(45f * Mathf.Deg2Rad + thetaRad));           
            transform.position = new Vector3(startPos.x + distanceX, startPos.y + distanceY, startPos.z + distanceZ);                      

            
            transform.rotation = Quaternion.Lerp(fromRotation, toRotation, ratio);      

            
            if (ratio == 1)
            {
                isRotate = false;
                directionX = 0;
                directionZ = 0;
                rotationTime = 0;
            }
        }
    }
}
