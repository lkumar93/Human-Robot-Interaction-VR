﻿using Hover.Cast;
using Hover.Cast.Custom;
using Hover.Cast.Items;
using Hover.Common.Items;
using UnityEngine;

namespace RoboticsVR.HoverCastMenu
{

    /*================================================================================================*/
    public abstract class BaseListener<T> : HovercastItemListener<T> where T : ISelectableItem
    {

        protected HovercastSetup CastSetup { get; private set; }
        protected HovercastItemVisualSettings ItemSett { get; private set; }
        protected InteractionSettings InteractSett { get; private set; }


        ////////////////////////////////////////////////////////////////////////////////////////////////
        /*--------------------------------------------------------------------------------------------*/
        protected override void Setup()
        {
           
            CastSetup = GameObject.Find("Hovercast").GetComponent<HovercastSetup>();
            ItemSett = CastSetup.DefaultItemVisualSettings;
            InteractSett = CastSetup.InteractionSettings.GetSettings();
        }

    }

}