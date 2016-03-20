using System;
using UnityEngine;
using System.Collections;
using Packages.EventSystem;
using UniRx;

namespace EndlessMine {

	public class DamageSkill : SkillBase {

		private bool _isActive = false;
		private float _amount = 5;

		public override void Initialize( EMCharacter character ) {

			base.Initialize( character );

			EventSystem.Events.SubscribeOfType<EMClickInput.Clicked>( OnObjectClick );
		}

		public override void TryUse() {

			_isActive = true;
		}

		private void OnObjectClick( EMClickInput.Clicked clicked ) {

			if ( !_isActive ) {

				return;
			}

			var character = clicked.ClickTarget as EMCharacter;
			if ( character != null && character.TeamId != Character.TeamId ) {

				character.Damage( _amount );

				Debug.Log( "Damage!" );
			}
		}

	}

}