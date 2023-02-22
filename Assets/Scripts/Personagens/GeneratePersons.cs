using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GeneratePersons
{
    Estudantes estudantes;
    Menu menu;

    Dictionary<string, int> personagensIndices = new Dictionary<string, int>
    { /* List Global de Todos os Personagens que ta no Game */
        { "Amai Odayaka"   , 0 },
        { "Alícia"         , 1 },
        { "Carolina"       , 2 },
        { "Alana"          , 3 }
    };

    Dictionary<string, int> personagensMorenosFace = new Dictionary<string, int>
    { /* List de Todos os Personagens Morenos */
        { "Carolina"       , 1 },
        { "Alana"          , 2 }
    };

    Dictionary<int, int> uniformeMap = new Dictionary<int, int>()
    {
        { 0, 4 }, { 1, 6 }, { 2, 5 }, { 3, 7 }
    };

    public int indexMesh; // Quantidade de Mesh

    int modelosIndex; // Quantidade de Modelos
    int saiaIndex;

    public int IndexModelo { // Quantidade de Modelos
        get { return modelosIndex; }
        set { modelosIndex = value; }
    }
    public int IndexSaia {
        get { return saiaIndex; }
        set { saiaIndex = value; }
    }

    public List<Transform> spamPosition = new List<Transform>();

    public List<Mesh> saveMeshAvatar = new List<Mesh>();
    public List<Material> saveMaterialCorpo = new List<Material>();
    public List<Material> saveMaterialFace = new List<Material>();

    public Mesh AvatarMesh(int num) { return saveMeshAvatar[num]; }
    public Material AvatarMaterialCorpo(int num) { return saveMaterialCorpo[num]; }
    public Material AvatarMaterialFace(int num) { return saveMaterialFace[num]; }

    public void AddSavesLists(ScriptableBancoDeDados bancoDados)
    { /* Adiciona do Banco de dados para algumas List para seram Ultilizadas */
        List<int> meshIndices = new List<int> { 0, 3 };
        saveMeshAvatar.AddRange(meshIndices.Select(i => bancoDados.mesh[i]));

        List<int> corpoIndices = new List<int> { 3, 4, 22, 5, 0, 23, 24, 25 };
        saveMaterialCorpo.AddRange(corpoIndices.Select(i => bancoDados.material[i]));

        List<int> faceIndices = new List<int> { 6, 9, 10 };
        saveMaterialFace.AddRange(faceIndices.Select(i => bancoDados.material[i]));
    }

    void MeshAvatar(ScriptableBancoDeDados bancoDados, string name) // Mesh dos NPCs
    {
        bancoDados.components.AvatarCuston("CorpoNemesis - " + name).sharedMesh =
            AvatarMesh(indexMesh);
    }

    public void SpamSalaDosArmarios(ScriptableBancoDeDados bancoDados)
    {
        menu = Object.FindObjectOfType<Menu>(); // Referencia do Script - Menu

        for (int i = 0; i < bancoDados.listNames.Count; i++) // Percorrer Toda a List de Nomes
        {
            string nome = bancoDados.listNames[i]; // Coletando 1 nome por Ves ate o fim da List

            if (personagensIndices.ContainsKey(nome)) // Coleta so os Nomes Atribuidos ao Dictionary
                SpawnAvatar(nome, bancoDados); // Instanciar só os Encontrados
        }
    }

    private void SpawnAvatar(string nomePersonagem, ScriptableBancoDeDados bancoDados)
    { /* Sistema que Spawn todos os NPCs do Game "Scene dos Armários" */
        int indice = personagensIndices[nomePersonagem]; // Verifica dodos os nomes que ta no Dictionary e Coleta o Index
        Object obj = Object.Instantiate(bancoDados.avatar, spamPosition[0].position, spamPosition[0].rotation);
        obj.name = nomePersonagem; // Atribui o Nome para o Avatar

        RenomeCorpo(obj.name); // Adiciona um Nome do NPC Junto ao Corpo para depois Procurar
        RenomeCabelo(obj.name); // Adiciona um Nome do NPC Junto ao Local que ficara o Cabelo para depois Procurar
        MeshAvatar(bancoDados, obj.name); // Procura o nome do NPC Para Adicionar uma Mesh a ele
        MaterialCorAvatar(bancoDados, obj.name); // Procura o nome do NPC Para Adicionar os Materias a ele
        AddCobelos(bancoDados, indice, obj.name); // Procura o nome do NPC Para Adicionar um Cabelo
        Olhos(bancoDados, obj.name, indice); // Procura o nome do NPC para Adicionar as Mesh e os Materias nos 2 Olhos
        AddComponents(bancoDados, obj.name); // Procura o nome e Aciona todos os Componentes ao NPC
    }

    void MaterialCorAvatar(ScriptableBancoDeDados bancoDados, string name)
    {
        if (personagensMorenosFace.ContainsKey(name)) // Procurar os Nomes dos NPCs Morenos
        {
            int num = menu.indexUniforme; // Coleta o Valor do Index

            if (uniformeMap.ContainsKey(num)) // Procura cada Valor
                num = uniformeMap[num]; // Troca o Valor do Index pelo Valor do Dictionary

            Material materialCorpo = AvatarMaterialCorpo(num);
            Material materialFace = AvatarMaterialFace(personagensMorenosFace[name]);
            SetMaterialProperties(bancoDados, name, materialCorpo, materialFace);
        }
        else
        { /* Sistema Responsavel por alterar o Tipo do Material de acondo com o Index "NPCs Padão" */
            Material materialCorpo = AvatarMaterialCorpo(menu.indexUniforme);
            Material materialFace = AvatarMaterialFace(0);
            SetMaterialProperties(bancoDados, name, materialCorpo, materialFace);
        }
    }

    void SetMaterialProperties(ScriptableBancoDeDados bancoDados, string name, Material materialCorpo, Material materialFace)
    { /* Sistema de Atrimuição os Materiais de Cada Tipo De NPC "Altomatico" */
        Shader shaderCorpo = materialCorpo.shader;
        Shader shaderFace = materialFace.shader;
        Texture textureCorpo = materialCorpo.mainTexture;
        bancoDados.components.AvatarCuston("CorpoNemesis - " + name).materials
            .Take(2).ToList().ForEach(m =>
            {
                m.shader = shaderCorpo;
                m.mainTexture = textureCorpo;
            });
        bancoDados.components.AvatarCuston("CorpoNemesis - " + name).materials
            .ElementAt(2).shader = shaderFace;
        bancoDados.components.AvatarCuston("CorpoNemesis - " + name).materials
            .ElementAt(2).mainTexture = materialFace.mainTexture;
    }
 
    void AddComponents(ScriptableBancoDeDados bancoDados, string name)
    {
        GameObject.Find(name).AddComponent<Estudantes>(); // ADD Script - Estudantes

        estudantes = GameObject.Find(name).GetComponent<Estudantes>();
        estudantes.StartEsts(name); // Seta o Local do NPC a Animação

        estudantes.ListsAnimClips(bancoDados); // Audios dos NPCs
    }

    void AddCobelos(ScriptableBancoDeDados bancoDados, int num, string name)
    { /* Sistema que Procura o mesmo Nome do Local do NPC para Adicionar o Cabelo */
        GameObject obj = Object.Instantiate(bancoDados.cabelos[num]);
        obj.transform.SetParent(GameObject.Find("CabelosPer - " + name).transform, false);
    }

    void Olhos(ScriptableBancoDeDados bancoDados, string name, int num)
    { /* Sistema de Renomear os Olhos Right e Left dos Olhos para Mudar a Cor de Cada um Deles */
        Object obj = GameObject.Find("RightIris - Nemesis");
        obj.name = "RightIris - " + name + num;

        Object obj2 = GameObject.Find("LeftIris - Nemesis");
        obj2.name = "LeftIris - " + name + num;

        bancoDados.addGameObject.AddMesh("RightIris - " + name + num, "LeftIris - " + name + num);

        bancoDados.components.MeshIris("RightIris - " + name + num).mesh = bancoDados.mesh[4];
        bancoDados.components.MeshIris("LeftIris - " + name + num).mesh = bancoDados.mesh[4];

        bancoDados.components.MaterialIris("RightIris - " + name + num).material = bancoDados.material[7];
        bancoDados.components.MaterialIris("LeftIris - " + name + num).material = bancoDados.material[7];
    }

    void RenomeCorpo(string name)
    { /* Sistema onde Renomeia o Corpo onde Ira Add o Mesh e os Materias do NPC */
        Object obj = GameObject.Find("CorpoNemesis - Nemesis");
        obj.name = "CorpoNemesis - " + name;
    }

    void RenomeCabelo(string name)
    { /* Sistema de Renomear o local onde ta o Obj do NPC que Fica o Cabelo Correspontente a Ele(a) */
        Object obj = GameObject.Find("CabelosPer - Nemesis");
        obj.name = "CabelosPer - " + name;
    }
}
