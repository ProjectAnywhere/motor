using UnityEngine;
using System.Collections;
using System;

public class Button : MonoBehaviour {

	private string info;
	private Vector3 pos3D;
	private int angle1, angle2;	
	private int old1 = 0, old2 = 0;
	private Vector2 pos2D;
	public Texture img;
	
	void Start() {
		info = "Ready";
	}
	
	void OnGUI() {
		//label
		GUI.Label(new Rect(80,10,200,30),info);		
		//button
		if(GUI.Button(new Rect(80,35,200,30),"Run")) {
			int send1 = angle1 - old1;
			int send2 = angle2 - old2;
			old1 = angle1;
			old2 = angle2;
			info = "Send (" + send1 + ", " + send2 + ")";
			Sending.sendCmd(send1, send2);
		}

		//label
		Vector3 cam = Camera.main.transform.position;
		GUI.Label(new Rect(400,10,200,30), "Camera Position: " + cam);
		GUI.Label(new Rect(400,35,200,30), "Target Position: " + pos3D);
		GUI.Label(new Rect(650,10,200,30), "Old Angle 1: " + old1);
		GUI.Label(new Rect(650,35,200,30), "Old Angle 2: " + old2);
		GUI.Label(new Rect(800,10,200,30), "Angle 1: " + angle1);
		GUI.Label(new Rect(800,35,200,30), "Angle 2: " + angle2);
		if (Input.GetMouseButton(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)) {
				if(hit.collider.gameObject.tag=="Finish") {
					pos3D = hit.point;
					angle1 = 45 - (int)(Math.Atan2(pos3D.z, pos3D.x)*180.0/Math.PI);
					double d = Math.Sqrt(pos3D.x*pos3D.x+pos3D.z*pos3D.z);
					angle2 = 45 - (int)(Math.Atan2(6-pos3D.y, d)*90.0/Math.PI);
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
