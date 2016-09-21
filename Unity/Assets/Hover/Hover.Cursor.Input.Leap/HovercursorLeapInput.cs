﻿using System;
using System.Collections.Generic;
using Hover.Common.Input;
using Hover.Common.Input.Leap;
using Hover.Common.Util;
using Leap;
using Leap.Unity;

namespace Hover.Cursor.Input.Leap {

	/*================================================================================================*/
	public class HovercursorLeapInput : HovercursorInput {

		private LeapProvider vLeapProvider;
		private List<InputCursor> vCursors;
		private Dictionary<CursorType, InputCursor> vCursorMap;
		private Dictionary<CursorType, bool> vSideMap;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public virtual void Awake() {
			vLeapProvider = GetComponent<LeapProvider>();
			vCursors = new List<InputCursor>();
			vCursorMap = new Dictionary<CursorType, InputCursor>(EnumIntKeyComparer.CursorType);
			vSideMap = new Dictionary<CursorType, bool>(EnumIntKeyComparer.CursorType);
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public override void UpdateInput() {
			Frame frame = vLeapProvider.CurrentFrame;
			Hand leapHandL = LeapUtil.GetValidLeapHand(frame, true);
			Hand leapHandR = LeapUtil.GetValidLeapHand(frame, false);

			if ( !IsEnabled ) {
				leapHandL = null;
				leapHandR = null;
			}

			foreach ( InputCursor cursor in vCursors ) {
				cursor.UpdateWithHand(vSideMap[cursor.Type] ? leapHandL : leapHandR);
			}
		}

		/*--------------------------------------------------------------------------------------------*/
		public override IInputCursor GetCursor(CursorType pType) {
			if ( pType == CursorType.Look ) {
				throw new Exception("The "+typeof(HovercursorLeapInput)+" component does not support "+
					"the use of "+typeof(CursorType)+"."+pType+".");
			}

			if ( !vCursorMap.ContainsKey(pType) ) {
				var cursor = new InputCursor(pType);

				vCursors.Add(cursor);
				vCursorMap.Add(pType, cursor);
				vSideMap.Add(pType, CursorTypeUtil.IsLeft(pType));
			}

			return vCursorMap[pType];
		}

	}

}
