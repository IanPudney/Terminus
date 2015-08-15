using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StmtBlock : Statement {
  virtual public void Start () {
    Debug.Log ("Instantiating Placeholder");
    GameObject initialPlaceholder = Instantiate(ProtoDict.obj.placeholder, transform.position, transform.rotation) as GameObject;
    initialPlaceholder.transform.SetParent(this.transform);
    initialPlaceholder.GetComponent<Placeholder> ().OnReplace += ReplacePlaceholder;
    Layout ();
    base.Start();
  }
  
  protected override void Update () {
  	base.Update();
  }

  void ReplacePlaceholder(Placeholder ps, ProgBlock pg) {
    //TODO: allow middle placeholders
    ps.transform.parent = null;
    pg.transform.SetParent(this.transform);
    ps.transform.SetParent(this.transform);

		RecursiveLayout ();
  }

  public override float Layout() {
    Vector3 nextPosition = new Vector3 (0.2f, -transform.localScale.y, 0);
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
    return -nextPosition.y;
  }
}
