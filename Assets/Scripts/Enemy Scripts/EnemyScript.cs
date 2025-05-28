using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	private GameObject player;
	private Rigidbody myBody;
	private Animator anim;

	private float enemy_speed = 10f;
	private float enemy_watch_treshold=70f;
	private float enemy_attack_treshold = 6f;

	public GameObject damagePoint;

	void Awake () {
		player = GameObject.FindGameObjectWithTag(MyTags.PLAYER_Tag);
		myBody = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
	}
	void FixedUpdate () {
		
		if(GameplayController.instance.isPlayerAlive)
        {
			EnemyAI();
		}
        else
        {
			if(anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUN_Animation)|| anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.ATTACK_Animation))
            {
				anim.SetTrigger(MyTags.STOP_Trigger);
            }
        }
	}
	void EnemyAI()
    {
		Vector3 direction = player.transform.position - transform.position;
		float distance = direction.magnitude;
		direction.Normalize();

		Vector3 velocity = direction * enemy_speed;

        if (distance > enemy_attack_treshold && distance < enemy_watch_treshold)
        {
			myBody.velocity = new Vector3(velocity.x, myBody.velocity.y, velocity.z);

            if (anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.ATTACK_Animation))
            {
				anim.SetTrigger(MyTags.STOP_Trigger);
            }
			anim.SetTrigger(MyTags.RUN_Trigger);

			transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        }
		else if (distance < enemy_attack_treshold)
        {
			if (anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUN_Animation))
			{
				anim.SetTrigger(MyTags.STOP_Trigger);
			}
			anim.SetTrigger(MyTags.ATTACK_Trigger);

			transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
		}
        else
        {
			myBody.velocity = new Vector3(0f, 0f, 0f);

			if(anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.ATTACK_Animation)|| anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUN_Animation))
            {
				anim.SetTrigger(MyTags.STOP_Trigger);
			}
        }
    }
	void ActivateDamagePoint()
    {
		damagePoint.SetActive(true);
    }
	void DectivateDamagePoint()
	{
		damagePoint.SetActive(false);
	}
}
