﻿using System;
using Hover.Common.Items;
using UnityEngine;

namespace Hover.Board.Items {

	/*================================================================================================*/
	public abstract class HoverboardItemListener<T> : MonoBehaviour where T : IBaseItem {

		public HoverboardItem Component { get; private set; }
		public T Item { get; private set; }


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public void Awake() {
			Component = gameObject.GetComponent<HoverboardItem>();

			if ( Component == null ) {
				throw new Exception("There must be a "+typeof(HoverboardItem).Name+" component "+
					"attached to this GameObject.");
			}

			Item = (T)Component.GetItem();
		}

		/*--------------------------------------------------------------------------------------------*/
		public void Start() {
			Setup();
			BroadcastInitialValue();
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		protected abstract void Setup();

		/*--------------------------------------------------------------------------------------------*/
		protected abstract void BroadcastInitialValue();

	}

}
