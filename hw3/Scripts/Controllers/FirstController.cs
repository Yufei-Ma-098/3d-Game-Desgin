using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//主控制器，负责游戏主要运行逻辑以及协调各控制器之间的工作
public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    private LandModelController rightLandRoleController;                        //右岸控制器
    private LandModelController leftLandRoleController;                         //左岸控制器
    private RoleModelController[] roleModelControllers;                         //人物控制器集合
    private MoveController moveController;                                      //移动控制器
    private bool isRuning;                                                      //游戏进行状态
    private float time;                                                         //游戏进行时间
    private RiverModel riverModel;                                              //河流Model
    private BoatController boatRoleController;                                  //船控制器

    //导入资源
    public void LoadResources()
    {
        //初始化
        roleModelControllers = new RoleModelController[6];
        for (int i = 0; i < 6; i++)
        {
            roleModelControllers[i] = new RoleModelController();
            roleModelControllers[i].CreateRole(PositionModel.roles[i], i < 3 ? true : false, i);
        }
        
        leftLandRoleController = new LandModelController();
        leftLandRoleController.CreateLand("left_land", PositionModel.left_land);
        rightLandRoleController = new LandModelController();
        rightLandRoleController.CreateLand("right_land", PositionModel.right_land); 
        foreach (RoleModelController roleModelController in roleModelControllers)
        {
            roleModelController.GetRoleModel().role.transform.localPosition = leftLandRoleController.AddRole(roleModelController.GetRoleModel());
        }
        riverModel = new RiverModel(PositionModel.river);
        boatRoleController = new BoatController();
        boatRoleController.CreateBoat(PositionModel.left_boat);
        moveController = new MoveController();
        isRuning = true;
        time = 60;
    }

    //移动船,首先判断是否可以移动，以及船在哪一侧，接着将船移向相反的位置。
    public void MoveBoat()
    {
        if ((!isRuning) || moveController.GetIsMoving())
            return;
        if (boatRoleController.GetBoatModel().isRight)
            moveController.SetMove(PositionModel.left_boat, boatRoleController.GetBoatModel().boat);
        else
            moveController.SetMove(PositionModel.right_boat, boatRoleController.GetBoatModel().boat);
        boatRoleController.GetBoatModel().isRight = !boatRoleController.GetBoatModel().isRight;
    }

    //移动人物，判断当前游戏是否在进行，同时是否有对象正在移动，然后判断人在船上还是岸上，再移动
    public void MoveRole(RoleModel roleModel)
    {
        //
        if ((!isRuning) || moveController.GetIsMoving())
            return;
 
        if (roleModel.isInBoat)
        {
            if (boatRoleController.GetBoatModel().isRight)
                moveController.SetMove(rightLandRoleController.AddRole(roleModel), roleModel.role);
            else
                moveController.SetMove(leftLandRoleController.AddRole(roleModel), roleModel.role);
            roleModel.isRight = boatRoleController.GetBoatModel().isRight;
            boatRoleController.RemoveRole(roleModel);
        }
        else
        {
            if (boatRoleController.GetBoatModel().isRight == roleModel.isRight)
            {
                if (roleModel.isRight)
                {
                    rightLandRoleController.RemoveRole(roleModel);
                }
                else
                {
                    leftLandRoleController.RemoveRole(roleModel);
                }
                moveController.SetMove(boatRoleController.AddRole(roleModel), roleModel.role);
            }
        }
    }

    //游戏重置
    public void Restart()
    {
        time = 60;
        leftLandRoleController.CreateLand("left_land", PositionModel.left_land);
        rightLandRoleController.CreateLand("right_land", PositionModel.right_land);
        for (int i = 0; i < 6; i++)
        {
            roleModelControllers[i].CreateRole(PositionModel.roles[i], i < 3 ? true : false, i);
            roleModelControllers[i].GetRoleModel().role.transform.localPosition = leftLandRoleController.AddRole(roleModelControllers[i].GetRoleModel());
        }
        boatRoleController.CreateBoat(PositionModel.left_boat);
        isRuning = true;
    }

    //检测游戏状态
    public void Check()
    {
        if (!isRuning)
            return;
        this.gameObject.GetComponent<UserGUI>().gameMessage = "";
        //判断是否已经胜利
        if (rightLandRoleController.GetLandModel().priestNum == 3)
        {
            this.gameObject.GetComponent<UserGUI>().gameMessage = "You Win!";
            isRuning = false;
        }
        else
        {
            /*判断是否已经失败
             若任意一侧，牧师数量不为0且牧师数量少于恶魔数量，则游戏失败
             */
            int leftPriestNum, leftDevilNum, rightPriestNum, rightDevilNum;
            leftPriestNum = leftLandRoleController.GetLandModel().priestNum + (boatRoleController.GetBoatModel().isRight ? 0 : boatRoleController.GetBoatModel().priestNum);
            leftDevilNum = leftLandRoleController.GetLandModel().devilNum + (boatRoleController.GetBoatModel().isRight ? 0 : boatRoleController.GetBoatModel().devilNum);
            if (leftPriestNum != 0 && leftPriestNum < leftDevilNum)
            {
                this.gameObject.GetComponent<UserGUI>().gameMessage = "Game Over!";
                isRuning = false;
            }
            rightPriestNum = rightLandRoleController.GetLandModel().priestNum + (boatRoleController.GetBoatModel().isRight ? boatRoleController.GetBoatModel().priestNum : 0);
            rightDevilNum = rightLandRoleController.GetLandModel().devilNum + (boatRoleController.GetBoatModel().isRight ? boatRoleController.GetBoatModel().devilNum : 0);
            if (rightPriestNum != 0 && rightPriestNum < rightDevilNum)
            {
                this.gameObject.GetComponent<UserGUI>().gameMessage = "Game Over!";
                isRuning = false;
            }
        }
    }



    void Awake()
    {
        SSDirector.GetInstance().CurrentSenceController = this;
        LoadResources();
        this.gameObject.AddComponent<UserGUI>();
    }

    void Update()
    {
        if (isRuning)
        {
            time -= Time.deltaTime;
            this.gameObject.GetComponent<UserGUI>().time = (int)time;
            if (time <= 0)
            {
                this.gameObject.GetComponent<UserGUI>().time = 0;
                this.gameObject.GetComponent<UserGUI>().gameMessage = "Game Over!";
                isRuning = false;
            }
        }
    }

}
