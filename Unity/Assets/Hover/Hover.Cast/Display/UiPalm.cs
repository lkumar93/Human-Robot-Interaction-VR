using System;
using Hover.Cast.Custom;
using Hover.Cast.State;
using Hover.Common.Custom;
using Hover.Common.Items;
using Hover.Common.State;
using UnityEngine;

namespace Hover.Cast.Display {

	/*================================================================================================*/
	public class UiPalm : MonoBehaviour {

		private MenuState vMenuState;
		private IItemVisualSettingsProvider vVisualSettingsProv;
		private bool vRebuildOnUpdate;

		private GameObject vRendererHold;
		private GameObject vPrevRendererObj;
		private GameObject vRendererObj;
		private IUiPalmRenderer vRenderer;
		private int vPrevDepth;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		internal void Build(MenuState pMenu, IItemVisualSettingsProvider pVisualSettingsProv) {
			vMenuState = pMenu;
			vVisualSettingsProv = pVisualSettingsProv;

			vRendererHold = new GameObject("RendererHold");
			vRendererHold.transform.SetParent(gameObject.transform, false);
			vRendererHold.transform.localRotation = Quaternion.AngleAxis(170, Vector3.up);

			////

			BaseItemState itemState = pMenu.GetPalmItem();
			IItemVisualSettings visualSett = pVisualSettingsProv.GetSettings(itemState.Item);

			var itemObj = new GameObject("BackItem");
			itemObj.transform.SetParent(vRendererHold.transform, false);

			UiItem uiItem = itemObj.AddComponent<UiItem>();
			uiItem.Build(vMenuState, itemState, (float)Math.PI*2, visualSett);

			////

			vMenuState.OnLevelChange += HandleLevelChange;
			Rebuild();
		}

		/*--------------------------------------------------------------------------------------------*/
		internal void UpdateAfterSideChange() {
			vRebuildOnUpdate = true;
		}

		/*--------------------------------------------------------------------------------------------*/
		public void Update() {
			if ( vPrevRendererObj != null ) {
				vPrevRendererObj.SetActive(false);
				Destroy(vPrevRendererObj);
				vPrevRendererObj = null;
			}

			if ( vRebuildOnUpdate ) {
				Rebuild();
			}

			if ( vMenuState.DisplayDepthHint != vPrevDepth ) {
				vRenderer.SetDepthHint(vMenuState.DisplayDepthHint);
				vPrevDepth = vMenuState.DisplayDepthHint;
			}

			vRendererHold.SetActive(vMenuState.DisplayStrength > 0);
			vRebuildOnUpdate = false;
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		private void HandleLevelChange(int pDirection) {
			vRebuildOnUpdate = true;
		}

		/*--------------------------------------------------------------------------------------------*/
		private void Rebuild() {
			vPrevRendererObj = vRendererObj;

			const float halfAngle = UiLevel.AngleFull/2f;
			IBaseItem item = vMenuState.GetLevelParentItem();
			IItemAndPalmVisualSettings visualSett = 
				(IItemAndPalmVisualSettings)vVisualSettingsProv.GetSettings(item);

			vRendererHold.SetActive(true); //ensures that Awake() is called in the renderers

			vRendererObj = new GameObject("Renderer");
			vRendererObj.transform.SetParent(vRendererHold.transform, false);

			vRenderer = (IUiPalmRenderer)vRendererObj.AddComponent(visualSett.PalmRenderer);
			vRenderer.Build(vMenuState, visualSett, -halfAngle, halfAngle);
			vRenderer.SetDepthHint(vMenuState.DisplayDepthHint);
			vPrevDepth = vMenuState.DisplayDepthHint;
		}

	}

}
