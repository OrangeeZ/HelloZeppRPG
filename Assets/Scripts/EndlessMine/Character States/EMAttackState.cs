using UnityEngine;
using System.Collections;
using EndlessMine.Items;

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

					weapon.Attack(attackTarget);
					_attackTimer.Reset();

					yield return null;
				}
			}

			private EMWeaponInfo.EMWeapon GetActiveWeapon() {

				return character.Inventory.GetArmSlotItem<EMWeaponInfo.EMWeapon>( ArmSlotType.Primary );
			}

		}

		public override CharacterState GetState() {

			return new State( this );
		}

	}

}