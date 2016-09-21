using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Hover.Common.Display;
using UnityEngine;

namespace Hover.Cast.Display.Standard {

	/*================================================================================================*/
	public class UiItemSliderTrackRenderer {

		protected readonly UiHoverMeshSlice[] vTracks;
		protected readonly UiHoverMeshSlice[] vFills;
		protected readonly UiHoverMeshSlice[] vAllBgs;
		protected readonly List<DisplayUtil.TrackSegment> vSlices;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public UiItemSliderTrackRenderer(GameObject pParent) {
			vTracks = new UiHoverMeshSlice[4];
			vFills = new UiHoverMeshSlice[2];

			for ( int i = 0 ; i < vTracks.Length ; i++ ) {
				vTracks[i] = new UiHoverMeshSlice(pParent, true, "Track"+i);
			}

			for ( int i = 0 ; i < vFills.Length ; i++ ) {
				vFills[i] = new UiHoverMeshSlice(pParent, true, "Fill"+i);
			}

			vAllBgs = vTracks.Concat(vFills).ToArray();
			vSlices = new List<DisplayUtil.TrackSegment>();
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public virtual void SetDepthHint(int pDepthHint) {
			foreach ( UiHoverMeshSlice bg in vAllBgs ) {
				bg.SetDepthHint(pDepthHint);
			}
		}

		/*--------------------------------------------------------------------------------------------*/
		public virtual void SetColors(Color pTrackColor, Color pFillColor) {
			foreach ( UiHoverMeshSlice track in vTracks ) {
				track.UpdateBackground(pTrackColor);
			}

			foreach ( UiHoverMeshSlice fill in vFills ) {
				fill.UpdateBackground(pFillColor);
			}
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public virtual void UpdateSegments(ReadOnlyCollection<DisplayUtil.TrackSegment> pSegments,	
													ReadOnlyCollection<DisplayUtil.TrackSegment> pCuts,
													float pRadiusInner, float pRadiusOuter) {
			DisplayUtil.SplitTrackSegments(pSegments, pCuts, vSlices);
			
			foreach ( UiHoverMeshSlice bg in vAllBgs ) {
				bg.Show(false);
			}

			int trackI = 0;
			int fillI = 0;

			foreach ( DisplayUtil.TrackSegment slice in vSlices ) {
				UiHoverMeshSlice bg;

				if ( slice.IsFill ) {
					bg = vFills[fillI++];
				}
				else {
					bg = vTracks[trackI++];
				}

				float angle0 = slice.StartValue;
				float angle1 = slice.EndValue;

				if ( slice.IsZeroAtStart == true ) {
					angle0 -= UiHoverMeshSlice.AngleInset*2;
				}
				else if ( slice.IsZeroAtStart == false ) {
					angle1 += UiHoverMeshSlice.AngleInset*2;
				}

				bg.Show(true);
				bg.UpdateSize(pRadiusInner, pRadiusOuter, angle0, angle1);
			}
		}

	}

}
