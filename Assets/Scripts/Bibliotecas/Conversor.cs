using System.Collections.Generic;

namespace Conversor
{
    public static class Valores
    {
        public static int index;

        static readonly Dictionary<int, int> IndiceBlusaAmarradaSintura = new Dictionary<int, int>()
        { /* Array do Local dos Materiais "Blusa Amarrada na Sintura" */
            {2, 20}, {3, 18}
        };

        static int[] indiceMap = { 6, 15, 9, 10, 12 
        }; // Index do Local do Array dos Materiais para cor dos Olhos as Iris
        static int[] uniformeMap = { 4, 6, 5, 7 
        }; // Materiais para os Tipos de NPCs Morenos
        static int[] uniformeOliviaMap = { 2, 29, 30, 31 
        };

        public static void IndiceIris(int indice)
        { /* Coleta o indice e Adiciona o Valor do indiceMap no index */
            if (indice >= 0 && indice < indiceMap.Length)
                index = indiceMap[indice];
        }

        public static void IndiceCorpoMoreno(int indice)
        {
            if (indice >= 0 && indice < uniformeMap.Length)
                index = uniformeMap[indice];
        }

        public static int IndiceBlusaCores(int indice)
        { /* Percorrer o Indice e Adicionar o Local do Array Cor */
            if (IndiceBlusaAmarradaSintura.TryGetValue(indice, out int value))
                return value;

            return 0;
        }

        public static void IndiceOlivia(int indice)
        {
            if (indice >= 0 && indice < uniformeOliviaMap.Length)
                index = uniformeOliviaMap[indice];
        }
    }
}
