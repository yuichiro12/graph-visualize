using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController : MonoBehaviour {

	public GameObject ConePrefab;
	public bool isDirected;
	private GameObject TopCone;
	private GameObject StartNode;
	private GameObject EndNode;
	private bool isEdgeDragged;

	public void SetStartNode (GameObject node) {
		this.StartNode = node;
	}

	public GameObject GetStartNode () {
		return this.StartNode;
	}
	public void SetEndNode (GameObject node) {
		this.EndNode = node;
	}

	public GameObject GetEndNode () {
		return this.EndNode;
	}

	// Use this for initialization
	void Awake () {
		isEdgeDragged = true;
		if (this.isDirected) {
			TopCone = Instantiate (ConePrefab);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// 0 for left button, 1 for right button 2 for middle button
		if (Input.GetMouseButtonUp (0)) {
			if (isEdgeDragged) {
				Ray ray = new Ray ();
				RaycastHit hit = new RaycastHit ();
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				// if on node, determine current edge; otherwise destroy
				// TODO: remove loop edge
				if (Physics.Raycast (ray.origin, ray.direction, out hit)) {
					GameObject obj = hit.collider.gameObject;
					if (obj.CompareTag ("node")) {
						// fix this edge property
						SetEndNode(obj);
						setProperty (StartNode.transform.position, EndNode.transform.position);
					} else {
						Destroy (this.gameObject);
					}
				} else {
					Destroy (this.gameObject);
				}
				isEdgeDragged = false;
			}
		}

		if (Input.GetMouseButton (0)) {
			if (isEdgeDragged) {
				setProperty (StartNode.transform.position, InputHelper.MousePosition());
			}
		}
	}

	// Mouse position 
	private void setProperty (Vector3 start, Vector3 end) {
		var dir = end - start;
		this.transform.rotation = Quaternion.FromToRotation (Vector3.up, dir);
		this.transform.position = dir / 2f + start;
		this.transform.localScale = new Vector3 (0.1f, Vector3.Magnitude (dir) / 2f, 0.1f);
		if (this.isDirected) {
			TopCone.transform.rotation = Quaternion.FromToRotation (Vector3.back, dir);
			TopCone.transform.position = end - dir.normalized * 0.2f;
		}
	}
}
