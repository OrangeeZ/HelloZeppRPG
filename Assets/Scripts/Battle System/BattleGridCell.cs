using UnityEngine;
using System.Collections;

namespace EndlessMine.BattleSystem {

	public class BattleGridCell : MonoBehaviour {

		[SerializeField]
		private Renderer _renderer;

		public EMCharacter Character { get; set; }

		public int X;
		public int Y;

		public float GetCellSize() {

			return _renderer.bounds.size.x;
		}

		public bool IntersectsRay( Ray ray ) {

			return _renderer.bounds.IntersectRay( ray );
		}
	}

}