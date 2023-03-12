using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraYandereChan : MonoBehaviour
{
    Menu menu;
    SystemPersonagens scriptPerson;

    Transform targetCamYan;

    [SerializeField] float mouseSensivity = 10;
    [SerializeField] float distanceFromTarget = 2;
    [SerializeField] Vector2 pitchMinMax = new Vector2(-40, 85);

    [SerializeField] float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    [SerializeField] List<string> nameObjtsCamAtravesa = new List<string>();
    RaycastHit hit = new RaycastHit();

    [SerializeField] int indiceScene;

    float x;
    float y;

    void Awake()
    {
        menu = FindObjectOfType<Menu>();
        scriptPerson = FindObjectOfType<SystemPersonagens>();

        targetCamYan = GameObject.Find("targetComeraBiaChiqui").transform;

        Scene("Menu");
    }

    void Scene(string name)
    {
        if (menu == null) /* Verifica se a Scene não é a do Menu e Loga nela */
            SceneManager.LoadSceneAsync(name);
    }

    void Start() =>
        mouseSensivity = menu.sensibility;

    void LateUpdate()
    {
        x += Input.GetAxis("Mouse X") * mouseSensivity;
        y -= Input.GetAxis("Mouse Y") * mouseSensivity;
        y = Mathf.Clamp(y, pitchMinMax.x, pitchMinMax.y);

        if (!menu.escape && !scriptPerson.atvCaixaEscolhas) {
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(y, x),
            /***/ ref rotationSmoothVelocity, rotationSmoothTime);
            transform.eulerAngles = currentRotation;
        }

        transform.position = targetCamYan.position - transform.forward * distanceFromTarget;

        if (Physics.Linecast(targetCamYan.position, transform.position, out hit))
        {
            // Objeto atingido não é um NPC ou NPC está atrás de uma parede
            for (int i = 0; i < nameObjtsCamAtravesa.Count; i++)
            {
                if (hit.collider.name == nameObjtsCamAtravesa[i])
                    transform.position = hit.point + transform.forward * 0;
            }
        }
    }
}
