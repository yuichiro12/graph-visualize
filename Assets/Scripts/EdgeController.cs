using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController : MonoBehaviour {

	public GameObject ConePrefab;
	public bool isDirected;
	public GameObject Head;
	public GameObject Tail;

	private GameObject TopCone;
	private bool isEdgeDragged;

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
		if (isEdgeDragged) {
			if (Input.GetMouseButtonUp (0)) {
				Ray ray = new Ray ();
				RaycastHit hit = new RaycastHit ();
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				// if on node, determine current edge; otherwise destroy
				// TODO: remove loop edge
				if (Physics.Raycast (ray.origin, ray.direction, out hit)) {
					GameObject obj = hit.collider.gameObject;
					if (obj.CompareTag ("node")) {
						// fix this edge property
						this.Tail = obj;
						SetProperty (Head.transform.position, Tail.transform.position);
						Head.GetComponent<NodeController>().LeavingEdges.Add(this.gameObject);
						Tail.GetComponent<NodeController>().EnteringEdges.Add(this.gameObject);
					} else {
						Destroy (this.gameObject);
						if (this.isDirected) Destroy (this.TopCone);
					}
				} else {
					Destroy (this.gameObject);
					if (this.isDirected) Destroy (this.TopCone);
				}
				isEdgeDragged = false;
			}

			if (Input.GetMouseButton (0)) {
				SetProperty (Head.transform.position, InputHelper.MousePosition());
			}
		}
	}

	// Mouse position 
	public void SetProperty (Vector3 headPosition, Vector3 tailPosition) {
		var dir = tailPosition - headPosition;
		this.transform.rotation = Quaternion.FromToRotation (Vector3.up, dir);
		this.transform.position = dir / 2f + headPosition;
		this.transform.localScale = new Vector3 (0.1f, Vector3.Magnitude (dir) / 2f, 0.1f);
		if (this.isDirected) {
			TopCone.transform.rotation = Quaternion.FromToRotation (Vector3.back, dir);
			TopCone.transform.position = tailPosition - dir.normalized * 0.2f;
		}
	}
}
