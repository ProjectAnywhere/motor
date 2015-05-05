/// <summary>
/// Sending.cs is used to establish the connection between Unity and Arduino;
/// the command (two angle) will be sent as a string
/// Date: 5/1/2015
/// Author: Wei Huang
/// </summary>

using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class Sending : MonoBehaviour {
    
	// change the port based on your computer setting
	public static SerialPort sp = new SerialPort("COM4", 9600);
	// Use this for initialization
	void Start () {
		OpenConnection();
	}

	// Connect the SerialPort
	public void OpenConnection() {
		if (sp != null) {
			if (sp.IsOpen) {
         		sp.Close();
          		print("Closing port, because it was already open!");
         	}
	         else {
				sp.Open();  // opens the connection
		     	sp.ReadTimeout = 16;  // sets the timeout value before reporting error
		     	print("Port Opened!");
	         }
       	}
		else {
	         if (sp.IsOpen) {
				print("Port is already open");
	         }
	         else {
				print("Port == null");
	         }
		}
    }

	// Close the SerialPort
    void OnApplicationQuit() {
		sp.Close();
    }

	// Send the command (two angle) to SerialPort
	public static void sendCmd(int angle1, int angle2){
		sp.Write(angle1+","+angle2);
	}
}
