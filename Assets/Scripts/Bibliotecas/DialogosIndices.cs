using TMPro;

namespace DialogosIndices 
{
    public static class DialogueEscolhas
    {
        public static void Apesentacao(DialogueTrigger dtg, int indice, int num) =>
            dtg.StartDialogue(indice, num);

        public static void IntroduceYourself(TMP_Text txt, int indice) 
        {
            string mensagem = (indice == 0) ? "Apresentações" : "Text 2";
            txt.text = mensagem;
        }
    }
}
