using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGenerate : MonoBehaviour {

	public GameObject NodePrefab;
	public GameObject EdgePrefab;

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
		Instantiate (NodePrefab, position, NodePrefab.transform.rotation);
	}

	private void CreateEdge (GameObject node) {
		var edge = Instantiate (EdgePrefab, node.transform.position, EdgePrefab.transform.rotation);
		edge.GetComponent<EdgeController>().SetStartNode(node);
	}
}
