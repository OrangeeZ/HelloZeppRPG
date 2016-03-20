using UnityEngine;
using System.Collections;

public abstract class EMCharacterBehaviour : ScriptableObject {

	public abstract IEnumerator UpdateLoop();

}
