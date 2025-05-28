using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour {

	public GameObject player, playButton;
    public Animator anim;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(MyTags.PLAYER_Tag);
        anim = GetComponent<Animator>();
    }
	public void DeactivateGameObject()
    {
        player.SetActive(false);
        playButton.SetActive(false);
    }
    public void ActivateGameObject()
    {
        player.SetActive(true);
        playButton.SetActive(true);
    }
    void OnTriggerEnter(Collider target)
    {
        if(target.tag==MyTags.PLAYER_Tag)
        {
            anim.Play("DoorSlideOut");
        }
    }
    void OnTriggerExit(Collider target)
    {
        if (target.tag == MyTags.PLAYER_Tag)
        {
            anim.Play("DoorSlideIn");
        }
    }
}
