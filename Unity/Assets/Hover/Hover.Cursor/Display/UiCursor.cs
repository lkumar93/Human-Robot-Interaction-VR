﻿using Hover.Cursor.Custom;
using Hover.Cursor.State;
using UnityEngine;

namespace Hover.Cursor.Display {

	/*================================================================================================*/
	public class UiCursor : MonoBehaviour {

		private ICursorState vCursorState;
		private Transform vCameraTx;

		private GameObject vCursorRendererHold;
		private GameObject vCursorRendererObj;
		private IUiCursorRenderer vCursorRenderer;
		

		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		internal void Build(ICursorState pCursorState, ICursorSettings pSettings, Transform pCameraTx) {
			vCursorState = pCursorState;
			vCameraTx = pCameraTx;
			
			vCursorRendererHold = new GameObject("CursorRendererHold");
			vCursorRendererHold.transform.SetParent(gameObject.transform, false);

			vCursorRendererObj = new GameObject("CursorRenderer");
			vCursorRendererObj.transform.SetParent(vCursorRendererHold.transform, false);

			vCursorRenderer = (IUiCursorRenderer)vCursorRendererObj.AddComponent(pSettings.Renderer);
			vCursorRenderer.Build(vCursorState, pSettings);
		}

		/*--------------------------------------------------------------------------------------------*/
		public void Update() {
			if ( !vCursorState.IsInputAvailable || vCursorState.DisplayStrength <= 0 ) {
				vCursorRendererHold.SetActive(false);
				return;
			}

			vCursorRendererHold.SetActive(true);

			Transform holdTx = vCursorRendererHold.transform;
			holdTx.position = vCursorState.Position;
			holdTx.rotation = Quaternion.identity;
			holdTx.localScale = Vector3.one*vCursorState.Size;

			Vector3 camWorld = vCameraTx.TransformPoint(Vector3.zero);
			Vector3 camLocal = holdTx.InverseTransformPoint(camWorld);
			holdTx.rotation = Quaternion.FromToRotation(Vector3.down, camLocal);
		}

	}

}
