using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBox2 : MonoBehaviour {

    public float tumblingDuration = 0.2f;

    void Update()
    {
        var dir = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
            dir = Vector3.forward;

        if (Input.GetKey(KeyCode.DownArrow))
            dir = Vector3.back;

        if (Input.GetKey(KeyCode.LeftArrow))
            dir = Vector3.left;

        if (Input.GetKey(KeyCode.RightArrow))
            dir = Vector3.right;

        if (dir != Vector3.zero && !isTumbling)
        {
            StartCoroutine(Tumble(dir));
        }
    }

    bool isTumbling = false;
    IEnumerator Tumble(Vector3 direction)
    {
        isTumbling = true;

        var rotAxis = Vector3.Cross(Vector3.up/2, direction);
        var pivot = (transform.position + Vector3.down * 0.5f) + direction * 0.5f;

        var startRotation = transform.rotation;
        var endRotation = Quaternion.AngleAxis(90.0f, rotAxis) * startRotation;

        var startPosition = transform.position;
        var endPosition = transform.position + direction;

        var rotSpeed = 90.0f / tumblingDuration;
        var t = 0.0f;

        while (t < tumblingDuration)
        {
            t += Time.deltaTime;
            transform.RotateAround(pivot, rotAxis, rotSpeed * Time.deltaTime);
            yield return null;
        }

        transform.rotation = endRotation;
        transform.position = endPosition;

        isTumbling = false;
    }
}

