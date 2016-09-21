
using UnityEngine;
using System.Collections;
using Hover.Cast.Items;
using Hover.Common.Items;
using Hover.Common.Items.Types;

public class StateMachine : MonoBehaviour {

    public HovercastItem TrainItem;
    public HovercastItem ExecuteItem;
    public HovercastItem StopItem;

    public int TrainFlag = 0;
    public int StopFlag = 0;
    public int ExecuteFlag = 0;
    public int TrainingCompletedFlag = 0;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        IStickyItem TrainItemBehavior = (IStickyItem)TrainItem.GetItem();
        IStickyItem StopItemBehavior = (IStickyItem)StopItem.GetItem();
        IStickyItem ExecuteItemBehavior = (IStickyItem)ExecuteItem.GetItem();

        if (TrainingCompletedFlag == 1 && TrainFlag == 0 && ExecuteFlag == 0 )
        {
            ExecuteItemBehavior.IsEnabled = true;
        }

        if (TrainingCompletedFlag == 0 || TrainFlag == 1 || ExecuteFlag == 1)
        {
            ExecuteItemBehavior.IsEnabled = false;
        }

        if (TrainFlag == 1 || ExecuteFlag == 1)
        {
            StopItemBehavior.IsEnabled = true;
        }

        if (TrainFlag == 0 && ExecuteFlag == 0)
        {
            StopItemBehavior.IsEnabled = false;
        }


        if (TrainFlag == 1|| ExecuteFlag == 1)
        {
            TrainItemBehavior.IsEnabled = false;
        }

        if (ExecuteFlag == 0 && TrainFlag == 0)
        {
            TrainItemBehavior.IsEnabled = true;
        }


        if (StopFlag == 1 && TrainFlag == 1)
        {
            StopFlag = 0;
            TrainFlag = 0;
        }

        if (StopFlag == 1 && ExecuteFlag == 1)
        {
            StopFlag = 0;
            ExecuteFlag = 0;
        }
                
    }
}
