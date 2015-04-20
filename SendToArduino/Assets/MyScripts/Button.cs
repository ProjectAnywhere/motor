using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	private string info;//显示的信息/
	private Vector3 pos3D;
	private int angle1, angle2;
	private Vector2 pos2D;
	public Texture img;
	
	void Start() {
		info = "Ready";
	}
	
	void OnGUI() {
		//标签/
		GUI.Label(new Rect(80,10,200,30),info);		
		//普通按钮，点击后显示Run
		if(GUI.Button(new Rect(80,35,200,30),"Run")) {
			info = "Send " + pos3D;
			Sending.sendCmd(angle1, angle2);
		}

		//标签/
		Vector3 cam = Camera.main.transform.position;
		GUI.Label(new Rect(500,10,200,30), "Camera Position: " + cam);
		GUI.Label(new Rect(500,35,200,30), "Target Position: " + pos3D);
		GUI.Label(new Rect(800,10,200,30), "Angle 1: " + angle1);
		GUI.Label(new Rect(800,35,200,30), "Angle 2: " + angle2);
		if (Input.GetMouseButton(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)) {
				if(hit.collider.gameObject.tag=="Finish") {
					pos3D = hit.point;
					angle1 = (int)pos2D.x;
					angle2 = (int)pos2D.y;
					pos2D = Input.mousePosition;
					GUI.DrawTexture(new Rect(pos2D.x,Screen.height - pos2D.y,30,30), img);
				}
			}
		}
		if(GUI.Button(new Rect(600,70,200,30),"Reset")) {
			pos3D = new Vector3(0, 0, 0);
			angle1 = 0;
			angle2 = 0;
		}
	}
}
