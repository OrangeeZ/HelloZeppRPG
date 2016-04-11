using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EndlessMine.BattleSystem;
using EndlessMine;

public class EMCharacter : Character {

	public static List<EMCharacter> Instances = new List<EMCharacter>();

	public event Action<int, int> DestinationSet;

	public readonly EMCharacterPawn Pawn;
	private readonly EMCharacterInfo _info;

	public readonly BattleGrid BattleGrid;

	public int X { get; private set; }
	public int Y { get; private set; }

	private bool _didHavePreviousPosition = false;

	public SkillBase[] Skills;

	public EMCharacter( EMCharacterPawn pawn, CharacterStatus status, CharacterStateController stateController, int teamId, EMCharacterInfo info, BattleGrid battleGrid ) : base( pawn, status, stateController, teamId, info ) {

		_info = info;
		BattleGrid = battleGrid;

		Pawn = pawn;
		Pawn.Initialize();

		Instances.Add( this );

		Skills = new[] {new HealSkill(), new DamageSkill() as SkillBase, };

		foreach ( var each in Skills ) {

			each.Initialize( this );
		}
	}

	public void MoveBy( int deltaX, int deltaY ) {

		OnExitCell( X, Y );

		X += deltaX;
		Y += deltaY;

		UpdatePawnPosition();
	}

	public void SetPosition(int x, int y, bool updatePawnPosition) {

		OnExitCell( X, Y );

		X = x;
		Y = y;

		BattleGrid.SetCharacterAtCell( X, Y, this );

		if (updatePawnPosition)
		{
		UpdatePawnPosition();						
		}

	}

	public void SetDestination( int x, int y ) {

		if ( DestinationSet != null ) {

			DestinationSet( x, y );
		}
	}

	protected override void Dispose() {

		base.Dispose();

		Instances.Remove( this );

		BattleGrid.SetCharacterAtCell( X, Y, null );
	}

	public bool IntersectRay( Ray ray ) {

		return Pawn.IntersectRay( ray );
	}

	public void NotifySelectionChange( bool value ) {

		Pawn.SetSelection( value );
	}

	private void OnExitCell( int x, int y ) {

		if ( _didHavePreviousPosition ) {

			BattleGrid.SetCharacterAtCell( x, y, null );
		}

		_didHavePreviousPosition = true;
	}

	private void UpdatePawnPosition() {

		Pawn.transform.position = BattleGrid.GridToWorldPosition( X, Y );
	}

}