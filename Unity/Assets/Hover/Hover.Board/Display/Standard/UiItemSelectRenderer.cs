﻿using System;
using Hover.Board.Custom.Standard;
using Hover.Board.State;
using Hover.Common.Custom;
using Hover.Common.Display;
using Hover.Common.Items;
using Hover.Common.State;
using UnityEngine;

namespace Hover.Board.Display.Standard {

	/*================================================================================================*/
	public class UiItemSelectRenderer : MonoBehaviour, IUiItemRenderer {

		public const float LabelCanvasScale = UiItem.Size*0.012f;

		protected IHoverboardPanelState vPanelState;
		protected IHoverboardLayoutState vLayoutState;
		protected IBaseItemState vItemState;
		protected ItemVisualSettingsStandard vSettings;

		protected float vMainAlpha;
		protected float vWidth;
		protected float vHeight;

		protected UiHoverMeshRect vHoverRect;
		protected UiLabel vLabel;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public virtual void Build(IHoverboardPanelState pPanelState, 
										IHoverboardLayoutState pLayoutState, IBaseItemState pItemState,
										IItemVisualSettings pSettings) {
			vPanelState = pPanelState;
			vLayoutState = pLayoutState;
			vItemState = pItemState;
			vSettings = (ItemVisualSettingsStandard)pSettings;

			gameObject.transform.SetParent(gameObject.transform, false);

			vHoverRect = new UiHoverMeshRect(gameObject);

			var labelObj = new GameObject("Label");
			labelObj.transform.SetParent(gameObject.transform, false);
			vLabel = labelObj.AddComponent<UiLabel>();

			float width = UiItem.Size*vItemState.Item.Width;
			float height = UiItem.Size*vItemState.Item.Height;
			SetCustomSize(width, height);
		}
		
		/*--------------------------------------------------------------------------------------------*/
		public virtual void SetDepthHint(int pDepthHint) {
			vHoverRect.SetDepthHint(pDepthHint);
			vLabel.SetDepthHint(pDepthHint);
		}

		/*--------------------------------------------------------------------------------------------*/
		public virtual void SetCustomSize(float pWidth, float pHeight, bool pCentered=true) {
			vWidth = pWidth;
			vHeight = pHeight;

			gameObject.transform.localPosition = (pCentered ? 
				new Vector3(vWidth/2, 0, vHeight/2f) : Vector3.zero);
			vHoverRect.UpdateSize(vWidth, vHeight);
			vLabel.SetSize(vWidth, vHeight, vSettings.TextSize*0.25f, LabelCanvasScale);
		}

		/*--------------------------------------------------------------------------------------------*/
		public virtual void OnEnable() {
			if ( vLabel != null ) {
				vLabel.Alpha = 0;
			}
		}

		/*--------------------------------------------------------------------------------------------*/
		public virtual void Update() {
			vMainAlpha = vPanelState.DisplayStrength*vLayoutState.DisplayStrength;

			if ( !vItemState.Item.IsEnabled || !vItemState.Item.IsAncestryEnabled ) {
				vMainAlpha *= 0.333f;
			}

			ISelectableItem selItem = (vItemState.Item as ISelectableItem);
			float high = vItemState.MaxHighlightProgress;
			bool showEdge = DisplayUtil.IsEdgeVisible(vItemState);
			float edge = (showEdge ? high : 0);
			float select = 1-(float)Math.Pow(1-vItemState.SelectionProgress, 1.5f);
			float selectAlpha = select;

			if ( selItem != null && selItem.IsStickySelected ) {
				selectAlpha = 1;
			}

			Color colBg = vSettings.BackgroundColor;
			Color colEdge = vSettings.EdgeColor;
			Color colHigh = vSettings.HighlightColor;
			Color colSel = vSettings.SelectionColor;

			colBg.a *= vMainAlpha;
			colEdge.a *= edge*vMainAlpha;
			colHigh.a *= high*vMainAlpha;
			colSel.a *= selectAlpha*vMainAlpha;

			vHoverRect.UpdateBackground(colBg);
			vHoverRect.UpdateEdge(colEdge);
			vHoverRect.UpdateHighlight(colHigh, high);
			vHoverRect.UpdateSelect(colSel, select);

			vLabel.Alpha = vMainAlpha;
			vLabel.FontName = vSettings.TextFont;
			vLabel.FontSize = vSettings.TextSize;
			vLabel.Color = vSettings.TextColor;
			vLabel.Label = vItemState.Item.Label;
		}

		/*--------------------------------------------------------------------------------------------*/
		public virtual void UpdateHoverPoints(IBaseItemPointsState pPointsState,
																			Vector3 pCursorWorldPos) {
			vHoverRect.UpdateHoverPoints(pPointsState);
		}

	}

}
