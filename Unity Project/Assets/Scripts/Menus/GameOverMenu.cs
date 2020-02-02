using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {

	public Animator transition;

    private void Start()
    {
        StartCoroutine("QuitAfterSeconds");
    }

    public void QuitToMenu(){
		StartCoroutine(transitionScene(2f, "Menu"));
	}

    private IEnumerator QuitAfterSeconds()
    {
        yield return new WaitForSeconds(3);
        QuitToMenu();
    }

	private IEnumerator transitionScene(float duration, string sceneName){
		transition.SetTrigger("FadeOut");
		float elapsed = 0;
		while (elapsed < duration){
			elapsed += Time.deltaTime;
			yield return null;
		}
		SceneManager.LoadSceneAsync(sceneName);
	}
}
