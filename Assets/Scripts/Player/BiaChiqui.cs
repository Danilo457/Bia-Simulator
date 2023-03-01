using System.Collections.Generic;
using UnityEngine;

public class BiaChiqui : MonoBehaviour
{
    [SerializeField] ScriptableBancoDeDados bancoDados;

    [SerializeField] float speed;
    [SerializeField] float speedShift;

    [SerializeField] float gravidade;

    [SerializeField] List<GameObject> cabelos = new List<GameObject>();

    PlayerManager playerManager;
    Menu menu;
    SystemPersonagens scriptPerson;

    Animation anim;
    AudioSource audioToks;

    Rigidbody rb;

    [HideInInspector] public bool animSegurar;
    [HideInInspector] public bool animAbrirTrancaArmario;

    GameObject targetPosition;
    GameObject targetPositionArmario;

    bool movimento;

    float time;
    float velocidade;
    float x;
    float y;

    void Awake() 
    {
        playerManager = FindObjectOfType<PlayerManager>();
        menu = FindObjectOfType<Menu>();
        scriptPerson = FindObjectOfType<SystemPersonagens>();

        anim = GetComponent<Animation>();

        audioToks = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();

        for (int i = 0; i < cabelos.Count; i++)
            cabelos[i].SetActive(true);

        Time.timeScale = 1; // UnPause
    }

    void Start()
    {
        if (targetPosition == null) {
            targetPosition = GameObject.Find("Target - Play Start");

            transform.position = targetPosition.transform.position;
        }

        if (targetPositionArmario == null)
            targetPositionArmario = GameObject.Find("Target - Position Player Armario");
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        movimento = x != 0 || y != 0;

        if (Input.GetKey(KeyCode.LeftShift))
            velocidade = speed + speedShift;
        else
            velocidade = speed;

        if (!scriptPerson.atvCaixaEscolhas)
            Rotation();

        if (!menu.escape && !scriptPerson.atvCaixaEscolhas)
            Animation();
    }

    void Rotation() {
        float camY = Camera.main.transform.rotation.eulerAngles.y;

        if (y == 1) 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY, 0), Time.deltaTime * 5);
        
        if (y == -1) 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY - 180, 0), Time.deltaTime * 5);
        
        if (x == 1 || x == -1) 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY + (x * 90), 0), Time.deltaTime * 5);
    }

    void Animation()
    {
        if (!animAbrirTrancaArmario) {
            if (movimento && Input.GetKey(KeyCode.LeftShift)) 
                anim.Play(bancoDados.clip[3].name);
            else if (movimento)
                anim.Play(bancoDados.clip[2].name);
            else
                anim.Play(bancoDados.clip[0].name);
        }
        else
            PlayArmarioAnimation();
    }

    void PlayArmarioAnimation()
    {
        time += Time.deltaTime;
        transform.position = targetPositionArmario.transform.position;
        transform.rotation = targetPositionArmario.transform.rotation;
        anim.Play(bancoDados.clip[4].name);

        if (time >= 2.0f) {
            time = 0.0f;
            animAbrirTrancaArmario = false;
        }
    }

    void FixedUpdate()
    {
        if (movimento && !scriptPerson.atvCaixaEscolhas)
            rb.MovePosition(rb.position + velocidade * Time.fixedDeltaTime * transform.forward);

        //rb.AddForce(new Vector3(0, -1, 0) * 3.0f, ForceMode.Impulse);
        //rb.AddForce(3 * -Vector2.up, ForceMode.Impulse);
    }
    
    public void EventBiaAudioArmarioTranca() {
        playerManager.myListAudios.TryGetValue("Tranca do Armario", out AudioClip audio);

        audioToks.clip = audio;
        audioToks.loop = false;
        audioToks.volume = 0.3f;

        audioToks.Play();
    }
}
