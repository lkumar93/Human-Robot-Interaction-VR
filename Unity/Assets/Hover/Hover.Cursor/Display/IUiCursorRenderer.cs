﻿using Hover.Cursor.Custom;
using Hover.Cursor.State;

namespace Hover.Cursor.Display {

	/*================================================================================================*/
	public interface IUiCursorRenderer {

		
		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		void Build(ICursorState pCursorState, ICursorSettings pSettings);

	}

}
