using UnityEngine;
using System.Collections;
using Flow;

public class EMBasicEnemyBehaviour : MonoBehaviour {

	public EMCharacterPawn CharacterPawn;

	// Use this for initialization
	private void Start() {

		FlowExecutor.UpdateKernel.Factory.NewCoroutine( UpdateLoop );
	}

	private IEnumerator UpdateLoop( IGenerator generator ) {

		var attackAction = new EM.Actions.Attack( CharacterPawn.character as EMCharacter, -1 );
		var moveAction = new EM.Actions.Move( CharacterPawn.character as EMCharacter, -1 );

		var factory = FlowExecutor.UpdateKernel.Factory;

		while ( true ) {

			yield return generator.ResumeAfter( factory.NewCoroutine( attackAction.GetCoroutine ) );
			yield return generator.ResumeAfter( factory.NewCoroutine( moveAction.GetCoroutine ) );
		}
	}

}