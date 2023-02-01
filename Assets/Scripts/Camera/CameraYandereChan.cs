using System.Collections.Generic;
using UnityEngine;

public class CameraYandereChan : MonoBehaviour
{
    Menu menu;

    Transform targetCamYan;

    [SerializeField] float mouseSensivity = 10;
    [SerializeField] float distanceFromTarget = 2;
    [SerializeField] Vector2 pitchMinMax = new Vector2(-40, 85);

    [SerializeField] float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    [SerializeField] List<string> nameObjtsCamAtravesa = new List<string>();
    RaycastHit hit = new RaycastHit();

    float x;
    float y;

    void Awake()
    {
        menu = FindObjectOfType<Menu>();

        targetCamYan = GameObject.Find("targetComeraBiaChiqui").transform;
    }

    private void Start()
    {
        mouseSensivity = menu.sensibility;
    }

    void LateUpdate()
    {
        x += Input.GetAxis("Mouse X") * mouseSensivity;
        y -= Input.GetAxis("Mouse Y") * mouseSensivity;
        y = Mathf.Clamp(y, pitchMinMax.x, pitchMinMax.y);

        if (!menu.escape) {
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(y, x),
            /***/ ref rotationSmoothVelocity, rotationSmoothTime);
            transform.eulerAngles = currentRotation;
        }

        transform.position = targetCamYan.position - transform.forward * distanceFromTarget;

        if (Physics.Linecast(targetCamYan.position, transform.position, out hit)) {
            for (int i = 0; i < nameObjtsCamAtravesa.Count; i++) {
                if (hit.collider.name == nameObjtsCamAtravesa[i])
                    transform.position = hit.point + transform.forward * 0;
            }

            //Debug.Log(hit.collider.name);
        }
    }
}
