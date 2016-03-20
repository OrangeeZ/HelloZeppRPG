using System;
using UniRx;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Packages.EventSystem;

public class Character {

	public struct Died : IEventBase {

		public Character Character;

	}

	public struct RecievedDamage : IEventBase {

		public Character Character;
		public float Damage;

	}

	public struct Speech : IEventBase {

		public Character Character;
		public string MessageId;

	}

	//public static List<Character> Instances = new List<Character>();

	public readonly FloatReactiveProperty Health;
	
	public readonly IInventory Inventory;

	public readonly CharacterPawnBase Pawn;

	public readonly CharacterStateController StateController;

	public readonly int TeamId;
	public readonly CharacterInfo Info;

	public readonly CharacterStatus Status;

	public ItemInfo[] ItemsToDrop;
	public float DropProbability = 0.15f;
	public float SpeakProbability = 0.15f;

	private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();

	public Character( CharacterPawnBase pawn, CharacterStatus status, CharacterStateController stateController, int teamId, CharacterInfo info ) {

		this.Status = status;
		this.Health = new FloatReactiveProperty( this.Status.MaxHealth.Value );
		this.Pawn = pawn;
		this.StateController = stateController;
		this.TeamId = teamId;
		this.Info = info;
		this.Inventory = new BasicInventory( this );

		pawn.SetCharacter( this );

		this.StateController.Initialize( this );

		Observable.EveryUpdate().Subscribe( OnUpdate ).AddTo( _compositeDisposable );
		Health.Subscribe( OnHealthChange ); //.AddTo( _compositeDisposable );

		//Instances.Add( this );

		Status.ModifierCalculator.Changed += OnModifiersChange;
	}

	private void OnHealthChange( float health ) {

		if ( health <= 0 ) {

			EventSystem.RaiseEvent( new Died {Character = this} );

			if ( 1f.Random() <= SpeakProbability ) {

				EventSystem.RaiseEvent( new Speech {Character = this} );
			}

			//Instances.Remove( this );

			//_compositeDisposable.Dispose();

			Status.ModifierCalculator.Changed -= OnModifiersChange;
		}
	}

	public void Heal( float amount ) {

		if ( Health.Value == Status.MaxHealth.Value ) {

			return;
		}

		var healAmount = Mathf.Min( Status.MaxHealth.Value - Health.Value, amount );

		if ( healAmount > 0 ) {

			Health.Value += healAmount;
		}

		Debug.Log( "Healed! " + Health.Value );
	}

	public void Damage( float amount ) {

		if ( Health.Value <= 0 ) {

			return;
		}

		Health.Value -= amount;

		EventSystem.RaiseEvent( new RecievedDamage {Character = this, Damage = amount} );
	}

	public bool IsEnemy() {

		return TeamId != 0;
	}

	protected virtual void Dispose() {
		
		_compositeDisposable.Dispose();
		Health.Dispose();
	}

	public void Destroy() {

		Dispose();

		Pawn.Destroy();
	}

	private void OnUpdate( long ticks ) {

		StateController.Tick( Time.deltaTime );
	}

	private void OnModifiersChange() {

	}

}