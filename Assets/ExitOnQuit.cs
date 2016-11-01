using UnityEngine;
using System.Collections;

public class ExitOnQuit : MonoBehaviour {

	// Use this for initialization
	public void Quit() {
        Debug.logger.Log("Exit");
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
	}
}
