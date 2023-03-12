using System.Collections.Generic;
using UnityEngine;
using UnityChan;

public class BiaChiqui : MonoBehaviour
{
    // Variáveis para referências a outros scripts, objetos e componentes do jogo
    PlayerManager     playerManager;
    Menu              menu;
    SystemPersonagens scriptPerson;

    // Variáveis para animação e áudio
    Animation   anim;
    AudioSource audioToks;

    // Variáveis do Rigidbody
    Rigidbody rb;

    // Variáveis que podem ser modificadas pelo usuário na interface do Unity
    [SerializeField] ScriptableBancoDeDados bancoDados;
    [SerializeField] float speed;
    [SerializeField] float speedShift;
    [SerializeField] float gravidade;
    [SerializeField] List<GameObject> cabelos      = new List<GameObject>();
    [SerializeField] Bones fisicasBones = new Bones();

    // Variáveis públicas que podem ser acessadas por outros scripts
    [HideInInspector] public bool animSegurar;
    [HideInInspector] public bool animAbrirTrancaArmario;

    // Variáveis para posicionamento e movimento
    GameObject targetPosition;
    GameObject targetPositionArmario;
    bool  movimento;
    float time;
    float velocidade;
    float x;
    float y;

    void Awake() 
    {
        // Procura as instâncias dos scripts PlayerManager, Menu e SystemPersonagens na cena
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

        // Faz a rotação do Player de acordo com a direção do movimento
        if (!scriptPerson.atvCaixaEscolhas)
            Rotation();

        // Controla a animação do Player
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
        // Pega o ângulo de rotação da câmera
        float camY = Camera.main.transform.rotation.eulerAngles.y;

        // Se o jogador estiver se movendo para cima (direção Y positiva), rotacione na direção da câmera
        if (y == 1) 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY, 0), Time.deltaTime * 5);

        /*   Se o Player estiver se movendo para baixo (direção Y negativa),      *
        *    rotacione na direção oposta da câmera (180 graus de diferença)       */
        if (y == -1) 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY - 180, 0), Time.deltaTime * 5);

        /*   Se o Player estiver se movendo para a esquerda ou direita (direção X positiva ou negativa),     *
         *   rotacione na direção da câmera com um deslocamento de 90 graus                                  */
        if (x == 1 || x == -1) 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY + (x * 90), 0), Time.deltaTime * 5);
    }

    void Animation()
    {
        // Se a animação de abrir o armário não estiver ativa
        if (!animAbrirTrancaArmario)
        { // Se o Player estiver se movendo e segurando Shift, toque a animação de correr
            if (movimento && Input.GetKey(KeyCode.LeftShift))
                anim.Play(bancoDados.clip[3].name);
            else if (movimento) // Se o Player estiver se movendo, toque a animação de caminhar
                anim.Play(bancoDados.clip[2].name);
            else // Se o Player não estiver se movendo, toque a animação de parado
                anim.Play(bancoDados.clip[0].name);
        }
        else // Se a animação de abrir o armário estiver ativa, toque a animação correspondente
            PlayArmarioAnimation();
    }

    void PlayArmarioAnimation()
    {
        time += Time.deltaTime;
        transform.position = targetPositionArmario.transform.position;
        transform.rotation = targetPositionArmario.transform.rotation;
        anim.Play(bancoDados.clip[4].name);

        /*  Se o tempo de espera da animação de abrir o armário for maior que 2 segundos,    *
         *  termine a animação e retorne ao jogo                                             */
        if (time >= 2.0f) {
            time = 0.0f;
            animAbrirTrancaArmario = false;
        }
    }

    // Função chamada para tocar o áudio de abrir a tranca do armário
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
