﻿using System.Collections.Generic;
using Hover.Common.Custom;
using Hover.Common.Input;

namespace Hover.Board.Custom {

	/*================================================================================================*/
	public class InteractionSettings : BaseInteractionSettings {

		public IList<CursorType> Cursors { get; private set; }


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public InteractionSettings() {
			Cursors = new List<CursorType>();
		}

	}

}
