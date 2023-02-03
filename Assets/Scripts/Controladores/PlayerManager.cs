using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] ScriptablePersonagens dadosPer;

    Menu menu;

    List<GameObject> cabelos = new List<GameObject>();
    List<GameObject> acessorios = new List<GameObject>();

    public Dictionary<string, GameObject> myListManager = new Dictionary<string, GameObject>();
    public Dictionary<string, AudioClip> myListAudios = new Dictionary<string, AudioClip>();

    void Awake()
    {
        menu = FindObjectOfType<Menu>();

        myListAudios.Add("Tranca do Armario", dadosPer.alunos.audio[0]);
    }

    void Start()
    {
        acessorios.Add(GameObject.Find("Blusa na Sintura - BiaChiqui"));
        cabelos.Add(GameObject.Find("YunoHair - BiaChiqui"));
        cabelos.Add(GameObject.Find("AmaiHairRig - BiaChiqui"));
        cabelos.Add(GameObject.Find("Bully_Kashiko - BiaChiqui"));
        cabelos.Add(GameObject.Find("Bully_Kokoro - BiaChiqui"));
        cabelos.Add(GameObject.Find("Bully_Musume - BiaChiqui"));
        cabelos.Add(GameObject.Find("FemaleHair2 - BiaChiqui"));
        cabelos.Add(GameObject.Find("MusicHair5 - BiaChiqui"));
        cabelos.Add(GameObject.Find("OsanaShortHair - BiaChiqui"));

        myListManager.Add("Blusa Amarrada na Sintura", acessorios[0]);

        myListManager.TryGetValue("Blusa Amarrada na Sintura", out GameObject blusa);
        blusa.GetComponent<SkinnedMeshRenderer>().material = dadosPer.alunos.material[menu.indexBlusa];

        blusa.SetActive(!menu.actvBlusa);

        for (int i = 0; i < cabelos.Count; i++)
            cabelos[i].SetActive(false);

        cabelos[menu.indexCanelo].SetActive(!menu.actvCabelo);
    }
}
