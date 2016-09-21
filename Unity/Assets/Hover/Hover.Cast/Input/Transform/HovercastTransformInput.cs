﻿namespace Hover.Cast.Input.Transform {

	/*================================================================================================*/
	public class HovercastTransformInput : HovercastInput {

		public bool IsAvailable = true;
		public float DisplayStrength = 1;
		public float NavigateBackStrength = 0;

		private InputMenu vMenuL;
		private InputMenu vMenuR;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public virtual void Awake() {
			vMenuL = new InputMenu(true);
			vMenuR = new InputMenu(false);
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public override void UpdateInput() {
			UpdateMenu(vMenuL);
			UpdateMenu(vMenuR);
		}

		/*--------------------------------------------------------------------------------------------*/
		public override IInputMenu GetMenu(bool pIsLeft) {
			return (pIsLeft ? vMenuL : vMenuR);
		}

		/*--------------------------------------------------------------------------------------------*/
		public override void SetCameraTransform(UnityEngine.Transform pCameraTx) {
			//do nothing...
		}
		
		
		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		private void UpdateMenu(InputMenu pMenu) {
			UnityEngine.Transform tx = gameObject.transform;

			pMenu.IsAvailable = IsAvailable;
			pMenu.Position = tx.localPosition;
			pMenu.Rotation = tx.localRotation;
			pMenu.DisplayStrength = DisplayStrength;
			pMenu.NavigateBackStrength = NavigateBackStrength;
		}

	}

}
