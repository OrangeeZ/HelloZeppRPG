using UnityEngine;
using System.Collections;

namespace EndlessMine {

	public class SkillBase {

		public string Name {
			get { return this.GetType().Name; }
		}

		protected EMCharacter Character;

		public virtual void Initialize( EMCharacter character ) {

			Character = character;
		}

		public virtual void TryUse() {
		}

	}

}