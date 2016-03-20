using System;
using UnityEngine;
using System.Collections;
using Flow;

namespace EM.Actions {

	public class Move {

		private readonly EMCharacter _owner;
		private readonly int _direction;

		public Move( EMCharacter owner, int direction ) {

			_owner = owner;
			_direction = direction;

		}

		public IEnumerator GetCoroutine( IGenerator generator ) {

			var characterAtNextCell = _owner.BattleGrid.GetCharacterAtCell( _owner.X + _direction, _owner.Y );
			if ( characterAtNextCell != null ) {
				
				yield break;
			}

			_owner.MoveBy( _direction, 0 );

			//_owner.BattleGrid.SetCharacterAtCell( _owner.X, _owner.Y, null );

			//_owner.SetPosition( _owner.X + _direction, _owner.Y );

			//_owner.BattleGrid.SetCharacterAtCell( _owner.X, _owner.Y, _owner );

			yield return generator.ResumeAfter( new TimeSpan( 0, 0, 0, 1 ) );
		}

	}

}