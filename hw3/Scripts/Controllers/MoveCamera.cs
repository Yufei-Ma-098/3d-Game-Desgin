using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    //调整了主摄像机的位置
    void Start()
    {
        transform.position = new Vector3((float)1.11,1, (float)-18.32);        
    }
}
