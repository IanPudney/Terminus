using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Placeholder : Statement {
	//this statement does nothing

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	protected override void Update() {
		base.Update ();
	}

	//get the nearest one of these
	static public Placeholder GetClosestPlaceholder(Transform targetTransform) {
		Placeholder pMin = null;
		float minDist = float.MaxValue;
		//Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Bounds targetBounds = targetTransform.GetComponent<BoxCollider>().bounds;
		
		foreach (Object obj in FindObjectsOfType<Placeholder>()) {
			Placeholder holder = (Placeholder) obj;
			Bounds objBounds = holder.GetComponent<BoxCollider>().bounds;
			//skip if member of self
			if(holder.transform.parent == targetTransform) {
				continue;
			}
			float dist = Vector2.Distance(holder.transform.position, targetBounds.center);
			if (dist < minDist) {
				List<Vector2> corners = new List<Vector2>();
				corners.Add (targetBounds.max);
				corners.Add (targetBounds.min);
				corners.Add(new Vector2(targetBounds.max.x, targetBounds.min.y));
	            corners.Add(new Vector2(targetBounds.min.x, targetBounds.max.y));
	            foreach (Vector2 corner in corners) {
	            	if (
							corner.x < objBounds.max.x &&
							corner.y < objBounds.max.y &&
							corner.x > objBounds.min.x &&
							corner.y > objBounds.min.y) {
						minDist = dist;
						pMin = holder;
					}
				}		
			}
		}
		foreach (BlockBank bank in FindObjectsOfType<BlockBank>()) {
			//skip if wrong bank
			if(bank != targetTransform.GetComponent<ProgBlock>().blockBank) {
				continue;
			}
			//Check children, etc
			Bounds bankBounds = bank.GetComponent<BoxCollider>().bounds;
			float dist = Vector2.Distance(bank.transform.position, targetBounds.center);
			if (dist < minDist &&
				    targetBounds.center.x < bankBounds.max.x &&
				    targetBounds.center.y < bankBounds.max.y &&
				    targetBounds.center.x > bankBounds.min.x &&
				    targetBounds.center.y > bankBounds.min.y) {
				bank.numLeft += 1;
				bank.GetComponentInChildren<TextMesh>().text = ("x" + bank.numLeft);
				Destroy (targetTransform.GetComponentInChildren<Image>().gameObject);
				Destroy (targetTransform);
				return null;
			}
		}
	return pMin;
	}

	public delegate void OnReplaceDelegate(Placeholder pl, ProgBlock pg);

	//set what happens when the placeholder is replaced
	public event OnReplaceDelegate OnReplace;

	public void Replace(ProgBlock pg) {
		OnReplace (this, pg);
	}
	void OnMouseDown() {}
}
