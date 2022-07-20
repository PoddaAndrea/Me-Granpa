using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController: MonoBehaviour {
	private VideoPlayer video;
	public int sceneIndex;
	private bool videoStarted = false;

	// Use this for initialization
	void Start () {
		video = GetComponent<VideoPlayer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (video.isPlaying) {
			videoStarted = true;
		}
		if (!video.isPlaying && videoStarted)
		{
			SceneManager.LoadScene(sceneIndex);
		}

		if (Input.GetButtonDown("Escape")) { 
			SceneManager.LoadScene(sceneIndex);
		}
	}
}
