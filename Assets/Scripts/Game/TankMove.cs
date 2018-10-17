using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{

    float fZ;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.z < 19.0f)
        {
            var Rot = this.transform.localRotation;
            fZ = 8.0f * Time.deltaTime;
            Rot.y = 0.0f;
            this.transform.rotation = Rot;
            this.transform.Translate(0.0f, 0.0f, fZ);
            this.transform.position = new Vector3(0.0f, 0.0f, this.transform.position.z);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && this.transform.position.z > -19.0f)
        {
            var Rot = this.transform.localRotation;
            fZ = 8.0f * Time.deltaTime;
            Rot.y = 180.0f;
            this.transform.rotation = Rot;
            this.transform.Translate(0.0f, 0.0f, fZ);
            this.transform.position = new Vector3(0.0f, 0.0f, this.transform.position.z);
        }
    }
}
