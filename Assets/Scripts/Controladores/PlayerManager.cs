using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] ScriptableBancoDeDados bancoDados;

    Menu menu;

    List<GameObject> cabelos = new List<GameObject>();
    List<GameObject> acessorios = new List<GameObject>();

    public Dictionary<string, GameObject> myListManager = new Dictionary<string, GameObject>();
    public Dictionary<string, AudioClip> myListAudios = new Dictionary<string, AudioClip>();

    List<Mesh> saveMesh = new List<Mesh>();
    List<Material> saveMateralBlusa = new List<Material>();

    Mesh MeshPlayer(int num) { return saveMesh[num]; }
    Material MaterialBlusa(int num) { return saveMateralBlusa[num]; }

    void Awake()
    {
        menu = FindObjectOfType<Menu>();

        myListAudios.Add("Tranca do Armario", bancoDados.audio[0]);

        saveMateralBlusa.Add(bancoDados.material[17]);
        saveMateralBlusa.Add(bancoDados.material[18]);
        saveMateralBlusa.Add(bancoDados.material[19]);
        saveMateralBlusa.Add(bancoDados.material[20]);
        saveMateralBlusa.Add(bancoDados.material[21]);

        saveMesh.Add(bancoDados.mesh[0]);
        saveMesh.Add(bancoDados.mesh[3]);
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
        blusa.GetComponent<SkinnedMeshRenderer>().material = MaterialBlusa(menu.indexBlusa); // bancoDados.material[menu.indexBlusa];

        blusa.SetActive(!menu.actvBlusa);

        for (int i = 0; i < cabelos.Count; i++)
            cabelos[i].SetActive(false);

        cabelos[menu.indexCanelo].SetActive(!menu.actvCabelo);

        ModeloEscolha(menu.index);
    }
    
    void ModeloEscolha(int num)
    {
        GameObject.Find(bancoDados.namesHierarchy.nameCorpoNemesis[2]).GetComponent<SkinnedMeshRenderer>().sharedMesh =
            MeshPlayer(num);

        if (num == 0) {
            GameObject.Find(bancoDados.namesHierarchy.nameCorpoNemesis[2]).
                GetComponent<SkinnedMeshRenderer>().materials[0].shader = bancoDados.material[3].shader;

            GameObject.Find(bancoDados.namesHierarchy.nameCorpoNemesis[2]).
                GetComponent<SkinnedMeshRenderer>().materials[0].mainTexture = bancoDados.material[3].mainTexture;

        }else {
            GameObject.Find(bancoDados.namesHierarchy.nameCorpoNemesis[2]).
                GetComponent<SkinnedMeshRenderer>().materials[1].shader = bancoDados.material[4].shader;

            GameObject.Find(bancoDados.namesHierarchy.nameCorpoNemesis[2]).
                GetComponent<SkinnedMeshRenderer>().materials[1].mainTexture = bancoDados.material[4].mainTexture;
        }
    }
}
