using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StmtBlock : Statement {
	public GameObject indentationHint;
  virtual public void Start () {
	base.Start ();
    GameObject initialPlaceholder = Instantiate(ProtoDict.obj.placeholder, transform.position, transform.rotation) as GameObject;
    initialPlaceholder.transform.SetParent(this.transform);
	initialPlaceholder.transform.localScale = Vector3.one;
    initialPlaceholder.GetComponent<Placeholder> ().OnReplace += ReplacePlaceholder;

	indentationHint = Instantiate(ProtoDict.obj.indentationHint, transform.position, transform.rotation) as GameObject;
	indentationHint.transform.SetParent(this.transform);
	indentationHint.transform.localPosition = new Vector3(-2f, 0f, 0f);
	indentationHint.transform.localScale = Vector3.one;
	indentationHint.GetComponent<Image>().color = GetComponent<Image>().color;
    Layout ();
  }
  
  protected override void Update () {
  	base.Update();
  }

  void ReplacePlaceholder(Placeholder ps, ProgBlock pg) {
    //TODO: allow middle placeholders
    ps.transform.SetParent(null);
    pg.transform.SetParent(this.transform);
    ps.transform.SetParent(this.transform);

		RecursiveLayout ();
  }

	public override float Layout() {
		float result = base.Layout ();
		indentationHint.transform.localScale = Vector3.one;
		indentationHint.transform.localPosition = new Vector3(-2f, -1f, 0);
		return result;
	}

}
