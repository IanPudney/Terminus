using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StmtBlock : Statement {
  // Use this for initialization
  virtual public void Start () {
    Debug.Log ("Instantiating Placeholder");
    GameObject initialPlaceholder = Instantiate(ProtoDict.obj.placeholder, transform.position, transform.rotation) as GameObject;
    initialPlaceholder.transform.SetParent(this.transform);
    initialPlaceholder.GetComponent<Placeholder> ().OnReplace += ReplacePlaceholder;
    Layout ();
    base.Start();
  }
  
  // Update is called once per frame
  void Update () {
  }

  void ReplacePlaceholder(Placeholder ps, ProgBlock pg) {
    //TODO: allow middle placeholders
    ps.transform.parent = null;
    pg.transform.parent = this.transform;
    ps.transform.parent = this.transform;

    ProgBlock uppermost = this;
    while (true) {
      if(uppermost.transform.parent == UICanvas.obj.transform) break;
      ProgBlock parent = uppermost.transform.parent.gameObject.GetComponent<ProgBlock>();
	  if(parent == UICanvas.obj.transform) break;
      uppermost = parent;
    }
    uppermost.Layout ();

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
