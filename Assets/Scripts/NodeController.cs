using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour {

	public List<GameObject> EnteringEdges = new List<GameObject>();
	public List<GameObject> LeavingEdges = new List<GameObject>();
	public bool isMovable = false;

	void Update () {
		if (this.isMovable) {
			if (Input.GetMouseButton (0)) {
				this.transform.position = InputHelper.MousePosition();
				foreach (var edge in this.EnteringEdges) {
					var edgeController = edge.GetComponent<EdgeController>();
					edgeController.SetProperty(edgeController.Head.transform.position, this.transform.position);
				}
				foreach (var edge in this.LeavingEdges) {
					var edgeController = edge.GetComponent<EdgeController>();
					edgeController.SetProperty(this.transform.position, edgeController.Tail.transform.position);
				}
			}

			if (Input.GetMouseButtonUp (0)) {
				this.isMovable = false;
			}
		}
	}
}
