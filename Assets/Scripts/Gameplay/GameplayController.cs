using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EndlessMine.BattleSystem;

public class GameplayController : MonoBehaviour {

	public List<Character> CharacterInstances = new List<Character>();

	//[SerializeField]
	//private Character _characterPrefab;

	[SerializeField]
	private EMCharacterSpawner[] _spawners;

	[SerializeField]
	private BattleGrid _grid;

	[SerializeField]
	private EMClickInput _clickInput;

	//[SerializeField]
	//private CharacterSelectionListener _characterSelectionListener;

	private void Start() {

		_grid.Initialize();

		_clickInput.Initialize( _grid );

		foreach ( var each in _spawners ) {

			each.Spawn();
		}
		//_spawners.Spawn( _grid );

		for ( var y = 0; y < 3; y++ ) {

			//var characterInstance = Instantiate( _characterPrefab );

			//characterInstance.Initialize( _grid );
			//characterInstance.SetGridCell( 0, y );

			//CharacterInstances.Add( characterInstance );
		}

		//_characterSelectionListener.Initialize( this, _grid );
    }

}