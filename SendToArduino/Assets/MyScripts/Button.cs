/*
 * Wei Huang, 5/1/2015
 */

using UnityEngine;
using System.Collections;
using System;

public class Button : MonoBehaviour {

	private string info;
	private Vector3 pos3D; // world coordinates of target point
	private int angle1, angle2;	// parameters for two motors
	private int old1 = 0, old2 = 0; // previous parameters for two motors
	private Vector2 pos2D; // screen coordinates of target point
	public Texture img; // mark the target
	
	void Start() {
		info = "Ready";
	}
	
	void OnGUI() {
		// Sending module
		GUI.Label(new Rect(50,20,120,30),info);		
		if(GUI.Button(new Rect(150,10,50,50),"Send")) {
			int send1 = angle1 - old1;
			int send2 = angle2 - old2;
			old1 = angle1;
			old2 = angle2;
			info = "Send (" + send1 + ", " + send2 + ")";
			Sending.sendCmd(send1, send2);
		}

		// Dashboard
		Vector3 cam = Camera.main.transform.position;
		GUI.Label(new Rect(300,10,200,30), "Camera Position: " + cam);
		GUI.Label(new Rect(300,35,200,30), "Target Position: " + pos3D);
		GUI.Label(new Rect(600,10,100,30), "Old Angle 1: " + old1);
		GUI.Label(new Rect(600,35,100,30), "Old Angle 2: " + old2);
		GUI.Label(new Rect(750,10,100,30), "Angle 1: " + angle1);
		GUI.Label(new Rect(750,35,100,30), "Angle 2: " + angle2);
		// Track the target point
		if (Input.GetMouseButton(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit; // intersection point of light beam with the wall
			if(Physics.Raycast(ray, out hit)) {
				if(hit.collider.gameObject.tag=="Finish") {
					pos2D = Input.mousePosition;
					pos3D = hit.point;
					// calculate parameter for horizontal rotation
					angle1 = 45 - (int)(Math.Atan2(pos3D.z, pos3D.x)*180.0/Math.PI);
					// calculate parameter for vertical rotation
					double d = Math.Sqrt(pos3D.x*pos3D.x+pos3D.z*pos3D.z);
					angle2 = 45 - (int)(Math.Atan2(6-pos3D.y, d)*90.0/Math.PI);
				}
			}
		}
		// Mark the target on screen
		GUI.DrawTexture(new Rect(pos2D.x-15, Screen.height - pos2D.y-15, 30, 30), img);

		// Reset module
		if(GUI.Button(new Rect(850,10,50,50),"Reset")) {
			pos3D = new Vector3(0, 0, 0);
			angle1 = 0;
			angle2 = 0;
			old1 = 0;
			old2 = 0;
		}
	}
}
