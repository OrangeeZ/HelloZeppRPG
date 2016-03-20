using UnityEngine;
using System.Collections;

namespace EndlessMine.BattleSystem {

	public class SimpleWaveSpawner : MonoBehaviour {

		[SerializeField]
		private EMCharacterSpawner[] _spawners;

		[SerializeField]
		private int _targetSpawnCount = 10;

		private int _spawnCount;

		private IEnumerator Start() {

			while ( true ) {

				yield return new WaitForSeconds( 1f );

				foreach ( var each in _spawners ) {

					if ( !each.IsSpawnedCharacterAlive() ) {

						each.Spawn();

						_spawnCount++;

						if ( _spawnCount >= _targetSpawnCount ) {

							StopAllCoroutines();
							break;
						}
					}
				}
			}
		}

	}

}