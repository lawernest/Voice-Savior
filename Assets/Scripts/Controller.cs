using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Controller : MonoBehaviour {

	public static GameObject selected;
	[SerializeField] protected Text gameLog;

	protected abstract void UpdateLog();
}
