using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class WiiCameraScript: MonoBehaviour {
	
	float X;
	float Y;
	float Z;
	public Transform center;
	Vector3 vec;
	Vector3 oldVec;
	public bool menu;
	
	void Start () {
		if (menu == true){
		
		}
		else{
			WiiMote.wiimote_start();
			print(Configuration.cameraWiiMote);
			X = WiiMote.wiimote_getIrX(Configuration.cameraWiiMote)*10;
			Y = WiiMote.wiimote_getIrY(Configuration.cameraWiiMote)*10;
			Z = 60f;
			if (X==-1000) {
				oldVec = transform.position;
			}
			else {
				oldVec = new Vector3(-X, -Y+1.8f ,Z);
			}
		}
	}
	
	
	void Update () {
		MoveCamera();
	}
	
	void MoveCamera() {
		X = WiiMote.wiimote_getIrX(Configuration.cameraWiiMote) * 10;
		Y = WiiMote.wiimote_getIrY(Configuration.cameraWiiMote) * 2;
		Z = 60f;
		
		/* NO BORRAR
		if (X==-1000 && Y==-200) {
			transform.position = oldVec;
		}
		else if(X>-9.5f && X<9.5f && Y<0f) {
			oldVec = new Vector3(-X, -Y+1.8f ,Z);
		}
		*/
		bool spaceBar = Input.GetKey(KeyCode.Space);
		
		if (spaceBar = true){
			transform.position = new Vector3(0,0,0);
		}
		
		if(X>-10f && X<10f && Y<2.5f){
			if (menu == true){
				vec = new Vector3(-X, -Y ,0f);
				gameObject.transform.LookAt(vec);
			}
			else{
				oldVec = new Vector3(-X, -Y+1.8f ,60f);
				transform.position = oldVec;
				gameObject.transform.LookAt(center);
			}
		}
		else if (X==-1000 && Y==-200){
			transform.position = oldVec;
		}	
	}
	
	void OnGUI() {
       // GUI.Label(new Rect(10, 50, 300, 300), "X: "+ X + " Y: " +Y);
		//print( "X: "+ X + " Y: " +Y);
    }
}


