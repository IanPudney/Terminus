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
    initialPlaceholder.GetComponent<Placeholder> ().OnReplace += ReplacePlaceholder;

	indentationHint = Instantiate(ProtoDict.obj.indentationHint, transform.position, transform.rotation) as GameObject;
	indentationHint.transform.SetParent(this.transform);
	indentationHint.transform.localPosition = new Vector3(-2f, 0f, 0f);
	indentationHint.transform.localScale = new Vector3(1, 1, 1);
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
    Vector3 nextPosition = new Vector3 (1f, -transform.localScale.y, 0);
    Transform last = null;
    foreach (Transform child in transform) {
      ProgBlock block = child.GetComponent<ProgBlock>();
      if (!block) {
      	continue;
      }
      float height = block.Layout();
      Vector3 blockPosition = block.transform.localPosition;
      block.transform.localPosition = nextPosition;
      nextPosition.y -= height;
      last = child;
    }
    //nextPosition = last.transform.localPosition;
    //nextPosition.x = 0.0f;
    //last.transform.localPosition = nextPosition;
		indentationHint.transform.localScale = Vector3.one;
		indentationHint.transform.localPosition = new Vector3(-2, -1, 0);

    return -nextPosition.y;
  }
}
