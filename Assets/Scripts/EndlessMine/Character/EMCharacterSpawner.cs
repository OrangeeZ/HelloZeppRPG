using UnityEngine;
using System.Collections;
using EndlessMine.BattleSystem;

public class EMCharacterSpawner : MonoBehaviour {

	[SerializeField]
	private EMCharacterInfo _characterInfo;

	[SerializeField]
	private int _startOffsetX;

	[SerializeField]
	private int _startOffsetY;

	[SerializeField]
	private bool _startFromEnd;

	[SerializeField]
	private BattleGrid _battleGrid;

	[SerializeField]
	private WeaponInfo _startingWeaponInfo;

	private EMCharacter _spawnedCharacter;

	public EMCharacter Spawn() {

		var result = _characterInfo.GetCharacter( _battleGrid );

		if ( _startFromEnd ) {

			result.SetPosition( _battleGrid.GetWidth() - 1, 0, updatePawnPosition: true);
		}

		result.MoveBy( _startOffsetX, _startOffsetY );

		_spawnedCharacter = result;
		var weapon = _startingWeaponInfo.GetItem();
		weapon.SetCharacter( _spawnedCharacter );
		weapon.Apply();
		//_spawnedCharacter.Inventory.SetArmSlotItem( ArmSlotType.Primary, _startingWeaponInfo.GetItem() );

		return result;
	}

	public bool IsSpawnedCharacterAlive() {

		return _spawnedCharacter != null && _spawnedCharacter.Health.Value > 0;
	}

}