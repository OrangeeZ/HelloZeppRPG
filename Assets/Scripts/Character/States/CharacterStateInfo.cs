using System;
using UnityEngine;

public abstract class CharacterStateInfo : ScriptableObject {

	[Header( "Animation settings" )]
	public string AnimatorStateName;

	public abstract CharacterState GetState();

}
