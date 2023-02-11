using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleModelController :ClickAction
{
    RoleModel roleModel;                                       
    IUserAction userAction;

    //处理RoleModel的事件，实现ClickAction的接口，携带IUserAction的接口成员
    public RoleModelController()
    {
        userAction = SSDirector.GetInstance().CurrentSenceController as IUserAction;
    }

    //创建或者初始化RoleModel
    public void CreateRole(Vector3 position, bool isPriest, int tag)
    {
        if (roleModel != null)
            Object.DestroyImmediate(roleModel.role);
        roleModel = new RoleModel(position, isPriest, tag);
        roleModel.role.GetComponent<Click>().setClickAction(this);
    }

    //返回controller管理的RoleModel
    public RoleModel GetRoleModel()
    {
        return roleModel;
    }

    //处理RoleModel的点击事件
    public void DealClick()
    {
        userAction.MoveRole(roleModel);
    }
}
