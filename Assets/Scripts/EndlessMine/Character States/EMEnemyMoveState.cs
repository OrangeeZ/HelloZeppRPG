using UnityEngine;
using System.Collections;

[CreateAssetMenu( menuName = "EM/States/Enemy Move State" )]
public class EMEnemyMoveState : CharacterStateInfo {

	[SerializeField]
	private float _moveInterval = 1f;

	private class State : CharacterState<EMEnemyMoveState>
	{

		private float _lastMoveTime;

		public State(CharacterStateInfo info) : base(info)
		{
		}

		public override IEnumerable GetEvaluationBlock()
		{

			if (Time.time - _lastMoveTime < typedInfo._moveInterval)
			{

				yield break;
			}

			var emCharacter = character as EMCharacter;
			var animationBlock = MoveAnimation(0.5f, emCharacter.X - 1, emCharacter.Y).GetEnumerator();
			while (animationBlock.MoveNext())
			{
				yield return null;
			}

//			emCharacter.MoveBy(-1, 0);

			_lastMoveTime = Time.time;
		}

		public override bool CanBeSet()
		{
			var emCharacter = character as EMCharacter;

			return emCharacter.BattleGrid.IsFree(emCharacter.X - 1, emCharacter.Y);
		}

		private IEnumerable MoveAnimation(float duration, int targetX, int targetY)
		{
			var emCharacter = character as EMCharacter;

			emCharacter.SetPosition(targetX, targetY, updatePawnPosition: false);

			var fromPosition = character.Pawn.position;
			var toPosition = emCharacter.BattleGrid.GridToWorldPosition(targetX, targetY);

			var timer = new AutoTimer(duration);
			while (timer.ValueNormalized < 1)
			{

				character.Pawn.position = Vector3.Slerp(fromPosition, toPosition, timer.ValueNormalized);

				yield return null;
			}
		}
	}

	public override CharacterState GetState() {

		return new State( this );
	}

}