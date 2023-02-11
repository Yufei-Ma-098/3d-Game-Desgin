# 空间与运动
## 一. 简答并用程序验证【建议做】

### 1. 游戏对象运动的本质是什么？
游戏对象 position、scale、rotation 属性的变化。
### 请用三种方法以上方法，实现物体的抛物线运动。（如，修改Transform属性，使用向量Vector3的方法…）

 1. 修改 Transform
```csharp
public int speed = 2;

void Update()
{
    transform.position += new Vector3(Time.deltaTime * speed, -Time.deltaTime * speed * (2 * transform.position.x + Time.deltaTime * speed), 0);
}

```

 2. 使用 Transform.translate
 ```csharp
	public int speedX = 1; //单位时间内x方向的位移
    public int speedY = 1; //单位时间内y方向的位移
    public int t = 1; //时间
    
    //Update函数每帧调用一次
    void Update()
	{
    	transform.Translate(Vector3.right * Time.deltaTime * xSpeed + Vector3.down * Time.deltaTime * ySpeed * Time.deltaTime * T);
    T++;
	}
```
 3. 使用 Vector3
 

```csharp
	public int speedX = 1; //单位时间内x方向的位移
    public int speedY = 1; //单位时间内y方向的位移
    public int t = 1; //时间
    
    //Update函数每帧调用一次
    void Update()
	{
    	transform.position += Vector3.right * Time.deltaTime * speedX;
    	transform.position += Vector3.down * Time.deltaTime * speedY* Time.deltaTime * t;
    	t++;
	}
```
### 3. 写一个程序，实现一个完整的太阳系， 其他星球围绕太阳的转速必须不一样，且不在一个法平面上。

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

  public Transform Sun;
  public Transform Moon;
  public Transform Mercury;//水星
  public Transform Venus;//金星
  public Transform Earth;//地球
  public Transform Mars;//火星
  public Transform Jupiter;//木星
  public Transform Saturn;//土星
  public Transform Uranus;//天王星
  public Transform Neptune;//海王星

  // Use this for initialization
  void Start () {
      Sun.position = Vector3.zero;
  }

  // Update is called once per frame
  void Update () {
      Mercury.RotateAround (Sun.position, new Vector3(0, 5, 1), 60 * Time.deltaTime);
      Mercury.Rotate ( new Vector3(0, 5, 1) * 10000 / 58 * Time.deltaTime);

      Venus.RotateAround (Sun.position, new Vector3(0, 2, 1), 55 * Time.deltaTime);
      Venus.Rotate (new Vector3(0, 2, 1) * 10000/ 243 *Time.deltaTime);

      Earth.RotateAround (Sun.position, Vector3.up, 50 * Time.deltaTime);
      Earth.Rotate (Vector3.up * 30 * Time.deltaTime);
      Moon.transform.RotateAround (Earth.position, Vector3.up, 5 * Time.deltaTime);

      Mars.RotateAround (Sun.position, new Vector3(0, 12, 5), 45 * Time.deltaTime);
      Mars.Rotate (new Vector3(0, 12, 5) * 10000 * Time.deltaTime);

      Jupiter.RotateAround (Sun.position, new Vector3(0, 10, 3), 35 * Time.deltaTime);
      Jupiter.Rotate (new Vector3(0, 10, 3) * 10000/0.3f * Time.deltaTime);

      Saturn.RotateAround (Sun.position, new Vector3(0, 3, 1), 20 * Time.deltaTime);
      Saturn.Rotate (new Vector3(0, 3, 1) * 10000/0.4f * Time.deltaTime);

      Uranus.RotateAround (Sun.position, new Vector3(0, 10, 1), 15 * Time.deltaTime);
      Uranus.Rotate (new Vector3(0, 10, 1) * 10000/0.6f * Time.deltaTime);

      Neptune.RotateAround (Sun.position, new Vector3(0, 8, 1), 10 * Time.deltaTime);
      Neptune.Rotate (new Vector3(0, 8, 1) * 10000/0.7f * Time.deltaTime);
  }
}

