using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	
	public float timeToDestroy;
	public enum TargetEnum {Player = 0, Enemy = 1};
	public TargetEnum target = 0;
	public int damage;
	public static GameObject bulletParticle = (GameObject) Resources.Load("Prefabs/Particles/BulletHit");
	public static GameObject laserParticle  = (GameObject) Resources.Load("Prefabs/Particles/LaserHit");
	
	
	// Use this for initialization
	void Start () {
	}
	
	
	public void setDamage(int dmg) {
		this.damage = dmg;
	}	
	
	void OnTriggerEnter(Collider col){
				
		if(col.tag != "DevLayer") {
			if(target == TargetEnum.Enemy) {
				GameObject tmpParticle = (GameObject) Instantiate(laserParticle, transform.position, transform.rotation);
				tmpParticle.transform.LookAt(Enemy.player.transform);
				//mover un toque la particula para que se vea mas
				tmpParticle.transform.position -= (tmpParticle.transform.position - Enemy.player.transform.position).normalized;
				Destroy(tmpParticle,0.9f);
			}
			HealthSystem colTMP = col.GetComponent<HealthSystem>();
			if (colTMP != null) {
				//para que no se auto-saque vida
				if (colTMP.type == HealthSystem.Type.Player && target == TargetEnum.Player || colTMP.type == HealthSystem.Type.Enemy && target == TargetEnum.Enemy || colTMP.type == HealthSystem.Type.Bullet) {
					colTMP.damageHp(damage);
					Destroy(gameObject);
				}
				
			}
		}

	}
		


	
	
	
	// Update is called once per frame
	void Update() {
		Destroy (gameObject, timeToDestroy);
	}
}
