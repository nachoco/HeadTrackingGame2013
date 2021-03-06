using UnityEngine;
using System.Collections;

public class BootComputer : MonoBehaviour {
	
	string fullText = "booting system|wait1|.|wait1|.|wait1|.|jump1|"+
		"Initalizing shooting system 2.90|jump1|" +
		"Optimizing partyhats |jump1|" +
		"Mounting system|jump1|"+
		"Preparing long boolean variables|jump1|" +
		"Killing root user|jump1|";
	float keyStroke = 0.001f;
	public KeyboardScript kboard;
	float keyTimer = 0f;
	float delay = 0f;
	int cont = 0;
	int length;
	public TextMesh [] screenText;
	private int line = 0;
	public GameObject nameGameObject;
	
	
	// Use this for initialization
	void Start () {
		screenText[line].text = "";
		length = fullText.Length;
		for (int i=0; i<screenText.Length;i++) {
			screenText[i].text = "";	
		}
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		keyTimer += Time.deltaTime;
		
		if (cont < length) {
			if (keyTimer >= delay) {
				delay = 0f;
				if (keyTimer >= keyStroke) {
					keyTimer=0;
					if (fullText.Substring(cont,1) == "|") {
						string instruccion = fullText.Substring(cont+1,4);	
						int number =int.Parse(fullText.Substring(cont+5,1));
						if(instruccion == "wait") {
							delay = number;	
						}
						if (instruccion == "jump") {
							line += number;	
						}
						cont += 6;
						
					}
					else {
						screenText[line].text += fullText.Substring(cont,1);
					}
					cont++;
					
					
				}
			}
		} else {
			nameGameObject.SetActive(true);
			kboard.enabled = true;	
		}
		
		
		
		
	}
}
