using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgBlock : MonoBehaviour {
	//Set to true for program elements the player cannot change.
	public bool boundToParent = false;
	protected Text label;
	//Used for adding things back into block banks
	[HideInInspector]
	public BlockBank blockBank;

	protected virtual void Start() {
		SetupLabel ();
	}
	
	public virtual Text SetupLabel () {
		if(label != null) return label;
		label = GameObject.Instantiate(ProtoDict.obj.label) as Text;
		label.transform.SetParent(transform);
		label.transform.localPosition = new Vector3(0, 0, -2);
		label.fontSize = 12;
		label.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
		return label;
	}
	
	protected virtual void Update() {
	}
	
	public IEnumerator OnMouseDown() {
		if (boundToParent) {
			return false;
		}
		//remove from parent
		ProgBlock oldParent = transform.parent.gameObject.GetComponent<ProgBlock> ();
	
		transform.SetParent(UICanvas.obj.transform);
		Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
		if (oldParent != null) {
			oldParent.RecursiveLayout ();
		}

		while (Input.GetMouseButton(0))
		{
			Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
			transform.position = curPosition;
			yield return null;
		}
		//find the nearest placeholder and replace it
		Placeholder nearest = Placeholder.GetClosestPlaceholder (transform);
		if (nearest != null) {
			nearest.GetComponent<Placeholder> ().Replace (this);
		} else {
		
		}
	}
	public virtual float Layout() {
		return transform.localScale.y;
	}

	public virtual void RecursiveLayout() {
		ProgBlock uppermost = this;
		while (true) {
			if(uppermost.transform.parent == UICanvas.obj.transform) break;
			ProgBlock parent = uppermost.transform.parent.gameObject.GetComponent<ProgBlock>();
			if(parent == UICanvas.obj.transform) break;
			uppermost = parent;
		}
		uppermost.Layout ();
	}
}
