using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollow : MonoBehaviour
{
    public GameObject player;
    public float cameraDist = 20.0f;
    public float cameraHeight = 10.0f;

    void Update()
    {
        Vector3 pos = player.transform.position;
        pos.z += cameraDist;
        pos.y += cameraHeight;
        transform.position = pos;
    }
}
