using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用户交互接口
public interface IUserAction
{
    void MoveBoat();
    void MoveRole(RoleModel roleModel);
    void Restart();

    void Check();
}
