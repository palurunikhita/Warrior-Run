using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	public Animator levelPanelanim;

	public void PlayGame()
    {
        levelPanelanim.Play("SlideIn");
    }
    public void Back()
    {
        levelPanelanim.Play("SlideOut");
    }
}
