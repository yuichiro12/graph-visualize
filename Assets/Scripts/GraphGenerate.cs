using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGenerate : MonoBehaviour {

	public GameObject node;
	public GameObject edge;
	public Vector3 clickPosition;
	private Vector3 start;
	private GameObject currentEdge;
	private bool isEdgeDragged;

	// Update is called once per frame
	void Update ()
	{
		Debug.Log (Input.GetMouseButton (0));
		// 0 for left button, 1 for right button 2 for middle button
		if (Input.GetMouseButtonUp (0)) {
			if (isEdgeDragged) {
				Ray ray = new Ray ();
				RaycastHit hit = new RaycastHit ();
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				// if on node, determine current edge; otherwise destroy
				// TODO: remove loop
				if (Physics.Raycast (ray.origin, ray.direction, out hit)) {
					GameObject obj = hit.collider.gameObject;
					if (obj.CompareTag ("node")) {
						setCurrentEdgeProperty (start, obj.transform.position);
					} else {
						Destroy (currentEdge);
					}
				} else {
					Destroy (currentEdge);
				}
				isEdgeDragged = false;
			}
		}

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = new Ray ();
			RaycastHit hit = new RaycastHit ();
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			// if on node, create edge
			if (Physics.Raycast (ray.origin, ray.direction, out hit)) {
				GameObject obj = hit.collider.gameObject;
				if (obj.CompareTag ("node")) {
					start = obj.transform.position;
					currentEdge = Instantiate (edge, obj.transform.position, edge.transform.rotation);
					isEdgeDragged = true;
				}
			// if not, create node
			} else {
				clickPosition = s2w(Input.mousePosition + Vector3.forward);
				Instantiate (node, clickPosition, node.transform.rotation);
			}
		}

		if (Input.GetMouseButton (0)) {
			if (isEdgeDragged) {
				setCurrentEdgeProperty (start, s2w (Input.mousePosition) + Vector3.forward);
			}
		}
	}

	private void setCurrentEdgeProperty (Vector3 start, Vector3 end) {
		currentEdge.transform.rotation = Quaternion.FromToRotation (Vector3.up, end - start);
		currentEdge.transform.position = (end - start) / 2f + start;
		currentEdge.transform.localScale = new Vector3 (0.1f, Vector3.Magnitude (end - start) / 2f, 0.1f);
	}

	private Vector3 s2w (Vector3 position)
	{
		return Camera.main.ScreenToWorldPoint (position);
	}
}
