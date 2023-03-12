using System.Collections.Generic;
using UnityEngine;
using UnityChan;

public class BiaChiqui : MonoBehaviour
{
    // Vari�veis para refer�ncias a outros scripts, objetos e componentes do jogo
    PlayerManager     playerManager;
    Menu              menu;
    SystemPersonagens scriptPerson;

    // Vari�veis para anima��o e �udio
    Animation   anim;
    AudioSource audioToks;

    // Vari�veis do Rigidbody
    Rigidbody rb;

    // Vari�veis que podem ser modificadas pelo usu�rio na interface do Unity
    [SerializeField] ScriptableBancoDeDados bancoDados;
    [SerializeField] float speed;
    [SerializeField] float speedShift;
    [SerializeField] float gravidade;
    [SerializeField] List<GameObject> cabelos      = new List<GameObject>();
    [SerializeField] Bones fisicasBones = new Bones();

    // Vari�veis p�blicas que podem ser acessadas por outros scripts
    [HideInInspector] public bool animSegurar;
    [HideInInspector] public bool animAbrirTrancaArmario;

    // Vari�veis para posicionamento e movimento
    GameObject targetPosition;
    GameObject targetPositionArmario;
    bool  movimento;
    float time;
    float velocidade;
    float x;
    float y;

    void Awake() 
    {
        // Procura as inst�ncias dos scripts PlayerManager, Menu e SystemPersonagens na cena
        playerManager = FindObjectOfType<PlayerManager>();
        menu          = FindObjectOfType<Menu>();
        scriptPerson  = FindObjectOfType<SystemPersonagens>();

        anim      = GetComponent<Animation>();
        audioToks = GetComponent<AudioSource>();
        rb        = GetComponent<Rigidbody>();

        // Ativa todos os cabelos na lista de cabelos
        for (int i = 0; i < cabelos.Count; i++)
            cabelos[i].SetActive(true);

        Time.timeScale = 1; // UnPause
    }

    void Start()
    {
        // Procura os objetos "Target - Play Start" e "Target - Position Player Armario" na cena
        if (targetPosition == null) {
            targetPosition = GameObject.Find("Target - Play Start");

            transform.position = targetPosition.transform.position;
        }

        if (targetPositionArmario == null)
            targetPositionArmario = GameObject.Find("Target - Position Player Armario");
    }

    void Update()
    {
        // Recebe os valores de entrada do teclado para movimentar o Player
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        movimento = x != 0 || y != 0;

        // Define a velocidade de movimento do Player com ou sem a tecla Shift pressionada
        if (Input.GetKey(KeyCode.LeftShift))
            velocidade = speed + speedShift;
        else
            velocidade = speed;

        fisicasBones.Fisicas(menu.escape ? 1.0f : 0.01f); // Trocar o Valor pra ficar melhor quando der PAUSE

        // Faz a rota��o do Player de acordo com a dire��o do movimento
        if (!scriptPerson.atvCaixaEscolhas)
            Rotation();

        // Controla a anima��o do Player
        if (!menu.escape && !scriptPerson.atvCaixaEscolhas)
            Animation();
    }

    void FixedUpdate()
    {
        if (movimento && !scriptPerson.atvCaixaEscolhas)
            rb.MovePosition(rb.position + velocidade * Time.fixedDeltaTime * transform.forward);

        //rb.AddForce(new Vector3(0, -1, 0) * 3.0f, ForceMode.Impulse);
        //rb.AddForce(3 * -Vector2.up, ForceMode.Impulse);
    }

    void Rotation() {
        // Pega o �ngulo de rota��o da c�mera
        float camY = Camera.main.transform.rotation.eulerAngles.y;

        // Se o jogador estiver se movendo para cima (dire��o Y positiva), rotacione na dire��o da c�mera
        if (y == 1) 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY, 0), Time.deltaTime * 5);

        /*   Se o Player estiver se movendo para baixo (dire��o Y negativa),      *
        *    rotacione na dire��o oposta da c�mera (180 graus de diferen�a)       */
        if (y == -1) 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY - 180, 0), Time.deltaTime * 5);

        /*   Se o Player estiver se movendo para a esquerda ou direita (dire��o X positiva ou negativa),     *
         *   rotacione na dire��o da c�mera com um deslocamento de 90 graus                                  */
        if (x == 1 || x == -1) 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY + (x * 90), 0), Time.deltaTime * 5);
    }

    void Animation()
    {
        // Se a anima��o de abrir o arm�rio n�o estiver ativa
        if (!animAbrirTrancaArmario)
        { // Se o Player estiver se movendo e segurando Shift, toque a anima��o de correr
            if (movimento && Input.GetKey(KeyCode.LeftShift))
                anim.Play(bancoDados.clip[3].name);
            else if (movimento) // Se o Player estiver se movendo, toque a anima��o de caminhar
                anim.Play(bancoDados.clip[2].name);
            else // Se o Player n�o estiver se movendo, toque a anima��o de parado
                anim.Play(bancoDados.clip[0].name);
        }
        else // Se a anima��o de abrir o arm�rio estiver ativa, toque a anima��o correspondente
            PlayArmarioAnimation();
    }

    void PlayArmarioAnimation()
    {
        time += Time.deltaTime;
        transform.position = targetPositionArmario.transform.position;
        transform.rotation = targetPositionArmario.transform.rotation;
        anim.Play(bancoDados.clip[4].name);

        /*  Se o tempo de espera da anima��o de abrir o arm�rio for maior que 2 segundos,    *
         *  termine a anima��o e retorne ao jogo                                             */
        if (time >= 2.0f) {
            time = 0.0f;
            animAbrirTrancaArmario = false;
        }
    }

    // Fun��o chamada para tocar o �udio de abrir a tranca do arm�rio
    public void EventBiaAudioArmarioTranca() {
        playerManager.myListAudios.TryGetValue("Tranca do Armario", out AudioClip audio);

        audioToks.clip = audio;
        audioToks.loop = false;
        audioToks.volume = 0.3f;

        audioToks.Play();
    }
}

[System.Serializable]
public class Bones
{ /* SpringBone Assesado diretamente de um namespace que controla a fisica */
    public List<SpringBone> scriptsBones = new List<SpringBone>();

    public void Fisicas(float value)
    {
        foreach (SpringBone bone in scriptsBones)
            bone.threshold = value;
    }
}
