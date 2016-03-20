using UnityEngine;
using System.Collections;

public class EMCharacterPawn : CharacterPawnBase {

	[SerializeField]
	private Renderer _renderer;

	[SerializeField]
	private GameObject _selectionGameObject;

	public void Initialize() {

		SetSelection( false );
	}

	public bool IntersectRay( Ray ray ) {

		return _renderer.bounds.IntersectRay( ray );
	}

	public void SetSelection( bool value ) {

		_selectionGameObject.SetActive( value );
	}

	private void OnGUI() {

		var emCharacter = character as EMCharacter;

		var screenPosition = Camera.main.WorldToScreenPoint( _selectionGameObject.transform.position );
		screenPosition.y = UnityEngine.Screen.height - screenPosition.y - 10;
		GUI.color = Color.black;
		GUI.Label( new Rect( screenPosition.x, screenPosition.y, 30, 30 ), emCharacter.Health.Value.ToString() );
		GUI.color = Color.white;

		if ( !_selectionGameObject.activeSelf ) {

			return;
		}

		foreach ( var each in emCharacter.Skills ) {

			if ( GUILayout.Button( each.Name ) ) {

				each.TryUse();
			}
		}
	}

}