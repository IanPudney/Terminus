using UnityEngine;
using System.Collections;

public static class CameraExtensions
{
	//******Orthographic Camera Only******//
	
	public static Vector2 BoundsMin(this Camera camera) {
		return new Vector2(camera.transform.position.x, camera.transform.position.y) - camera.Extents();
	}
	
	public static Vector2 BoundsMax(this Camera camera) {
		return new Vector2(camera.transform.position.x, camera.transform.position.y) + camera.Extents();
	}
	
	public static Vector2 Extents(this Camera camera) {
		if (camera.orthographic) {
			return new Vector2(camera.orthographicSize * Screen.width/Screen.height, camera.orthographicSize);
		} else {
			Debug.LogError("Camera is not orthographic!", camera);
			return new Vector2();
		}
	}
	//*****End of Orthographic Only*****//
}