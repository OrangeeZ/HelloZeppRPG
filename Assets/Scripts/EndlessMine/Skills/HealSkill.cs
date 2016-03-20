using UnityEngine;
using System.Collections;
using System.Linq;

namespace EndlessMine {

	public class HealSkill : SkillBase {

		private int _amount = 5;

		public override void TryUse() {

			var neighbourTeammates = Character.BattleGrid
				.GetNeighbourCells( Character.X, Character.Y )
				.Select( _ => _.Character )
				.Where( _ => _ != null && _.TeamId == Character.TeamId );

			foreach ( var each in neighbourTeammates ) {

				each.Heal( _amount );
			}

		}

	}

}