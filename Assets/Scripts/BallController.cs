using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.Windows.Speech;
//using System.Linq;

public class BallController : MonoBehaviour {
   // private KeywordRecognizer keywordRecognizer;
  //  private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public GameObject particle;
    [SerializeField] ParticleSystem SuccessParticles;
    [SerializeField] AudioClip success;
    [SerializeField]
    private float speed;
    [SerializeField]
    float jumpForce;
    bool started;
    bool gameOver;
    Rigidbody rb;
    AudioSource audioSource;
    public UiManager ui;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start() {
        // ui = addGetComponent<uiman>();
       // actions.Add("left", xaxis);
       // actions.Add("right", yaxis);
       // actions.Add("pause", Pause);
       // actions.Add("quit", Quit);
       // actions.Add("view", View);
        //keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
       // keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
       // keywordRecognizer.Start();
        //keywordRecognizer.Stop();
        started = false;
        gameOver = false;
        audioSource = GetComponent<AudioSource>();
        
    }
   // private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    //{
       // Debug.Log(speech.text);
       // actions[speech.text].Invoke();
   // }
    //private void xaxis()
   // {
        //  rb.velocity = new Vector3(speed, 0, 0);
   // }
   // private void yaxis()
   // {
      //  rb.velocity = new Vector3(0, 0, speed);
  //  }
    // Update is called once per frame
    void Update()
    {
        
        if (Application.platform == RuntimePlatform.Android)
        {
            if (!started)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    int pointerID = Input.GetTouch(0).fingerId;
                    if (!EventSystem.current.IsPointerOverGameObject(pointerID))
                    {
                        rb.velocity = new Vector3(speed, 0, 0);
                        started = true;
                        GameManager.instance.StartGame();
                    }
                }
            }
        }
        else
        {
            if (!started)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        rb.velocity = new Vector3(speed, 0, 0);
                        started = true;
                        GameManager.instance.StartGame();
                    }
                }
            }
        }
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
        if (!Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            gameOver = true;
            rb.velocity = new Vector3(0, -25f, 0);
            

            GameManager.instance.GameOver();
            ui.gameover(); 

        }
        if (Input.GetMouseButtonDown(0) && !gameOver)
        {
         
            SwitchDirection();
        }
    }
        void SwitchDirection()
        {

            if (rb.velocity.z > 0)
            {

                rb.velocity = new Vector3(speed, 0, 0);

            }
            else if (rb.velocity.x > 0)
            {

                rb.velocity = new Vector3(0, 0, speed);
            }
        }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Diamond")
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(success);
            SuccessParticles.Play();
            //speed = speed + 1;
            ScoreManager.instance.incrementScoreDiamond(); 
            GameObject part = Instantiate(particle, col.gameObject.transform.position, Quaternion.identity) as GameObject;

            Destroy(col.gameObject);
            Destroy(part, 1f);


        }
    }
}

