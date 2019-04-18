using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Controller : MonoBehaviour {

	public static GameObject selected;
	public static GameObject weaponInfo;
	public static int weaponIndex;

	public abstract void Buy(string name);
}
