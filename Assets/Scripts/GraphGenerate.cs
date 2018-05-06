using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGenerate : MonoBehaviour {

	public GameObject Node;
	public GameObject Edge;
	public Vector3 clickPosition;
	private Vector3 start;

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = new Ray ();
			RaycastHit hit = new RaycastHit ();
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			// if on node, create edge
			if (Physics.Raycast (ray.origin, ray.direction, out hit)) {
				GameObject obj = hit.collider.gameObject;
				if (obj.CompareTag ("node")) {
					CreateEdge(obj);
				}
			} else {
				CreateNode (InputHelper.MousePosition());
			}
		}
	}

	private void CreateNode (Vector3 position) {
		Instantiate (Node, position, Node.transform.rotation);
	}

	private void CreateEdge (GameObject node) {
		var edge = Instantiate (Edge, node.transform.position, Edge.transform.rotation);
		edge.GetComponent<EdgeController>().SetStartNode(node);
	}
}
