using Hover.Cast.Items;
using Hover.Common.Items;
using Hover.Common.Items.Types;
using System.Collections;
using System.IO.Ports;

namespace RoboticsVR.HoverCastMenu.Items
{

    /*================================================================================================*/
    public class SwitchListener : BaseListener<IStickyItem>
    {
        // SerialPort sp = new SerialPort("COM1", 115200);
        public int mimicflag = 0;


        public HovercastItem OtherItem;

        ////////////////////////////////////////////////////////////////////////////////////////////////
        /*--------------------------------------------------------------------------------------------*/
        protected override void Setup()
        {
            base.Setup();


           // sp.ReadTimeout = 1;
          //  sp.Open();
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

          //  sp.Write("1");
            if (pItem.Label == "Mimic")
            {


                print("Mimic Selected");


              
             //   ISelectorItem OtherItemBehavior = (ISelectorItem)OtherItem.GetItem();
               // OtherItemBehavior.IsEnabled = true;

               // OtherItemBehavior.;

            }

            else
            {
                print("Execute Selected");

            }

           
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        /*--------------------------------------------------------------------------------------------*/
        private void HandleDeselected(ISelectableItem pItem)
        {

           // sp.Write("0");
            print("Mimic Deselected");

              ISelectorItem OtherItemBehavior = (ISelectorItem)OtherItem.GetItem();
              OtherItemBehavior.IsEnabled = true;
        }
    }
}
