using Hover.Cast.Items;
using Hover.Common.Items;
using Hover.Common.Items.Types;
using UnityEngine;


namespace RoboticsVR.HoverCastMenu.Items
{

    /*================================================================================================*/
    public class ExecuteSwitchListener : BaseListener<IStickyItem>
    {
        public static int Executeflag ;
        public static int HapticIndexFeedback ;

        public GameObject Haptics;
        public GameObject StateMachine;


        ////////////////////////////////////////////////////////////////////////////////////////////////
        /*--------------------------------------------------------------------------------------------*/
        protected override void Setup()
        {
            base.Setup();
            Item.OnSelected += HandleSelected;
            Item.OnDeselected += HandleDeselected;


        }

        /*--------------------------------------------------------------------------------------------*/
        protected override void BroadcastInitialValue()
        {
            //do nothing...
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////
        /*--------------------------------------------------------------------------------------------*/
        private void HandleSelected(ISelectableItem pItem)
        {

            Executeflag = 1;
            print("Execute Selected");
            HapticFeedback HapticFlags = Haptics.GetComponent<HapticFeedback>();
            HapticFlags.HapticIndexFeedback = 1;

            StateMachine StateMachineFlags = StateMachine.GetComponent<StateMachine>();
            StateMachineFlags.ExecuteFlag = 1;


        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        /*--------------------------------------------------------------------------------------------*/
        private void HandleDeselected(ISelectableItem pItem)
        {

            print("Execute Deselected");
            HapticFeedback HapticFlags = Haptics.GetComponent<HapticFeedback>();
            HapticFlags.HapticIndexFeedback = 0;

        }
    }
}
