using UnityEngine;

namespace EndlessMine.Items {

	[CreateAssetMenu( menuName = "EM/Items/Weapon" )]
	public class EMWeaponInfo : WeaponInfo {

		private class EMWeapon : Weapon<EMWeaponInfo> {

			public EMWeapon( ItemInfo info ) : base( info ) {
			}

			public override bool CanAttack( global::Character target ) {

				if ( target == null ) {

					return false;
				}

				var thisCharacter = Character as EMCharacter;
				var otherCharacter = target as EMCharacter;

				return Mathf.Abs( thisCharacter.X - otherCharacter.X ) <= typedInfo.AttackRange;
			}

			public override void Attack( global::Character target ) {

				target.Damage( typedInfo.BaseDamage );
			}

		}

		public override Item GetItem() {

			return new EMWeapon( this );
		}

	}

}