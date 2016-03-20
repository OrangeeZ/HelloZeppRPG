using System;
using UnityEngine;
using System.Collections;

namespace Flow {

	public class FlowExecutor : MonoBehaviour {

		public static IKernel UpdateKernel { get; private set; }

		private void Awake() {

			UpdateKernel = Create.NewKernel();
		}

		void Start() {

			UpdateKernel.Factory.NewCoroutine( TestCoroutine );
		}

		void Update() {

			UpdateKernel.Step();
		}

		private IEnumerator TestCoroutine( IGenerator generator ) {

			Debug.Log( Time.time );

			yield return generator.ResumeAfter( new TimeSpan( 0, 0, 0, 1 ) );

			Debug.Log( Time.time );
		}

	}

}