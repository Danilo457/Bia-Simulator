using UnityEngine;

public class PortaPiquena : MonoBehaviour
{
    Animator anim;

    [SerializeField] Detecta detecta;
    Menu menu;

    BoxCollider boxColl;

    float timeAtvColl;

    bool aberfech;
    bool atvColl;

    void Awake()
    {
        menu = FindObjectOfType<Menu>();

        anim = GetComponent<Animator>();
        detecta.GetComponent<Detecta>();
        boxColl = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && detecta.local && !menu.escape)
        {
            aberfech = !aberfech;

            if (aberfech) {
                anim.SetBool("Porta do Deposito - Abrir", true);
                anim.SetBool("Porta do Deposito - Fechar", false);
                boxColl.enabled = false;
            }else {
                anim.SetBool("Porta do Deposito - Abrir", false);
                anim.SetBool("Porta do Deposito - Fechar", true);

                atvColl = true;
            }
        }

        if (atvColl) {
            timeAtvColl += Time.deltaTime;

            if (timeAtvColl >= 1.0f) {
                boxColl.enabled = true;

                timeAtvColl = 0.0f;
                atvColl = false;
            }
        }
    }
}
