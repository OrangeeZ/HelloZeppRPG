using System.Collections.Generic;
using System.Linq;
using System.Monads;
using UnityEngine;
using System.Collections;

public abstract class CharacterState {
    
    public readonly CharacterStateInfo info;

    protected Character character {
        get { return stateController.character; }
    }

    protected HashSet<CharacterState> possibleStates = new HashSet<CharacterState>();

    protected CharacterStateController stateController;

    protected float deltaTime;

    protected CharacterState( CharacterStateInfo info ) {

        this.info = info;
    }

    public void SetDeltaTime( float deltaTime ) {

        this.deltaTime = deltaTime;
    }

    public void SetTransitionStates( IEnumerable<CharacterState> states ) {

        foreach ( var each in states ) {

            possibleStates.Add( each );
        }
    }

    public CharacterState GetNextState() {

        return possibleStates.FirstOrDefault( which => which.CanBeSet() );
    }

    public virtual void Initialize( CharacterStateController stateController ) {

        this.stateController = stateController;
    }

    public virtual bool CanSwitchTo( CharacterState nextState ) {

        return possibleStates.Contains( nextState );
    }

    public virtual bool CanBeSet() {

        return true;
    }

    public virtual IEnumerable GetEvaluationBlock() {

        yield return null;
    }

    public void UpdateAnimator() {

	    if ( character.Pawn != null ) {
		    
			OnAnimationUpdate( character.Pawn );
	    }
    }

    protected virtual void OnAnimationUpdate( CharacterPawnBase pawn ) {

		pawn._animationController.SetBool( info.AnimatorStateName, true );
    }

}

public class CharacterState<TCharacterStateInfo> : CharacterState where TCharacterStateInfo : CharacterStateInfo {

    protected readonly TCharacterStateInfo typedInfo;

    public CharacterState( CharacterStateInfo info ) : base( info ) {

        typedInfo = info as TCharacterStateInfo;
    }

}