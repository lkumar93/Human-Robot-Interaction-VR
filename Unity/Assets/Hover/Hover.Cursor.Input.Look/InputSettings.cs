using UnityEngine;

namespace Hover.Cursor.Input.Look {

	/*================================================================================================*/
	public class InputSettings {

		public Transform InputTransform { get; set; }
		public Transform CameraTransform { get; set; }
		public float CursorSize { get; set; }
		public bool UseMouseForTesting { get; set; }
		public float MousePositionMultiplier { get; set; }
		
	}

}
