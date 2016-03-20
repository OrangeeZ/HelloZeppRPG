using UnityEngine;
using System.Collections;
using EndlessMine.BattleSystem;

[CreateAssetMenu( menuName = "Create/EM/Character Info" )]
public class EMCharacterInfo : CharacterInfo {

	public EMCharacterPawn PawnPrefab;

	public virtual EMCharacter GetCharacter( BattleGrid battleGrid ) {

		var pawnInstance = Instantiate( PawnPrefab );

		return new EMCharacter( pawnInstance, statusInfo.GetInstance(), stateControllerInfo.GetStateController(), teamId, this, battleGrid );
	}

}