using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : ClickAction
{
    BoatModel boatModel;
    IUserAction userAction;

    //处理有关Boat的事件，实现ClickAction的接口，携带IUserAction的接口成员
    public BoatController()
    {
        userAction = SSDirector.GetInstance().CurrentSenceController as IUserAction;
    }

    //创建或者初始化BoatModel
    public void CreateBoat(Vector3 position)
    {
        if (boatModel != null)
            Object.DestroyImmediate(boatModel.boat);
        boatModel = new BoatModel(position);
        boatModel.boat.GetComponent<Click>().setClickAction(this);
    }

    //返回所管理的BoatModel
    public BoatModel GetBoatModel()
    {
        return boatModel;
    }

    //处理人物上船的一些数据变动
    public Vector3 AddRole(RoleModel roleModel)
    {
        //判断船上的两个位置是否为空
        if (boatModel.roles[0] == null)
        {
            boatModel.roles[0] = roleModel;
            roleModel.isInBoat = true;
            roleModel.role.transform.parent = boatModel.boat.transform;
            if (roleModel.isPriest)
                boatModel.priestNum++;
            else
                boatModel.devilNum++;
            return PositionModel.boatRoles[0];

        }
        if (boatModel.roles[1] == null)
        {
            boatModel.roles[1] = roleModel;
            roleModel.isInBoat = true;
            roleModel.role.transform.parent = boatModel.boat.transform;
            if (roleModel.isPriest)
                boatModel.priestNum++;
            else
                boatModel.devilNum++;
            return PositionModel.boatRoles[1];
        }
        return roleModel.role.transform.localPosition;
    }

    //将角色从船上移除
    public void RemoveRole(RoleModel roleModel)
    {
        //判断穿上的两个位置当中有没有要移除的角色
        if (boatModel.roles[0] == roleModel)
        {
            boatModel.roles[0] = null;
            if (roleModel.isPriest)
                boatModel.priestNum--;
            else
                boatModel.devilNum--;
        }
        if (boatModel.roles[1] == roleModel)
        {
            boatModel.roles[1] = null;
            if (roleModel.isPriest)
                boatModel.priestNum--;
            else
                boatModel.devilNum--;
        }
    }

    //点击船
    public void DealClick()
    {
        if (boatModel.roles[0] != null || boatModel.roles[1] != null)
            userAction.MoveBoat();
    }
}
