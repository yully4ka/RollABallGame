using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public AudioClip collideSound;
    public Text counterText;
    public float seconds, minutes, milliseconds;

    private Rigidbody rb;
    private int count;
    private AudioSource sourcePlay;

    private float startTime;
    private bool stopTime = false;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        sourcePlay = GetComponent<AudioSource>();
        startTime = Time.time;
	}

    void FixedUpdate()
    {   
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
        if (stopTime)
            return;
        Timer();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            sourcePlay.Play();
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
        if (count >= 12)
        {
            stopTime = true;
            winText.text = "You win!";
        }
    }

    public void Timer()
    {
        minutes = (int)(Time.time / 60f);
        seconds = (int)(Time.time % 60f);
        milliseconds = (int)(Time.time * 1000f) % 1000;
        counterText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("000");
        //float t = Time.time - startTime;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
