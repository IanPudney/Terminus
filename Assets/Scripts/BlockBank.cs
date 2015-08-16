using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlockBank : MonoBehaviour {
	public enum BlockType {
		robotMove, robotLeft, robotRight,
		doorOpen, doorClose, doorToggle,
		whileBlock, ifElse, entryPoint, triggerPoint,
	};
	public BlockType blockType;
	public int numLeft = 100;
	
	private GameObject blockProto;
	private string blockDocString;
	protected Text label;
	
	void Start() {
		switch (blockType) {
			case (BlockType.robotMove):
				blockProto = ProtoDict.obj.robotMove;
				blockDocString = "Move R Fwd 1";
				break;
			case (BlockType.robotLeft):
				blockProto = ProtoDict.obj.robotLeft;
				blockDocString = "Turn R Left";
				break;
			case (BlockType.robotRight):
				blockProto = ProtoDict.obj.robotRight;
				blockDocString = "Turn R Right";
				break;
			case (BlockType.doorClose):
				blockProto = ProtoDict.obj.doorClose;
				blockDocString = "Close Door";
				break;
			case (BlockType.doorOpen):
				blockProto = ProtoDict.obj.doorOpen;
				blockDocString = "Open Door";
				break;
			case (BlockType.doorToggle):
				blockProto = ProtoDict.obj.doorToggle;
				blockDocString = "Toggle Door";
				break;
			case (BlockType.whileBlock):
				blockProto = ProtoDict.obj.whileBlock;
				blockDocString = "While Loop";
				break;
			case (BlockType.ifElse):
				blockProto = ProtoDict.obj.ifElse;
				blockDocString = "If/Else Split";
				break;
			case (BlockType.entryPoint):
				blockProto = ProtoDict.obj.entryPoint;
				blockDocString = "New Statement";
				break;
			case (BlockType.triggerPoint):
				blockProto = ProtoDict.obj.triggerPoint;
				blockDocString = "Triggered Statement";
				break;
		}
		
		label = GameObject.Instantiate(ProtoDict.obj.label) as Text;
		label.transform.SetParent(transform);
		label.transform.localPosition = new Vector3(0, 0, -2);
		label.fontSize = 25;
		label.transform.localScale = new Vector3(0.02f, 
		                                         0.02f, 
		                                         0.02f);
		label.text = blockDocString;
		GetComponentInChildren<TextMesh>().text = ("x" + numLeft);
		GetComponent<Image>().color = blockProto.GetComponent<Image>().color;
		GetComponentInChildren<MeshRenderer>().material.color = blockProto.GetComponent<Image>().color;
	}
	
	/*public bool HasSameClass() {
	
	}*/
  
	void OnMouseDown()
	{
		if (numLeft <= 0) {
			return;
		} else {
			numLeft -= 1;
			GetComponentInChildren<TextMesh>().text = ("x" + numLeft);
		}		
		GameObject newBlock = Instantiate(blockProto) as GameObject;
		newBlock.transform.SetParent(UICanvas.obj.transform);
		newBlock.transform.localScale = transform.localScale;
		newBlock.transform.position = transform.position;
		ProgBlock newProgBlock = newBlock.GetComponent<ProgBlock>();
		newProgBlock.OnMouseDown ();
		return;
	}
}
