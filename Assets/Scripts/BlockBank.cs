using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlockBank : MonoBehaviour {
	GameObject current;
	public GameObject blockProto;
	public int numLeft = 100;

	void Start() {
		Generate();
		gameObject.GetComponentInChildren<TextMesh>().text = "x" + numLeft.ToString();
	}

	void OnPickup() {
		current.GetComponent<ProgBlock>().onPickup -= OnPickup;
		numLeft--;
		gameObject.GetComponentInChildren<TextMesh>().text = "x" + numLeft.ToString();
		Generate();
	}
	void Generate() {
		if(numLeft > 0) {
			current = GameObject.Instantiate(blockProto, Vector3.zero, Quaternion.identity) as GameObject;
			Debug.Log (current.ToString());
			Debug.Log (current.GetType ().ToString());
			current.transform.SetParent (gameObject.transform);
			current.transform.localPosition = new Vector3(0.6f, 0, 0);
			current.transform.localScale = new Vector3(1, 1, 1);
			current.GetComponent<ProgBlock>().onPickup += OnPickup;
		}
	}

}
