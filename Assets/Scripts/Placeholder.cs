using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
		Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		foreach (Object obj in FindObjectsOfType<Placeholder>()) {
			Placeholder holder = (Placeholder) obj;
			Bounds objBounds = holder.GetComponent<BoxCollider>().bounds;
			//skip if member of self
			if(holder.transform.parent == targetTransform) {
				continue;
			}
			float dist = Vector2.Distance(holder.transform.position, currentPos);
			if (dist < minDist &&
					currentPos.x < objBounds.max.x &&
					currentPos.y < objBounds.max.y &&
					currentPos.x > objBounds.min.x &&
					currentPos.y > objBounds.min.y) {
				minDist = dist;
				pMin = holder;
			}
		}
		foreach (BlockBank bank in FindObjectsOfType<BlockBank>()) {
			//skip if wrong bank
			if(bank != targetTransform.GetComponent<ProgBlock>().blockBank) {
				continue;
			}
			//Check children, etc
			Bounds bankBounds = bank.GetComponent<BoxCollider>().bounds;
			float dist = Vector2.Distance(bank.transform.position, currentPos);
			if (dist < minDist &&
				    currentPos.x < bankBounds.max.x &&
				    currentPos.y < bankBounds.max.y &&
				    currentPos.x > bankBounds.min.x &&
				    currentPos.y > bankBounds.min.y) {
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
