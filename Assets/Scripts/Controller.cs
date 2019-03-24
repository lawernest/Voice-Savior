using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Controller : MonoBehaviour {

	public static GameObject selected;
	public static int weaponIndex;

	protected abstract void Buy(string name);
}
