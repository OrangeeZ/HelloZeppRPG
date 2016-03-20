using UnityEngine;
using System.Collections;

[CreateAssetMenu( menuName = "EM/States/Enemy Move State" )]
public class EMEnemyMoveState : CharacterStateInfo {

	[SerializeField]
	private float _moveInterval = 1f;

	private class State : CharacterState<EMEnemyMoveState> {

		private float _lastMoveTime;

		public State( CharacterStateInfo info ) : base( info ) {
		}

		public override IEnumerable GetEvaluationBlock() {

			if ( Time.time - _lastMoveTime < typedInfo._moveInterval ) {

				yield break;
			}

			var emCharacter = character as EMCharacter;
			emCharacter.MoveBy( -1, 0 );
			_lastMoveTime = Time.time;
		}

		public override bool CanBeSet() {

			var emCharacter = character as EMCharacter;

			return emCharacter.BattleGrid.IsFree( emCharacter.X - 1, emCharacter.Y );
		}
	}

	public override CharacterState GetState() {

		return new State( this );
	}

}