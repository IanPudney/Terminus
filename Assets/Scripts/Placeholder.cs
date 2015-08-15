using UnityEngine;
using System.Collections;

public class Placeholder : Statement {
	//this statement does nothing

	// Use this for initialization
	void Start () {
		placeholders.Add (this);
	}

	//a list of all of these objects
	static public System.Collections.ArrayList placeholders = new ArrayList();

	//get the nearest one of these
	static public Placeholder GetClosestPlaceholder(Transform targetTransform)
	{
		Placeholder pMin = null;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = targetTransform.position;
		foreach (Object obj in placeholders)
		{
			Placeholder holder = (Placeholder) obj;
			//skip if member of self
			if(holder.transform.parent == targetTransform) {
				continue;
			}
			float dist = Vector3.Distance(holder.transform.position, currentPos);
			if (dist < minDist)
			{
				minDist = dist;
				pMin = holder;
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
