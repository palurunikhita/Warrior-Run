﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public int health = 100;

	private EnemyScript enemyScript;
	private Animator anim;
	// Use this for initialization
	void Awake()
	{

		enemyScript = GetComponent<EnemyScript>();
		anim = GetComponent<Animator>();
	}
	public void ApplyDamage(int damageAmount)
	{
		health -= damageAmount;

		if (health < 0)
		{
			health = 0;
		}
		if (health == 0)
		{
			enemyScript.enabled = false;
			anim.SetTrigger(MyTags.DEAD_Trigger);

			Invoke("DeactivateEnemy", 3f);
		}
	}
	void DeactivateEnemy()
    {
		gameObject.SetActive(false);
    }
}
