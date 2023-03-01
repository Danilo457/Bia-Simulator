using UnityEngine;

public class Armarios : MonoBehaviour
{
    public BiaChiqui biaChiqui;

    [SerializeField] Animator anim;

    [SerializeField] BoxCollider colliderPorta;

    [SerializeField] DetectaArmario detcArmario;
    Menu menu;

    float timeAtvColl;

    bool atvAnim;
    bool atvColl;

    void Awake() 
    {
        menu = FindObjectOfType<Menu>();

        anim.GetComponent<Animator>();

        detcArmario.GetComponent<DetectaArmario>();

        colliderPorta.GetComponent<BoxCollider>();
    }

    void Start()
    {
        if (biaChiqui == null)
            biaChiqui = GameObject.Find("BiaChiqui").GetComponent<BiaChiqui>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && detcArmario.local && !menu.escape) {
            atvAnim = !atvAnim;

            if (atvAnim)
                biaChiqui.animAbrirTrancaArmario = true;
        }

        if (!biaChiqui.animAbrirTrancaArmario) {
            if (atvAnim) {
                anim.SetBool("Porta do Armario - Abrir", true);
                anim.SetBool("Porta do Armario - Fechar", false);

                colliderPorta.enabled = false;
            }else {
                anim.SetBool("Porta do Armario - Abrir", false);
                anim.SetBool("Porta do Armario - Fechar", true);

                atvColl = true;
            }
        }

        if (atvColl) {
            timeAtvColl += Time.deltaTime;

            if (timeAtvColl >= 1.0f) {
                colliderPorta.enabled = true;

                timeAtvColl = 0.0f;
                atvColl = false;
            }
        }
    }
}
