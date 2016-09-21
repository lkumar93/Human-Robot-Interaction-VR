using Hover.Cast.Items;
using Hover.Common.Items.Types;
using UnityEditor;
using UnityEngine;

namespace Hover.Cast.Edit.Items {

	/*================================================================================================*/
	[CustomEditor(typeof(HovercastItem))]
	public class HovercastItemEditor : Editor {

		private HovercastItem vTarget;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public void OnEnable() {
			vTarget = (HovercastItem)target;
		}
		
		/*--------------------------------------------------------------------------------------------*/
		public override void OnInspectorGUI() {
			Undo.RecordObject(vTarget, vTarget.GetType().Name);
			bool enabled = !Application.isPlaying;

			if ( !enabled ) {
				EditorGUILayout.HelpBox("The following values are for initialization only. To make "+
					"changes at runtime, modify the Item objects using scripts.", MessageType.Info);
			}

			GUI.enabled = enabled;

			vTarget.Id = EditorGUILayout.TextField("ID (optional)", vTarget.Id);
			vTarget.Label = EditorGUILayout.TextField("Label", vTarget.Label);
			vTarget.RelativeSize = EditorGUILayout.FloatField("Relative Size", vTarget.RelativeSize);
			vTarget.IsVisible = EditorGUILayout.Toggle("Visible", vTarget.IsVisible);
			vTarget.IsEnabled = EditorGUILayout.Toggle("Enabled", vTarget.IsEnabled);

			vTarget.Type = (HovercastItem.HovercastItemType)EditorGUILayout.EnumPopup(
				"Item Type", vTarget.Type);

			if ( vTarget.Type != HovercastItem.HovercastItemType.Parent ) {
				vTarget.NavigateBackUponSelect = EditorGUILayout.Toggle(
					"Navigate Back Upon Select", vTarget.NavigateBackUponSelect);
			}

			switch ( vTarget.Type ) {
				case HovercastItem.HovercastItemType.Checkbox:
					vTarget.CheckboxValue = EditorGUILayout.Toggle("Value", vTarget.CheckboxValue);
					break;

				case HovercastItem.HovercastItemType.Radio:
					vTarget.RadioValue = EditorGUILayout.Toggle("Value", vTarget.RadioValue);
					break;

				case HovercastItem.HovercastItemType.Slider:
					vTarget.SliderTicks = EditorGUILayout.IntField("Ticks", vTarget.SliderTicks);
					vTarget.SliderSnaps = EditorGUILayout.IntField("Snaps", vTarget.SliderSnaps);
					vTarget.SliderRangeMin = EditorGUILayout.FloatField("Min", vTarget.SliderRangeMin);
					vTarget.SliderRangeMax = EditorGUILayout.FloatField("Max", vTarget.SliderRangeMax);
					vTarget.SliderValue = EditorGUILayout.Slider("Value", vTarget.SliderValue,	
						vTarget.SliderRangeMin, vTarget.SliderRangeMax);
					vTarget.SliderAllowJump = EditorGUILayout.Toggle("Allow Jump-To-Value", 
						vTarget.SliderAllowJump);
					vTarget.SliderFillStartingPoint = (SliderItem.FillType)EditorGUILayout.EnumPopup(
						"Starting Point For Fill", vTarget.SliderFillStartingPoint);
					break;
			}

			if ( GUI.changed ) {
				EditorUtility.SetDirty(vTarget);
			}

			GUI.enabled = true;
		}

	}

}
