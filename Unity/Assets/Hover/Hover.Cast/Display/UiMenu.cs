using Hover.Cast.State;
using Hover.Common.Custom;
using UnityEngine;

namespace Hover.Cast.Display {

	/*================================================================================================*/
	public class UiMenu : MonoBehaviour {

		public const float ScaleArcSize = 1.1f;

		private HovercastState vState;

		private UiPalm vUiPalm;
		private UiArc vUiArc;

		private Quaternion vLeftRot;
		private Quaternion vRightRot;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		internal void Build(HovercastState pState, IItemVisualSettingsProvider pItemVisualSettingsProv){
			vState = pState;
			vLeftRot = Quaternion.AngleAxis(180, Vector3.up);
			vRightRot = Quaternion.identity;

			var palmObj = new GameObject("Palm");
			palmObj.transform.SetParent(gameObject.transform, false);
			vUiPalm = palmObj.AddComponent<UiPalm>();
			vUiPalm.Build(vState.FullMenu, pItemVisualSettingsProv);

			var arcObj = new GameObject("Arc");
			arcObj.transform.SetParent(gameObject.transform, false);
			vUiArc = arcObj.AddComponent<UiArc>();
			vUiArc.Build(vState.FullMenu, pItemVisualSettingsProv);

			vState.OnSideChange += HandleSideChange;
		}

		/*--------------------------------------------------------------------------------------------*/
		public void Update() {
			MenuState menu = vState.FullMenu;
			bool isLeft = menu.IsOnLeftSide;
			Vector3 scale = Vector3.one*(menu.Size*ScaleArcSize);

			if ( !isLeft ) {
				scale.z *= -1;
			}

			gameObject.transform.position = menu.Center;
			gameObject.transform.rotation = menu.Rotation;
			gameObject.transform.localScale = scale;

			vUiArc.gameObject.transform.localRotation = (isLeft ? vLeftRot : vRightRot);
			vUiPalm.gameObject.transform.localRotation = (isLeft ? vLeftRot : vRightRot);
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		private void HandleSideChange() {
			vUiPalm.UpdateAfterSideChange();
			vUiArc.UpdateAfterSideChange();
		}

	}

}
