﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public int health = 100;

	private PlayerScript playerScript;
	private Animator anim;
	// Use this for initialization
	void Awake () {

		playerScript = GetComponent<PlayerScript>();
		anim = GetComponent<Animator>();
	}
	void Start()
    {
		GameplayController.instance.DisplayHealth(health);
    }
	public void ApplyDamage(int damageAmount)
    {
		health -= damageAmount;

        if (health < 0)
        {
			health = 0;
        }
		//display health
		GameplayController.instance.DisplayHealth(health);
		if (health == 0)
        {
			playerScript.enabled=false;
			anim.Play(MyTags.DEAD_Animation);

			//Call gameover panel
			GameplayController.instance.isPlayerAlive = false;

			//Game over panel
			GameplayController.instance.GameOver();
        }
    }
	void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == MyTags.COIN_Tag)
        {
			collider.gameObject.SetActive(false);

			GameplayController.instance.CoinCollected();
			SoundManager.instance.PlayCoinSound();
        }
    }
}
