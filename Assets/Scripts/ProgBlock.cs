using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgBlock : MonoBehaviour {
	//Set to true for program elements the player cannot change.
	public bool boundToParent = false;
  protected Text label;

	protected virtual void Start() {
		SetupLabel ();
	}

  
  public virtual Text SetupLabel () {
		if(label != null) return label;
		label = GameObject.Instantiate(ProtoDict.obj.label) as Text;
		label.transform.SetParent(transform);
		label.transform.localPosition = new Vector3(0, 0, -2);
		label.fontSize = 13;
		label.transform.localScale = new Vector3(0.04f, 
		                                     0.04f, 
		                                     0.04f);
		return label;
  }
  
  protected virtual void Update() {
  }
  
  public IEnumerator OnMouseDown()
  {
		Vector3 origPos = transform.position;
		float origZ = origPos.z;
		origPos.z = 100;
		transform.position = origPos;
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
		origPos = transform.position;
		origPos.z = origZ;
		transform.position = origPos;

    //find the nearest placeholder and replace it
    Placeholder nearest = Placeholder.GetClosestPlaceholder (transform);
    if (nearest != null) {
    	nearest.GetComponent<Placeholder> ().Replace (this);
    } else {
		/*RectTransform blockRect = GetComponent<RectTransform>();
		Vector3 blockRectCenter = transform.TransformPoint(transform.position);
		float blockMinX = blockRectCenter.x - blockRect.rect.width;
		float blockMaxX = blockRectCenter.x + blockRect.rect.width;
		float blockMinY = blockRectCenter.y - blockRect.rect.height;
		float blockMaxY = blockRectCenter.y + blockRect.rect.height;
		print ("xc" + CameraExtensions.BoundsMin(CodeCamera.obj).x + " " + CameraExtensions.BoundsMax(CodeCamera.obj).x);
		print ("yc" + CameraExtensions.BoundsMin(CodeCamera.obj).y + " " + CameraExtensions.BoundsMax(CodeCamera.obj).y);
		
		
		print ("xb" + blockMinX + " " + blockMaxX);
		print ("yb" + blockMinY + " " + blockMaxY);*/
		/*if (blockMinX > CameraExtensions.BoundsMax(CodeCamera.obj).x) {
			print ("Too right");
		} else if (blockMaxX < CameraExtensions.BoundsMin(CodeCamera.obj).x) {
			print ("Too left" + blockMaxX + " " + CameraExtensions.BoundsMin(CodeCamera.obj).x);
		}
		if (blockMinY > CameraExtensions.BoundsMax(CodeCamera.obj).y) {
			print ("Too high");
		} else if (blockMaxY < CameraExtensions.BoundsMin(CodeCamera.obj).y) {
			print ("Too low");
		}
		Rect blockRect = this.GetComponent<Rect>();
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
