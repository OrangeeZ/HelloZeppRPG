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

			var emCharacter = character as EMCharacter;

			//emCharacter.BattleGrid.SetCharacterAtCell( emCharacter.X, emCharacter.Y, null );

			emCharacter.SetPosition( _x, _y );

			//emCharacter.BattleGrid.SetCharacterAtCell( emCharacter.X, emCharacter.Y, emCharacter );

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

	}

	public override CharacterState GetState() {

		return new State( this );
	}

}