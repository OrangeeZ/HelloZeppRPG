using UniRx;
using UnityEngine;
using System.Collections;
using System;

public class CharacterPawnBase : AObject {

	[SerializeField]
	public CharacterComplexAnimationController _animationController;

	public CharacterComplexAnimationController animatedView { get; private set; }

	public Character character { get; private set; }

	protected virtual void Start() {

		animatedView = _animationController;
	}

	public void SetCharacter( Character character ) {

		this.character = character;
	}

	public virtual void MakeDead() {

	}

	public void Destroy() {

		Destroy( gameObject );
	}

}