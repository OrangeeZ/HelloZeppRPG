using UnityEngine;
using System.Collections;
using UniRx;

namespace EndlessMine.Character.States {

	[CreateAssetMenu( menuName = "EM/States/Dead" )]
	public class EMDeadStateInfo : CharacterStateInfo {

		private float _removeDelay = 2f;

		private class State : CharacterState<EMDeadStateInfo> {

			public State( CharacterStateInfo info ) : base( info ) {
			}

			public override void Initialize(CharacterStateController stateController)
			{
				base.Initialize(stateController);

				character.Health.Subscribe( OnHealthChange );
			}

			public override bool CanBeSet() {

				return character.Health.Value <= 0;
			}

			public override IEnumerable GetEvaluationBlock() {

				(character as EMCharacter).RemoveFromGrid();

				var timer = new AutoTimer( typedInfo._removeDelay );
				while ( timer.ValueNormalized < 1 ) {

					yield return null;
				}

				character.Destroy();
			}

			private void OnHealthChange(float newValue)
			{
				if (newValue <= 0)
				{
					stateController.TrySetState(this);
				}
			}

		}

		public override CharacterState GetState() {
			
			return new State( this );
		}

	}

}