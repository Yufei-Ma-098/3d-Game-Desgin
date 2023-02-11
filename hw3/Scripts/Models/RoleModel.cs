using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleModel{ 
    public GameObject role;             //角色
    public bool isPriest;               //牧师或恶魔
    public bool isRight;                //左侧或右侧
    public bool isInBoat;               //船上或岸上
    public int tag;                     //标记对象
    
    public RoleModel(Vector3 position, bool isPriest, int tag)
    {
        this.isPriest = isPriest;
        isRight = false;
        isInBoat = false;
        this.tag = tag;
     
        role = GameObject.Instantiate(Resources.Load("Prefabs/"+(isPriest?"priest":"devil"), typeof(GameObject))) as GameObject;
        role.transform.localScale = new Vector3(1,1,1);
        role.transform.position = position;
        role.name = "role" + tag;
        role.AddComponent<Click>();
        role.AddComponent<BoxCollider>();
    }
}
