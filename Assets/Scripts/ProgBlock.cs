using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgBlock : MonoBehaviour {
  protected Text info;
  protected Image infoPanel;
  
  // Use this for initialization
  protected virtual void Start () {
  	Object textObj = GameObject.Instantiate(ProtoDict.obj.infoText);
  	infoPanel = ((GameObject)textObj).GetComponent<Image>();
  	infoPanel.transform.SetParent(transform);
  	infoPanel.rectTransform.localPosition = new Vector2(0.75f, 0f);
  	info = infoPanel.GetComponentInChildren<Text>();
	infoPanel.rectTransform.sizeDelta = info.rectTransform.sizeDelta + Vector2.one;
  }
  
  IEnumerator OnMouseDown()
  {
    //remove from parent
	transform.SetParent(UICanvas.obj.transform);

    Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);
    Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
    
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
    }
  }
  public virtual float Layout() {
    return transform.localScale.y;
  }
}
