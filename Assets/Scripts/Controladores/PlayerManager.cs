using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] ScriptableBancoDeDados bancoDados;

    Menu menu;

    List<GameObject> cabelos = new List<GameObject>();
    List<GameObject> acessorios = new List<GameObject>();

    public Dictionary<string, GameObject> myListManager = new Dictionary<string, GameObject>();
    public Dictionary<string, AudioClip> myListAudios = new Dictionary<string, AudioClip>();

    List<Mesh> saveMesh = new List<Mesh>(); // Mesh Corpo
    List<Material> saveMaterialBlusa = new List<Material>(); // Assesorio
    List<Material> saveMaterialCorpo = new List<Material>(); // Materia Corpo

    Mesh MeshPlayer(int num) { return saveMesh[num]; }
    Material MaterialBlusa(int num) { return saveMaterialBlusa[num]; }
    Material MaterialCorpo(int num) { return saveMaterialCorpo[num]; }

    void Awake()
    {
        menu = FindObjectOfType<Menu>();

        myListAudios.Add("Tranca do Armario", bancoDados.audio[0]);

        // salvar informações dos materiais da blusa
        List<int> blusaIndices = new List<int> { 17, 18, 19, 20, 21 };
        saveMaterialBlusa.AddRange(blusaIndices.Select(i => bancoDados.material[i]));

        // salvar informações dos meshes
        List<int> meshIndices = new List<int> { 0, 3 };
        saveMesh.AddRange(meshIndices.Select(i => bancoDados.mesh[i]));

        // salvar informações dos materiais do corpo
        List<int> corpoIndices = new List<int> { 3, 4, 22, 5 };
        saveMaterialCorpo.AddRange(corpoIndices.Select(i => bancoDados.material[i]));
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
        blusa.GetComponent<SkinnedMeshRenderer>().material = MaterialBlusa(menu.indexBlusa);

        blusa.SetActive(!menu.actvBlusa);

        for (int i = 0; i < cabelos.Count; i++)
            cabelos[i].SetActive(false);

        cabelos[menu.indexCanelo].SetActive(!menu.actvCabelo);

        int ceira = 0;

        for (int i = 0; i < 2; i++)
            ceira = menu.indexUniforme % 2;

        ModeloEscolha(menu.indexUniforme, menu.indexMesh, ceira);
    }

    void ModeloEscolha(int index, int indexMesh, int local)
    {
        GameObject.Find("Corpo - Player").GetComponent<SkinnedMeshRenderer>().sharedMesh =
            MeshPlayer(indexMesh);

        GameObject.Find("Corpo - Player").
            GetComponent<SkinnedMeshRenderer>().materials[local].shader = MaterialCorpo(index).shader;

        GameObject.Find("Corpo - Player").
            GetComponent<SkinnedMeshRenderer>().materials[local].mainTexture = MaterialCorpo(index).mainTexture;
    }
}
