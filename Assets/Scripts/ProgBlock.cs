using UnityEngine;
using System.Collections;

public class ProgBlock : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator OnMouseDown()
	{
		//remove from parent
		transform.parent = null;

		Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
		
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
		}
	}
	public virtual float Layout() {
		return transform.localScale.y;
	}
}
