using UnityEngine;

//Namespace UnityChan contém todos os scripts para a personagem Unity-chan.
namespace UnityChan
{
    //Script que define um collider de mola.
    public class SpringCollider : MonoBehaviour
    {
        //Variável que define o raio do collider de mola.
        public float radius = 0.5f;

        //Método que desenha o collider no Editor da Unity quando o objeto é selecionado.
        private void OnDrawGizmosSelected()
        {
            //Define a cor verde para o gizmo.
            Gizmos.color = Color.green;
            //Desenha uma esfera sem preenchimento (wireframe) na posição do objeto e com o raio definido pela variável radius.
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
