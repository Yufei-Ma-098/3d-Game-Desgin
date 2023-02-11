using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeController : MonoBehaviour
{
    public FirstController mainController;
    public LandModel leftLandModel;
    public LandModel rightLandModel;
    public BoatModel boatModel;
    // Start is called before the first frame update
    void Start()
    {
        mainController = (FirstController)SSDirector.GetInstance().CurrentSenceController;
        this.leftLandModel = mainController.leftLandController.GetLandModel();
        this.rightLandModel = mainController.rightLandController.GetLandModel();
        this.boatModel = mainController.boatController.GetBoatModel();
    }

    // Update is called once per frame
    void Update()
    {
        if (!mainController.isRuning)
            return;
        if (mainController.time <= 0)
        {
            mainController.JudgeCallback(false, "Game Over!");
            return;
        }
        this.gameObject.GetComponent<UserGUI>().gameMessage = "";
        //判断是否已经胜利
        if (rightLandModel.priestNum == 3)
        {
            mainController.JudgeCallback(false, "You Win!");
            return;
        }
        else
        {
            int leftPriestNum, leftDevilNum, rightPriestNum, rightDevilNum;
            leftPriestNum = leftLandModel.priestNum + (boatModel.isRight ? 0 : boatModel.priestNum);
            leftDevilNum = leftLandModel.devilNum + (boatModel.isRight ? 0 : boatModel.devilNum);
            if (leftPriestNum != 0 && leftPriestNum < leftDevilNum)
            {
                mainController.JudgeCallback(false, "Game Over!");
                return;
            }
            rightPriestNum = rightLandModel.priestNum + (boatModel.isRight ? boatModel.priestNum : 0);
            rightDevilNum = rightLandModel.devilNum + (boatModel.isRight ? boatModel.devilNum : 0);
            if (rightPriestNum != 0 && rightPriestNum < rightDevilNum)
            {
                mainController.JudgeCallback(false, "Game Over!");
                return;
            }
        }
    }
}
