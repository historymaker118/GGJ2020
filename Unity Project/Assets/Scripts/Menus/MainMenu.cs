using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GameObject titlecard;
	public GameObject loadMenu;
	public GameObject optionsMenu;
	public GameObject scoreboard;

	public Animator transition;

    public ControllerType left;
    public ControllerType right;

	void Start () {
		if (titlecard != null){
			titlecard.SetActive(true);
		}
		if (loadMenu != null){
			loadMenu.SetActive(false);
		}
		if (optionsMenu != null){
			optionsMenu.SetActive(false);
		}
		if (scoreboard != null){
			scoreboard.SetActive(false);
		}
	}

	public void StartButton_Click(){
		StartCoroutine(transitionScene(2f));
	}

	public void LoadButton_Click(){
		if (titlecard != null){
			titlecard.SetActive(false);
		}
		if (loadMenu != null){
			loadMenu.SetActive(true);
		}
		if (optionsMenu != null){
			optionsMenu.SetActive(false);
		}
		if (scoreboard != null){
			scoreboard.SetActive(false);
		}
	}

	public void OptionsButton_Click(){
		if (titlecard != null){
			titlecard.SetActive(false);
		}
		if (loadMenu != null){
			loadMenu.SetActive(false);
		}
		if (optionsMenu != null){
			optionsMenu.SetActive(true);
		}
		if (scoreboard != null){
			scoreboard.SetActive(false);
		}
	}

	public void ScoreboardButton_Click(){
		if (titlecard != null){
			titlecard.SetActive(false);
		}
		if (loadMenu != null){
			loadMenu.SetActive(false);
		}
		if (optionsMenu != null){
			optionsMenu.SetActive(false);
		}
		if (scoreboard != null){
			scoreboard.SetActive(true);
		}
	}

	public void QuitButton_Click(){
		Application.Quit();
    }

    public void Dropdown_ControllerLeft(int type)
    {
        left = (ControllerType)type;
    }

    public void Dropdown_ControllerRight(int type)
    {
        right = (ControllerType)type;
    }

    private IEnumerator transitionScene(float duration){
		transition.SetTrigger("FadeOut");
		float elapsed = 0;
		while (elapsed < duration){
			elapsed += Time.deltaTime;
			yield return null;
		}
		var load = SceneManager.LoadSceneAsync("Main");
        load.completed += loadComplete;
	}

    private void loadComplete(AsyncOperation obj)
    {
        var inst = GameController.instance;
        inst.playerL.controllerType = left;
        inst.playerR.controllerType = right;
    }
}
