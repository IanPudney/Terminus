using UnityEngine;
using System.Collections;

public class WorldObject : MonoBehaviour {
	public int xPos, yPos;
	public int futureXPos, futureYPos;
	public bool isMoving, isColliding;
	public bool isVerified;
	
	protected virtual void Start () {
		xPos = Mathf.FloorToInt(transform.position.x);
		yPos = Mathf.FloorToInt(transform.position.y);
		futureXPos = xPos;
		futureYPos = yPos;
		isMoving = false;
		isColliding = false;
		isVerified = false;
		SpaceControl.obj.currentSpace[xPos][yPos] = this;
	}
	
	public virtual void Telegraph(int x, int y) {
		futureXPos = x;
		futureYPos = y;
		WorldObject moveCollision = SpaceControl.obj.currentSpace[futureXPos][futureYPos];
		if (moveCollision == null) {
			SpaceControl.obj.currentSpace[futureXPos][futureYPos] = this;
			isMoving = true;
			isColliding = false;
			return;
		} else {
			moveCollision.isMoving = false;
			moveCollision.isColliding = true;
			isMoving = false;
			isColliding = true;
			return;
		}
	}
	
	public virtual void VerifyMove() {
		WorldObject occupyingObject = SpaceControl.obj.currentSpace[futureXPos][futureYPos];
		if (occupyingObject == null) {
			isMoving = true;
			isColliding = false;
			isVerified = true;
			return;
		}
		if (occupyingObject.MoveDirection == MoveDirection) {
			isMoving = true;
			isColliding = false;
			isVerified = true;
			return;
		}
		if (!occupyingObject.isVerified) {
			occupyingObject.VerifyMove();
		}
		if (occupyingObject.MoveDirection == MoveDirection && occupyingObject.isMoving) {
			isMoving = true;
			isColliding = false;
			isVerified = true;
			return;
		}
		isMoving = false;
		isColliding = true;
		isVerified = false;
		return;
	}
	
	public virtual void Move() {
		if (!isMoving) {
			return;
		} else if (isVerified) {
			return;
		}
		VerifyMove();
	}
	
	public virtual void Cleanup() {
		SpaceControl.obj.futureSpace[futureXPos][futureYPos] = null;
		
		if (isMoving) {
			WorldObject currentObject = SpaceControl.obj.currentSpace[xPos][yPos];
			if (currentObject == this) {
				currentObject = null;
			}
			
			xPos = futureXPos;
			yPos = futureYPos;
			SpaceControl.obj.currentSpace[xPos][yPos] = this;
		}
	}
	
	public Vector2 MoveDirection {
		get { return new Vector2(futureXPos - xPos, futureYPos - yPos); }
	}
	
	public virtual void Reset() {
		
	}
}
