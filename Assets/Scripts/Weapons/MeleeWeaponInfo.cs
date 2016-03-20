using UnityEngine;
using System.Collections;
using System.Linq;

[CreateAssetMenu( menuName = "Create/Weapons/Melee" )]
public class MeleeWeaponInfo : WeaponInfo {

	[SerializeField]
	private float _attackAngle = 15f;

	[SerializeField]
	private float _attackRange = 2f;

	private class MeleeWeapon : Weapon<MeleeWeaponInfo> {

		//public SimpleSphereCollider sphereCollider;

		private float nextAttackTime;

		public MeleeWeapon( MeleeWeaponInfo info )
			: base( info ) {

		}

		public override bool CanAttack( Character target ) {

			return ( target.Pawn.position - Character.Pawn.position ).sqrMagnitude <= typedInfo.AttackRange.Pow( 2 );
		}

	}

	public override Item GetItem() {

		return new MeleeWeapon( this );
	}

}