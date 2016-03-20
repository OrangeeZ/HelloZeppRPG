using UnityEngine;
using System.Collections;
using Packages.EventSystem;

public class ItemView : AObject {

	public class PickedUp : IEventBase {

		public ItemView ItemView;

	}

	public Item item;

	public float fadeInDuration = 1f;

	public AnimationCurve scaleCurve;
	public AnimationCurve positionCurve;

	private IEnumerator Start() {

		var timer = new AutoTimer( fadeInDuration );

		while ( timer.ValueNormalized < 1f ) {

			transform.localScale = Vector3.one * scaleCurve.Evaluate( timer.ValueNormalized );

			yield return null;
		}
	}

	public void NotifyPickUp( Character character ) {

		EventSystem.RaiseEvent( new PickedUp {ItemView = this} );

		Destroy( gameObject );
	}

}