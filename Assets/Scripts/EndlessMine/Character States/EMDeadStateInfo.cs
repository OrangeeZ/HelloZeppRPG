using UnityEngine;
using System.Collections;

namespace EndlessMine.Character.States {

	[CreateAssetMenu( menuName = "EM/States/Dead" )]
	public class EMDeadStateInfo : CharacterStateInfo {

		private float _removeDelay = 2f;

		private class State : CharacterState<EMDeadStateInfo> {

			public State( CharacterStateInfo info ) : base( info ) {
			}

			public override bool CanBeSet() {

				return character.Health.Value <= 0;
			}

			public override IEnumerable GetEvaluationBlock() {

				var timer = new AutoTimer( typedInfo._removeDelay );
				while ( timer.ValueNormalized < 1 ) {

					yield return null;
				}

				character.Destroy();
			}

		}

		public override CharacterState GetState() {
			
			return new State( this );
		}

	}

}