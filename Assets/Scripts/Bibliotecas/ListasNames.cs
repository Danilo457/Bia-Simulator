using System.Collections.Generic;

namespace ListasNames
{
    public static class NamesList
    {
        /* List Global de Todos os Personagens que ta no Game */
        public static List<Personagem> personagensList = new List<Personagem>()
        {
            new Personagem( "Amai Odayaka" , "ParadaNormal"     , "branco" ),
            new Personagem( "Alícia"       , "ParadaNormal"     , "branco" ),
            new Personagem( "Carolina"     , "ParadaNormal"     , "moreno" ),
            new Personagem( "Alana"        , "ParadaNormal"     , "moreno" ),
            new Personagem( "Olivia"       , "Parada Estolo 02" , "branco" )
        };
    }

    public class Personagem
    {
        public string name { get; set; }
        public string anim { get; set; }
        public string cor  { get; set; }

        public Personagem(string name, string anim, string cor)
        {
            this.name = name;
            this.anim = anim;
            this.cor  = cor;
        }
    }
}
