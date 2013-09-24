using UnityEngine;
using System;
using System.Collections;

public class Crosshair : MonoBehaviour {


	
	// mis attrs
	private string display;
	private int mira_x, mira_y;
	public Texture2D mira;
	private Vector3 oldVec;
	private Vector3 vec3Look;
	public GameObject arma;
	private int fireRate;
	private bool limitChange=false;
	public int control=0;
	// Use this for initialization
	void Start () {
		WiiMote.wiimote_start();
		//mira = (Texture2D) Resources.Load("crosshair");
	}
 
	// Update is called once per frame
	void FixedUpdate () {
		int c = WiiMote.wiimote_count();
		
		bool isB = WiiMote.wiimote_getButtonB(control);
		bool isA = WiiMote.wiimote_getButtonA(control);
		bool isBtnLeft = WiiMote.wiimote_getButtonLeft(control);
		bool isBtnRight = WiiMote.wiimote_getButtonRight(control);
		
		if(isB) {
			arma.GetComponent<Arma>().shoot();		
		}
		
		if(isA) {
			Enemy.player.GetComponent<PlayerBehave>().reloadWep();
			
		}
		//if(isBtnRight && limitChange==false && arma.GetComponent<Arma>().enumInt<=1){
		if(isBtnRight) {
			arma.GetComponent<Arma>().weaponModel = Arma.WeaponEnum.Laser;
		}
		
		//if(isBtnLeft && limitChange==false && arma.GetComponent<Arma>().enumInt==2){
		if(isBtnLeft) {
			arma.GetComponent<Arma>().weaponModel = Arma.WeaponEnum.Rifle;
		}
	//	if (c>0) {
		//	display = "";
		//	for (int i=0; i<=c-1; i++) {
				int x = WiiMote.wiimote_getAccX(control) ;
				int y = WiiMote.wiimote_getAccY(control);
				int z = WiiMote.wiimote_getAccZ(control);

				float roll = Mathf.Round(WiiMote.wiimote_getRoll(control));
				float p = Mathf.Round(WiiMote.wiimote_getPitch(control));
				float yaw = Mathf.Round(WiiMote.wiimote_getYaw(control));
				float ir_x = WiiMote.wiimote_getIrX(control);
				float ir_y = WiiMote.wiimote_getIrY(control);
				//display += "Wiimote " + i + " accX: " + x + " accY: " + y + " accZ: " + z + " roll: " + roll + " pitch: " + p + " yaw: " + yaw + " IR X: " + ir_x + " IR Y: " + ir_y + "\n";
				//if (!float.IsNaN(roll) && !float.IsNaN(p) && (i==c-1)) {

				    Vector3 vec = new Vector3(p, 0 , -1 * roll);
				    vec = Vector3.Lerp(oldVec, vec, Time.deltaTime * 5);
				    oldVec = vec;
					//oldVec.z = -(transform.position.x - Camera.mainCamera.transform.position.x);
					
				//}
			   // if ( (i==c-1) && (ir_x != -100) && (ir_y != -100) ) {
				    	//float temp_x = ((ir_x + (float) 1.0)/ (float)2.0) * (float) Screen.width;
				    	//float temp_y = (float) Screen.height - (((ir_y + (float) 1.0)/ (float)2.0) * (float) Screen.height);
				    	float temp_x = ( Screen.width / 2) + ir_x * (float) Screen.width / (float)2.0;
				    	float temp_y = Screen.height - (ir_y * (float) Screen.height / (float)2.0);
				    	mira_x = Mathf.RoundToInt(temp_x);
				    	mira_y = Mathf.RoundToInt(temp_y);
						vec3Look = camera.ScreenToWorldPoint(new Vector3(mira_x + 17.5f, Screen.height*1.5f - mira_y -20f , -20));
						arma.transform.LookAt(vec3Look);
			//	}
	//		}
	//	}
	//	else display = "Press the '1' and '2' buttons on your Wii Remote.";


		    
	}
 
		
		

 
	void OnGUI() {
		//GUI.Label( new Rect(10,10, 500, 100), display);
		//if ((mira_x != 0) || (mira_y != 0)) GUI.Box ( new Rect (mira_x, mira_y, 50, 50), mira); //"Pointing\nHere");
		int c = WiiMote.wiimote_count();
		for (int i=0; i<=c-1; i++) {
			float ir_x = WiiMote.wiimote_getIrX(control);
			float ir_y = WiiMote.wiimote_getIrY(control);
		    if ( (ir_x != -100) && (ir_y != -100) ) {
			    float temp_x = ((ir_x + (float) 1.0)/ (float)2.0) * (float) Screen.width;
			    float temp_y = (float) Screen.height - (((ir_y + (float) 1.0)/ (float)2.0) * (float) Screen.height);
			    temp_x = Mathf.RoundToInt(temp_x);
			    temp_y = Mathf.RoundToInt(temp_y);
				//if ((mira_x != 0) || (mira_y != 0))
				GUI.DrawTexture ( new Rect (temp_x, temp_y, 64, 64), mira, ScaleMode.ScaleToFit, true, 1.0F);
		    }
		}
	}
}
