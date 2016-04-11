using UnityEngine;

namespace EndlessMine.Items {

	[CreateAssetMenu( menuName = "EM/Items/Weapon" )]
	public class EMWeaponInfo : WeaponInfo
	{

		[SerializeField]
		private Projectile _attackEffectPrefab;

		public class EMWeapon : Weapon<EMWeaponInfo> {

			public EMWeapon( ItemInfo info ) : base( info ) {
			}

			public bool CanAttack( EMCharacter target ) {

				if ( target == null ) {

					return false;
				}

				var thisCharacter = Character as EMCharacter;
				var otherCharacter = target;

				return Mathf.Abs( thisCharacter.X - otherCharacter.X ) <= typedInfo.AttackRange;
			}

			public void Attack( EMCharacter target ) {

				var thisCharacter = Character as EMCharacter;
				var otherCharacter = target;
				var direction = new Vector3( otherCharacter.X - thisCharacter.X, otherCharacter.Y - thisCharacter.Y ) ;

				if (typedInfo._attackEffectPrefab != null) {

					var attackEffectInstance = Instantiate(typedInfo._attackEffectPrefab);
					attackEffectInstance.Launch(thisCharacter, direction.ToXZ(), 5f, 0, false, 0f);
				}

				target.Damage( typedInfo.BaseDamage );
			}

		}

		public override Item GetItem() {

			return new EMWeapon( this );
		}

	}

}