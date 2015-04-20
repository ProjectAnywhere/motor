using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class Sending : MonoBehaviour {
    
    //public static SerialPort sp = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
	public static SerialPort sp = new SerialPort("COM3", 9600);
	public string msg;
	// Use this for initialization
	void Start () {
		OpenConnection();
	}
	
	// Update is called once per frame
	void Update () {
			msg = sp.ReadLine();
			print(msg);
	}

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

    void OnApplicationQuit() {
		sp.Close();
    }

    public static void sendCmd(int a, int b){
		sp.Write(a+","+b);
    }
}
