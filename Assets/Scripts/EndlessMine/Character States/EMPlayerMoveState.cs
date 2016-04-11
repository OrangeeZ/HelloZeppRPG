using System;
using UnityEngine;
using System.Collections;

[CreateAssetMenu( menuName = "EM/States/Player Move State" )]
public class EMPlayerMoveState : CharacterStateInfo {

	private class State : CharacterState<EMPlayerMoveState> {

		private bool _hasDestination;
		private int _x;
		private int _y;

		public State( CharacterStateInfo info ) : base( info ) {
		}

		public override void Initialize( CharacterStateController stateController ) {

			base.Initialize( stateController );

			var emCharacter = character as EMCharacter;
			emCharacter.DestinationSet += OnDestinationSet;
		}

		public override IEnumerable GetEvaluationBlock() {

			yield return null;

			var animationBlock = MoveAnimation( 0.5f, _x, _y ).GetEnumerator();
			while ( animationBlock.MoveNext() ) {

				yield return null;
			}

			yield return null;

			_hasDestination = false;
		}

		public override bool CanBeSet() {

			var emCharacter = character as EMCharacter;

			return _hasDestination && emCharacter.BattleGrid.IsFree( _x, _y );
		}

		private void OnDestinationSet( int x, int y ) {

			_x = x;
			_y = y;

			_hasDestination = true;

			stateController.TrySetState( this );
		}

		private IEnumerable MoveAnimation(float duration, int targetX, int targetY) {

			var emCharacter = character as EMCharacter;

			emCharacter.SetPosition( targetX, targetY, updatePawnPosition: false);

			var fromPosition = character.Pawn.position;
			var toPosition = emCharacter.BattleGrid.GridToWorldPosition(targetX, targetY);

			var timer = new AutoTimer(duration);
			while (timer.ValueNormalized < 1) {

				character.Pawn.position = Vector3.Slerp(fromPosition, toPosition, timer.ValueNormalized);

				yield return null;
			}
		}

	}

	public override CharacterState GetState() {

		return new State( this );
	}

}