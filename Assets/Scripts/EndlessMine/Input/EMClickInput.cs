using UnityEngine;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using EndlessMine.BattleSystem;
using Packages.EventSystem;

public class EMClickInput : MonoBehaviour {

	public struct Clicked : IEventBase {

		public object ClickTarget;

	}

	private BattleGrid _grid;

	private EMCharacter _currentCharacter;

	public void Initialize( BattleGrid grid ) {

		_grid = grid;
	}

	private void Update() {

		if ( !UpdateCharacterClick() ) {

			UpdateGridCellClick();
		}
	}

	private void UpdateGridCellClick() {

		if ( Input.GetMouseButtonDown( 0 ) ) {

			var screenRay = Camera.main.ScreenPointToRay( Input.mousePosition );
			var clickedGridCell = _grid.GetCells().FirstOrDefault( _ => _.IntersectsRay( screenRay ) );

			if ( clickedGridCell != null && _currentCharacter != null ) {

				_currentCharacter.SetDestination( clickedGridCell.X, clickedGridCell.Y );

				EventSystem.RaiseEvent( new Clicked {ClickTarget = clickedGridCell} );
			}
		}
	}

	private bool UpdateCharacterClick() {

		if ( Input.GetMouseButtonDown( 0 ) ) {

			var screenRay = Camera.main.ScreenPointToRay( Input.mousePosition );
			var clickedCharacter = EMCharacter.Instances.FirstOrDefault( _ => _.IntersectRay( screenRay ) );

			if ( clickedCharacter != null ) {

				if ( _currentCharacter != null ) {

					_currentCharacter.NotifySelectionChange( false );
				}

				clickedCharacter.NotifySelectionChange( true );
				_currentCharacter = clickedCharacter;

				EventSystem.RaiseEvent( new Clicked {ClickTarget = clickedCharacter} );

				return true;
			}
		}

		return false;
	}

}