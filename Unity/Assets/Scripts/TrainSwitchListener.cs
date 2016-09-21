using Hover.Cast.Items;
using Hover.Common.Items;
using Hover.Common.Items.Types;
using UnityEngine;

namespace RoboticsVR.HoverCastMenu.Items
{
    /*================================================================================================*/
    public class TrainSwitchListener : BaseListener<IStickyItem>
    {
        public static int Trainflag;
        public static int HapticIndexFeedback;
        public static int HapticPalmFeedback;

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
            
                Trainflag = 1;
                print("Train Selected");
                HapticFeedback HapticFlags = Haptics.GetComponent<HapticFeedback>();
                HapticFlags.HapticIndexFeedback = 1;

                StateMachine StateMachineFlags = StateMachine.GetComponent<StateMachine>();
                StateMachineFlags.TrainFlag = 1;

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        /*--------------------------------------------------------------------------------------------*/
        private void HandleDeselected(ISelectableItem pItem)
        {

            print("Train Deselected");
            HapticFeedback HapticFlags = Haptics.GetComponent<HapticFeedback>();
            HapticFlags.HapticIndexFeedback = 0;

        }

    }
}
