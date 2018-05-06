using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHelper {

	public static Vector3 MousePosition () {
		return Camera.main.ScreenToWorldPoint (Input.mousePosition + Vector3.forward);
	}
}
