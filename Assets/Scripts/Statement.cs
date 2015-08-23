using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Statement : ProgBlock {
	int nextStmt = 0;
	Color defaultColor;

	public bool TakesTurn {get; protected set;}

	public List<Statement> getChildren() {
		List<Statement> children = new List<Statement>();
		foreach(Transform t in transform) {
			Statement stmt = t.gameObject.GetComponent<Statement>();
			if(stmt != null && !(stmt is Placeholder)) children.Add (stmt);
		}
		return children;
	}

	protected static Statement highlighted = null;
	protected void Highlight() {
		GetComponent<Image>().color = defaultColor * 0.5f;
	}
	protected void Unhighlight() {
		GetComponent<Image>().color = defaultColor;
	}

	protected override void Start () {
	base.Start();
	defaultColor = GetComponent<Image>().color;
	}
	
	protected override void Update () {
		base.Update();
	}

	public virtual void OnTick() {}
	public virtual void OnTelegraph() {}
	public virtual void OnStartAction() { OnTick (); }
	public virtual void OnStopAction() {}
}
