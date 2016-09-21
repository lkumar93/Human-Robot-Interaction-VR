﻿using System;
using Hover.Common.Input;
using Hover.Common.Input.Leap;
using Leap;
using UnityEngine;
using Leap.Unity;

namespace Hover.Cursor.Input.Leap {

	/*================================================================================================*/
	public class InputCursor : IInputCursor {

		private const float SizeScaleFactor = 1/0.16f;

		public CursorType Type { get; private set; }
		public bool IsAvailable { get; private set; }

		public Vector3 Position { get; private set; }
		public Quaternion Rotation { get; private set; }
		public float Size { get; private set; }

		private readonly Finger.FingerType? vLeapFingerType;
		private readonly bool vIsPalm;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public InputCursor(CursorType pType) {
			Type = pType;
			vLeapFingerType = LeapUtil.GetFingerType(pType);
			vIsPalm = CursorTypeUtil.IsPalm(pType);
		}
		
		/*--------------------------------------------------------------------------------------------*/
		public void UpdateWithHand(Hand pLeapHand) {
			if ( pLeapHand == null ) {
				UpdateForNull();
				return;
			}
			
			if ( vLeapFingerType != null ) {
				UpdateForFinger(pLeapHand, (Finger.FingerType)vLeapFingerType);
				return;
			}
			
			if ( vIsPalm ) {
				UpdateForPalm(pLeapHand);
				return;
			}

			throw new Exception("Unhandled CursorType: "+Type);
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		private void UpdateForNull() {
			IsAvailable = false;
			Position = Vector3.zero;
			Rotation = Quaternion.identity;
		}

		/*--------------------------------------------------------------------------------------------*/
		private void UpdateForFinger(Hand pLeapHand, Finger.FingerType pLeapFingerType) {
			Finger leapFinger = LeapUtil.GetValidFinger(pLeapHand, pLeapFingerType);

			if ( leapFinger == null ) {
				UpdateForNull();
				return;
			}

			Bone bone = leapFinger.Bone(Bone.BoneType.TYPE_DISTAL); //GC_ALLOC

			IsAvailable = true;
			Position = leapFinger.TipPosition.ToVector3(); //GC_ALLOC
			Rotation = bone.Basis.Rotation(); //GC_ALLOC

			Size = leapFinger.Width*SizeScaleFactor;
		}

		/*--------------------------------------------------------------------------------------------*/
		private void UpdateForPalm(Hand pLeapHand) {
			IsAvailable = true;
			Position = pLeapHand.PalmPosition.ToVector3();
			Rotation = pLeapHand.Basis.Rotation(); //GC_ALLOC
			Size = pLeapHand.PalmWidth*SizeScaleFactor;
		}

	}

}