```

<br />

## 二. 编程实践
### 1. 阅读以下游戏脚本
Priests and Devils

> Priests and Devils is a puzzle game in which you will help the Priests
> and Devils to cross the river within the time limit. There are 3
> priests and 3 devils at one side of the river. They all want to get to
> the other side of this river, but there is only one boat and this boat
> can only carry two persons each time. And there must be one person
> steering the boat from one side to the other side. In the flash game,
> you can click on them to move them and click the go button to move the
> boat to the other direction. If the priests are out numbered by the
> devils on either side of the river, they get killed and the game is
> over. You can try it in many > ways. Keep all priests alive! Good
> luck!

程序需要满足的要求：

- play the game ( http://www.flash-game.net/game/2535/priests-and-devils.html )
- 列出游戏中提及的事物（Objects）
- 用表格列出玩家动作表（规则表），注意，动作越少越好
- 请将游戏中对象做成预制
- 在场景控制器 LoadResources 方法中加载并初始化 长方形、正方形、球 及其色彩代表游戏中的对象。
- 使用 C# 集合类型 有效组织对象
- 整个游戏仅 主摄像机 和 一个 Empty 对象， 其他对象必须代码动态生成！！！ 。 整个游戏不许出现 Find 游戏对象， SendMessage 这类突破程序结构的 通讯耦合 语句。 违背本条准则，不给分
- 请使用课件架构图编程，不接受非 MVC 结构程序
- 注意细节，例如：船未靠岸，牧师与魔鬼上下船运动中，均不能接受用户事件！

#### 玩家规则表


|动作|游戏状态|结果|
|--|--|--|
|点击左侧岸上的角色|角色在岸上，船在左岸，船有空位|角色上船|
|点击船|船上有人|船移动到对岸|
|点击左侧岸上的角色 |角色在船上，船在右岸|角色上岸|
| /|任意一岸的恶魔大于牧师且牧师数量不为0|游戏失败|
|/|右侧岸上有三个牧师|游戏胜利|
|/|时间超过60s|游戏失败|

#### 代码结构

 1. 游戏对象实现做成预制
 ![在这里插入图片描述](https://img-blog.csdnimg.cn/20201005205655567.png#pic_center)

 2. MVC结构
![在这里插入图片描述](https://img-blog.csdnimg.cn/20201005205719155.png#pic_center)
#### 代码：
#### Models
Models存储游戏对象的各种数据，只携带初始化函数，每个可能会变更数据的model都分配了一个controller。

##### （1）RoleModel.cs
```csharp
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
```

##### （2）LandModel.cs

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandModel
{
    public GameObject land;                     //岸
    public int priestNum, devilNum;             //岸上牧师与恶魔的数量
    public LandModel(string name, Vector3 position)
    {
        priestNum = devilNum = 0;
        land = GameObject.Instantiate(Resources.Load("Prefabs/land", typeof(GameObject))) as GameObject;
        land.name = name;
        land.transform.position = position;
        land.transform.localScale = new Vector3(13,5,3);
    }
}
```
##### （3）BoatModel.cs
```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatModel {
    public GameObject boat;                     //船
    public bool isRight;                        //左侧还是右侧
    public int priestNum, devilNum;             //船上牧师与恶魔的数量
    public RoleModel[] roles;                   //船上的角色的指针
    

    public BoatModel(Vector3 position)
    {
        priestNum = devilNum = 0;
        roles = new RoleModel[2];
        boat = GameObject.Instantiate(Resources.Load("Prefabs/boat", typeof(GameObject))) as GameObject;
        boat.name = "boat";
        boat.transform.position = position;
        boat.transform.localScale = new Vector3(4, (float)1.5, 3);
        boat.AddComponent<BoxCollider>();
        boat.AddComponent<Click>();
        isRight = false;
    }
}
```
##### （4）RiverModel.cs

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverModel
{
    private GameObject river;               //河流

    public RiverModel(Vector3 position)
    {
        river = Object.Instantiate(Resources.Load("Prefabs/river", typeof(GameObject))) as GameObject;
        river.name = "river";
        river.transform.position = position;
        river.transform.localScale = new Vector3(15, 2, 3);
    }
}
```
##### （5）PositionModel

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionModel
{
    public static Vector3 right_land = new Vector3(15, -4, 0);                  //右岸位置
    public static Vector3 left_land = new Vector3(-13, -4, 0);                  //左岸位置
    public static Vector3 river = new Vector3(1, -(float)5.5, 0);               //河流位置
    public static Vector3 right_boat = new Vector3(7, -(float)3.8, 0);          //船在右边的位置
    public static Vector3 left_boat = new Vector3(-5, -(float)3.8, 0);          //船在左边的位置
    //角色在岸上的相对位置
    public static Vector3[] roles = new Vector3[]{new Vector3((float)-0.2, (float)0.7, 0) ,new Vector3((float)-0.1, (float)0.7,0),
    new Vector3(0, (float)0.7,0),new Vector3((float)0.1, (float)0.7,0),new Vector3((float)0.2, (float)0.7,0),new Vector3((float)0.3, (float)0.7,0)};
    //角色在船上的相对位置
    public static Vector3[] boatRoles = new Vector3[] { new Vector3((float)-0.1, (float)1.2, 0), new Vector3((float)0.2, (float)1.2, 0) };
}


```
##### （6）click.cs

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    ClickAction clickAction;

    public void setClickAction(ClickAction clickAction)
    {
        this.clickAction = clickAction;
    }

    void OnMouseDown()
    {
        clickAction.DealClick();
    }
}
```
##### （7）clickAction.cs

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ClickAction
{
    void DealClick();
}
```
<br />

