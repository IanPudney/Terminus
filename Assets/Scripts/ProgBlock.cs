using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgBlock : MonoBehaviour {
  protected Text info;
  protected Image infoPanel;
  
  protected virtual void Start () {
  	Object textObj = GameObject.Instantiate(ProtoDict.obj.infoText);
  	infoPanel = ((GameObject)textObj).GetComponent<Image>();
  	infoPanel.transform.SetParent(transform);
  	infoPanel.transform.localPosition = new Vector3(0.25f, 0f, -2f);
  	info = infoPanel.GetComponentInChildren<Text>();
  	info.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
  }
  
  protected virtual void Update() {
	infoPanel.rectTransform.sizeDelta = info.rectTransform.sizeDelta * info.transform.localScale.x
										+ new Vector2(0.5f, 0.5f);
  }
  
  IEnumerator OnMouseDown()
  {
    //remove from parent
	ProgBlock oldParent = transform.parent.gameObject.GetComponent<ProgBlock> ();

	transform.SetParent(UICanvas.obj.transform);

    Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);
    Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
    
		Debug.Log ("Trying oldparent fixup");
		if (oldParent != null) {
			Debug.Log ("Oldparent not null");
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
		RectTransform blockRect = GetComponent<RectTransform>();
		Vector3 blockRectCenter = transform.TransformPoint(transform.position);
		float blockMinX = blockRectCenter.x - blockRect.rect.width;
		float blockMaxX = blockRectCenter.x + blockRect.rect.width;
		float blockMinY = blockRectCenter.y - blockRect.rect.height;
		float blockMaxY = blockRectCenter.y + blockRect.rect.height;
		print ("xc" + CameraExtensions.BoundsMin(CodeCamera.obj).x + " " + CameraExtensions.BoundsMax(CodeCamera.obj).x);
		print ("yc" + CameraExtensions.BoundsMin(CodeCamera.obj).y + " " + CameraExtensions.BoundsMax(CodeCamera.obj).y);
		
		
		print ("xb" + blockMinX + " " + blockMaxX);
		print ("yb" + blockMinY + " " + blockMaxY);
		/*if (blockMinX > CameraExtensions.BoundsMax(CodeCamera.obj).x) {
			print ("Too right");
		} else if (blockMaxX < CameraExtensions.BoundsMin(CodeCamera.obj).x) {
			print ("Too left" + blockMaxX + " " + CameraExtensions.BoundsMin(CodeCamera.obj).x);
		}
		if (blockMinY > CameraExtensions.BoundsMax(CodeCamera.obj).y) {
			print ("Too high");
		} else if (blockMaxY < CameraExtensions.BoundsMin(CodeCamera.obj).y) {
			print ("Too low");
		}*/
		/*if(
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
