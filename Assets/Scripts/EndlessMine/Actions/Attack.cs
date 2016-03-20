using System;
using UnityEngine;
using System.Collections;
using Flow;

namespace EM.Actions {

	public class Attack {

		private readonly EMCharacter _owner;
		private readonly int _moveDirection;

		public Attack( EMCharacter owner, int moveDirection ) {

			_owner = owner;
			_moveDirection = moveDirection;
		}

		public IEnumerator GetCoroutine( IGenerator generator ) {

			var attackTarget = _owner.BattleGrid.GetCharacterAtCell( _owner.X + _moveDirection, _owner.Y );

			if ( attackTarget == null ) {
				
				yield break;
			}

			attackTarget.Damage( 1 );

			yield return generator.ResumeAfter( new TimeSpan( 0, 0, 0, 1 ) );
		}

	}

}