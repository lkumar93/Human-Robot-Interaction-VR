﻿namespace Hover.Common.Items {

	/*================================================================================================*/
	public interface IBaseItem {

		int AutoId { get; }
		string Id { get; set; }
		string Label { get; set; }
		float Width { get; }
		float Height { get; }
		object DisplayContainer { get; }

		bool IsEnabled { get; set; }
		bool IsVisible { get; set; }
		bool IsAncestryEnabled { get; set; }
		bool IsAncestryVisible { get; set; }

	}

}
