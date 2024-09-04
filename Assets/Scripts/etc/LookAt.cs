using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        Vector3 r = transform.localEulerAngles;
        r.x = 0f;
        r.z = 0f;

        transform.localEulerAngles = r;
    }
}
