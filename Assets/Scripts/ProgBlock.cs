using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgBlock : MonoBehaviour {
  protected Text label;
  
  protected virtual void Start () {
		label = GameObject.Instantiate(ProtoDict.obj.label) as Text;
		label.transform.parent = transform;
		label.transform.localPosition = new Vector3(0, 0, -2);
		label.transform.localScale = new Vector3(0.02f/transform.localScale.x, 
		                                     0.02f/transform.localScale.y, 
		                                     0.02f/transform.localScale.z);
		label.fontSize = 25;
  }
  
  protected virtual void Update() {
  }
  
  IEnumerator OnMouseDown()
  {
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
		/*Rect blockRect = this.GetComponent<Rect>();
		if(CodeCamera.obj.rect.Contains(blockRect.center + Vector2.up * (blockRect.height * 0.5f)) &&
		   CodeCamera.obj.rect.Contains(blockRect.center - Vector2.up * (blockRect.height * 0.5f)) &&
		   CodeCamera.obj.rect.Contains(blockRect.center + Vector2.right * (blockRect.width * 0.5f)) &&
		   CodeCamera.obj.rect.Contains(blockRect.center - Vector2.right * (blockRect.width * 0.5f))) {
			print ("Well shit");
		}*/
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
