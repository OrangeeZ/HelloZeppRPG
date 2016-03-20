using UnityEngine;
using System.Collections;

namespace EndlessMine.Character.States {

	[CreateAssetMenu( menuName = "EM/States/Attack" )]
	public class EMAttackState : CharacterStateInfo {

		[SerializeField]
		private int _direction;

		private class State : CharacterState<EMAttackState> {

			private AutoTimer _attackTimer = new AutoTimer( 1 );

			public State( CharacterStateInfo info ) : base( info ) {
			}

			public override bool CanBeSet() {

				return _attackTimer.ValueNormalized >= 1f && GetActiveWeapon() != null;
			}

			public override IEnumerable GetEvaluationBlock() {

				var emCharacter = character as EMCharacter;
				var weapon = GetActiveWeapon();
				var attackTarget = emCharacter.BattleGrid.GetCharacterAtDirectionX( emCharacter.X + typedInfo._direction, emCharacter.Y, typedInfo._direction );

				if ( !weapon.CanAttack( attackTarget ) ) {

					yield break;
				}

				if ( attackTarget != null && attackTarget.TeamId != character.TeamId ) {

					attackTarget.Damage( 1 );
					_attackTimer.Reset();

					yield return null;
				}
			}

			private Weapon GetActiveWeapon() {

				return character.Inventory.GetArmSlotItem<Weapon>( ArmSlotType.Primary );
			}

		}

		public override CharacterState GetState() {

			return new State( this );
		}

	}

}