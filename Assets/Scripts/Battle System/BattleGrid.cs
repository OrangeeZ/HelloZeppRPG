using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EndlessMine.BattleSystem {

	public class BattleGrid : MonoBehaviour {

		[SerializeField]
		private int _width;

		[SerializeField]
		private int _height;

		[SerializeField]
		private float _padding;

		[SerializeField]
		private BattleGridCell _cellPrefab;

		private List<BattleGridCell> _gridCells = new List<BattleGridCell>();

		public void Initialize() {

			for ( var x = 0; x < _width; x++ ) {

				for ( var y = 0; y < _height; y++ ) {

					var cellInstance = Instantiate( _cellPrefab );
					cellInstance.transform.position = GridToWorldPosition( x, y );

					cellInstance.X = x;
					cellInstance.Y = y;

					_gridCells.Add( cellInstance );
				}
			}
		}

		public IList<BattleGridCell> GetCells() {

			return _gridCells;
		}

		public IEnumerable<BattleGridCell> GetNeighbourCells( int x, int y ) {

			for ( var i = 0; i < 4; ++i ) {

				var cell = GetCell( x + Mathf.Cos( x ).RoundToInt(), y + Mathf.Sin( y ).RoundToInt() );
				if ( cell != null ) {

					yield return cell;
				}
			}
		}

		public Vector3 GridToWorldPosition( int x, int y ) {

			var offsetX = x > 0 ? _padding : 0;
			var offsetY = y > 0 ? _padding : 0;

			return transform.position + Vector3.right * x * ( _cellPrefab.GetCellSize() + offsetX ) + Vector3.back * y * ( _cellPrefab.GetCellSize() + offsetY );
		}

		public int GetWidth() {

			return _width;
		}

		public int GetHeight() {

			return _height;
		}

		public void SetCharacterAtCell( int x, int y, EMCharacter character ) {

			var cell = GetCell( x, y );

			if ( cell != null ) {

				cell.Character = character;
			}
		}

		public bool IsFree( int x, int y ) {

			return GetCharacterAtCell( x, y ) == null;
		}

		public global::Character GetCharacterAtCell( int x, int y ) {

			var cell = GetCell( x, y );

			return cell != null ? cell.Character : null;
		}

		public global::Character GetCharacterAtDirectionX( int x, int y, int direction ) {

			for ( var i = x; i >= 0 && i < _width; i += direction ) {

				var character = GetCharacterAtCell( i, y );

				if ( character != null ) {

					return character;
				}
			}

			return null;
		}

		private BattleGridCell GetCell( int x, int y ) {

			var index = PositonToIndex( x, y );
			if ( index < 0 || index > _gridCells.Count ) {

				return null;
			}

			return _gridCells[index];
		}

		private int PositonToIndex( int x, int y ) {

			return y * _width + x;
		}

		private void OnDrawGizmos() {

			Gizmos.color = Color.yellow;

			for ( var x = 0; x < _width; x++ ) {

				for ( var y = 0; y < _height; y++ ) {

					var cellPosition = GridToWorldPosition( x, y );
					Gizmos.DrawWireCube( cellPosition, _cellPrefab.GetCellSize() * Vector3.one );
				}
			}
		}

	}

}