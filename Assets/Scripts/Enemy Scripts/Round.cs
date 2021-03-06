using UnityEngine;
using System.Collections;
using System.Xml;

public static class Round{
	
	public static int destinyQuant;
  	public static int number = 0;
	public static int count;
	public static string [] type;
	public static int    [] rate;
	private static XmlDocument xmlAsset = new XmlDocument();
	public static bool isTargetRound, gameOver, weaponSpawned, weaponPicked = false;

	
	
	static Round() {
	 	TextAsset temporal = (TextAsset) Resources.Load("data/Waves");
		xmlAsset.LoadXml(temporal.text);
		Debug.Log("Reading waves.xml");
		
		Round.count = xmlAsset.ChildNodes[1].ChildNodes.Count;
		Debug.Log("Playing with " + Round.count + " waves");
	}
	
	
	public static void next(){
		/*childNodes[1] la primera vez porque el primer node del archivo es <?xml version="1.0" encoding="UTF-8"?>\
		 * despues uso el numero de ronda para agarrar al childNode
		 * */
		
		number++;
		if (number <= count) {
			XmlNode roundData = xmlAsset.ChildNodes[1].ChildNodes[number-1];	
			XmlNode typeNode = roundData["type"];
			isTargetRound = false;
			type = new string[typeNode.ChildNodes.Count];
			if (typeNode.ChildNodes.Count > 0) {
				for (int i=0; i<typeNode.ChildNodes.Count; i++) {
					type[i]	= typeNode.ChildNodes[i].InnerText;
					//rate[i] = int.Parse(typeNode.ChildNodes[i].Attributes["rate"].InnerText);
				}
			}
			else {
				rate[0] = 100;
				type[0] = (string) roundData["type"].InnerText;
			}
			/*for (int i=0; i<type.Length; i++) {
				Debug.Log(type[i]);
			}*/		
			
			if (type[0] == "targets") {
				isTargetRound = true;
			}
			else {
				
				if(roundData["spawnWeapon"] != null) {
					weaponPicked = false;
					weaponSpawned = true;	
				}
				
				destinyQuant = int.Parse(roundData["destiny"].InnerText);
				RoundManager.interval = float.Parse(roundData["interval"].InnerText);	
			}
		} else {
			gameOver = true;
		}
	}

}
