using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGenerate : MonoBehaviour {

	public GameObject NodePrefab;
	public GameObject EdgePrefab;
	public int Mode = 0;
	public const int DRAWMODE = 0;
	public const int MOVEMODE = 1;

	// Update is called once per frame
	void Update ()
	{
		if (!Input.GetMouseButton (0)) {
			this.modeDetection();
		}

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = new Ray ();
			RaycastHit hit = new RaycastHit ();
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			switch (Mode) {
				case DRAWMODE:
					// if on node, create edge
					if (Physics.Raycast (ray.origin, ray.direction, out hit)) {
						GameObject obj = hit.collider.gameObject;
						if (obj.CompareTag ("node")) {
							createEdge(obj);
						}
					} else {
						createNode (InputHelper.MousePosition());
					}
					break;
				case MOVEMODE:
					// if on node, move the node
					if (Physics.Raycast (ray.origin, ray.direction, out hit)) {
						GameObject obj = hit.collider.gameObject;
						if (obj.CompareTag ("node")) {
							obj.GetComponent<NodeController>().isMovable = true;
						}
					}
					break;
			}
		}
	}

	private void createNode (Vector3 position) {
		Instantiate (NodePrefab, position, NodePrefab.transform.rotation);
	}

	private void createEdge (GameObject node) {
		var edge = Instantiate (EdgePrefab, node.transform.position, EdgePrefab.transform.rotation);
		edge.GetComponent<EdgeController>().Head = node;
	}

	private void modeDetection() {
		if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.M)) {
			this.Mode = MOVEMODE;
		}

		if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.D)) {
			this.Mode = DRAWMODE;
		}
	}
}