#### Controllers
Controllers实现游戏的主要逻辑和所有的功能函数，读取修改Models和Views当中的数据。

##### （1）RoleModelControllers.cs

```csharp
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
```
##### （2）BoatController.cs

```csharp
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
```
##### （3）LandModelController.cs

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandModelController
{
    private LandModel landModel;

    public void CreateLand(string name, Vector3 position)
    {
        if (landModel==null)
            landModel = new LandModel(name, position);
        landModel.priestNum = landModel.devilNum = 0;
    } 

    public LandModel GetLandModel()
    {
        return landModel;
    }

    //将人物添加到岸上，返回角色在岸上的相对坐标
    public Vector3 AddRole(RoleModel roleModel)
    {
        if (roleModel.isPriest)
            landModel.priestNum++;
        else
            landModel.devilNum++;
        roleModel.role.transform.parent = landModel.land.transform;
        roleModel.isInBoat = false;
        return PositionModel.roles[roleModel.tag];
    }

    //将角色从岸上移除
    public void RemoveRole(RoleModel roleModel)
    {
        if (roleModel.isPriest)
            landModel.priestNum--;
        else
            landModel.devilNum--;
    }
}

```
##### （4）Move.cs

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public bool isMoving = false;                   //判断当前对象是否在移动
    public float speed = 5;                         //移动速度
    public Vector3 destination;                     //目的地
    public Vector3 mid_destination;                 //中转地址   

    //为了避免穿模现象，为移动设置了一个中转地址，通过对象x,y坐标与中转地址的x,y坐标的关系来判断该往哪移动。
    void Update()
    {
        //已到达目的地，不进行移动
        if (transform.localPosition == destination)
        {
            isMoving = false;
            return;
        }

        isMoving = true;
        if (transform.localPosition.x != destination.x && transform.localPosition.y != destination.y)
        {
            //还未到达中转地址，向中转地址移动
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, mid_destination, speed * Time.deltaTime);
        }
        else
        {
            //以到达中转地址，向目的地移动
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, speed * Time.deltaTime);
        }
    }
}

```
##### （5）MoveController.cs

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制游戏的移动事件
public class MoveController
{
    private GameObject moveObject;                      //移动对象

    //判断当前是否在移动，返回当前是否在移动的状态(有物体在移动时，外部不允许其他操作)
    public bool GetIsMoving()
    {
        return (moveObject != null && moveObject.GetComponent<Move>().isMoving);
    }

    //设置新的移动
    public void SetMove(Vector3 destination, GameObject moveObject)
    {
        //判断新的对象是否已携带Move脚本,若不携带，则为其添加
        Move test;
        this.moveObject = moveObject;
        if (!moveObject.TryGetComponent<Move>(out test))
            moveObject.AddComponent<Move>();
        //设置目的地
        this.moveObject.GetComponent<Move>().destination = destination;
        //设置中转地址
        if (this.moveObject.transform.localPosition.y > destination.y)
            this.moveObject.GetComponent<Move>().mid_destination = new Vector3(destination.x, this.moveObject.transform.localPosition.y, destination.z);
        else
            this.moveObject.GetComponent<Move>().mid_destination = new Vector3(this.moveObject.transform.localPosition.x, destination.y, this.moveObject.transform.localPosition.z);
    }
}

```
##### （6）ISceneController.cs

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//场景控制器接口
public interface ISceneController
{
    void LoadResources();    
}

```

##### （7）SSDirector.cs

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//获得主控制器
public class SSDirector : System.Object
{
    private static SSDirector _instance;
    public ISceneController CurrentSenceController { get; set; }
    public static SSDirector GetInstance()
    {
        if (_instance == null)
        {
            _instance = new SSDirector();
        }
        return _instance;
    }
}

```
##### （8）IUserAction.cs

```csharp
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

```
##### （9）moveCamera.cs

```csharp
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

```
##### （10）FirstController.cs

```csharp
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

```
<br />

#### Views
##### UserGUI.cs

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//设置UI和接收用户交互
public class UserGUI : MonoBehaviour
{
    private IUserAction userAction;
    public string gameMessage;
    public int time;
    void Start()
    {
        time = 60;
        userAction = SSDirector.GetInstance().CurrentSenceController as IUserAction;
    }

    void OnGUI()
    {
        userAction.Check();
        //小字体初始化
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 30;

        //大字体初始化
        GUIStyle bigStyle = new GUIStyle();
        bigStyle.normal.textColor = Color.white;
        bigStyle.fontSize = 50;

        GUI.Label(new Rect(200, 30, 50, 200), "Priests & Devils", bigStyle);

        GUI.Label(new Rect(320, 100, 50, 200), gameMessage, style);

        GUI.Label(new Rect(0, 0, 100, 50), "Time: " + time, style);

        if(GUI.Button(new Rect(340, 160, 100, 50), "Restart"))
        {
            userAction.Restart();
        }
    }
}

```
