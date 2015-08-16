using UnityEngine;
using System.Collections;

public class Placeholder : Statement {
  //this statement does nothing

  // Use this for initialization
  protected override void Start () {
    placeholders.Add (this);
    base.Start();
  }
  
  protected override void Update() {
  	base.Update ();
  }

  //a list of all of these objects
  static public System.Collections.ArrayList placeholders = new ArrayList();

  //get the nearest one of these
  static public Placeholder GetClosestPlaceholder(Transform targetTransform) {
    Placeholder pMin = null;
    float minDist = float.MaxValue;
    Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    foreach (Object obj in placeholders) {
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
  			currentPos.y > objBounds.min.y)
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
