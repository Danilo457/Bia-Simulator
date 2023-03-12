using UnityEngine;

namespace UnityChan
{
	public class SpringBone : MonoBehaviour
	{
		// Próximo osso na hierarquia
		public Transform child;

		// Direção do osso
		public Vector3 boneAxis = new Vector3(-1.0f, 0.0f, 0.0f);
		public float radius = 0.05f;

		// Usa as configurações de força de rigidez e arrasto definidas para cada SpringBone?
		public bool isUseEachBoneForceSettings = false;

		// Força de rigidez da mola
		public float stiffnessForce = 0.01f;

		// Força de arrasto
		public float dragForce = 0.4f;
		public Vector3 springForce = new Vector3(0.0f, -0.0001f, 0.0f);
		public SpringCollider[] colliders;

		// Para fins de debug
		public bool debug = true;

		// Limite para ativar a relação ativa
		public float threshold = 0.01f;

		private float springLength;
		private Quaternion localRotation;
		private Transform trs;
		private Vector3 currTipPos;
		private Vector3 prevTipPos;
		private Transform org;
		private SpringManager managerRef;

		// Configurações iniciais
		private void Awake()
		{
			trs = transform;
			localRotation = transform.localRotation;
			// Referência para o componente SpringManager do UnityChan
			managerRef = GetParentSpringManager(transform);
		}

		// Retorna o componente SpringManager do pai do objeto especificado
		private SpringManager GetParentSpringManager(Transform t)
		{
			var springManager = t.GetComponent<SpringManager>();

			if (springManager != null)
				return springManager;

			if (t.parent != null)
				return GetParentSpringManager(t.parent);

			return null;
		}

		// Configurações iniciais
		private void Start()
		{
			springLength = Vector3.Distance(trs.position, child.position);
			currTipPos = child.position;
			prevTipPos = child.position;
		}

		// Atualiza o estado da mola
		public void UpdateSpring() // Localizado dentro do, "LateUpdate"
		{
			//Kobayashi
			org = trs;
			//resetar a rotação
			trs.localRotation = Quaternion.identity * localRotation;

			//sqrDt significa deltaTime ao quadrado
			float sqrDt = Time.deltaTime * Time.deltaTime;

			//calcular a força de rigidez
			Vector3 force = trs.rotation * (boneAxis * stiffnessForce) / sqrDt;

			//calcular a força de arrasto
			force += (prevTipPos - currTipPos) * dragForce / sqrDt;

			//adicionar a força de mola
			force += springForce / sqrDt;

			//salvar a posição anterior da ponta do osso
			Vector3 temp = currTipPos;

			//verlet integration
			currTipPos = (currTipPos - prevTipPos) + currTipPos + (force * sqrDt);

			//corrigir o comprimento da mola
			currTipPos = ((currTipPos - trs.position).normalized * springLength) + trs.position;

			//verificar se a ponta do osso colide com algum objeto
			for (int i = 0; i < colliders.Length; i++)
			{
				if (Vector3.Distance(currTipPos, colliders[i].transform.position) <= (radius + colliders[i].radius))
				{
					Vector3 normal = (currTipPos - colliders[i].transform.position).normalized;
					currTipPos = colliders[i].transform.position + (normal * (radius + colliders[i].radius));
					currTipPos = ((currTipPos - trs.position).normalized * springLength) + trs.position;
				}
			}

			prevTipPos = temp;

			//aplicar a rotação
			Vector3 aimVector = trs.TransformDirection(boneAxis);
			Quaternion aimRotation = Quaternion.FromToRotation(aimVector, currTipPos - trs.position);
			Quaternion secondaryRotation = aimRotation * trs.rotation;
			trs.rotation = Quaternion.Lerp(org.rotation, secondaryRotation, managerRef.dynamicRatio);
		}

		/*  A função "OnDrawGizmos" é chamada para desenhar um Gizmo amarelo na posição   *
		 *  atual da ponta do osso com mola, para fins de depuração.                      */
		private void OnDrawGizmos()
		{
			if (debug)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawWireSphere(currTipPos, radius);
			}
		}
	}
}
