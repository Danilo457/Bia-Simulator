using UnityEngine;

namespace UnityChan
{
	public class SpringManager : MonoBehaviour
	{
		// DynamicRatio é um parâmetro para o nível ativado de animação dinâmica
		public float dynamicRatio = 1.0f;

		//Ebata
		public float			stiffnessForce;
		public AnimationCurve	stiffnessCurve;
		public float			dragForce;
		public AnimationCurve	dragCurve;
		public SpringBone[] springBones;

		void Start ()
		{
			UpdateParameters ();
		}
	
		void Update ()
		{
#if UNITY_EDITOR
		//Kobayashi
		if(dynamicRatio >= 1.0f)
			dynamicRatio = 1.0f;
		else if(dynamicRatio <= 0.0f)
			dynamicRatio = 0.0f;
		//Ebata
		UpdateParameters();
#endif
		}

		// Atualiza cada mola apenas se o dynamicRatio não for 0.0f
		private void LateUpdate ()
		{
			if (dynamicRatio != 0.0f) {
				for (int i = 0; i < springBones.Length; i++) {
					if (dynamicRatio > springBones [i].threshold) {
						springBones [i].UpdateSpring ();
					}
				}
			}
		}

		// Atualiza os parâmetros das molas
		private void UpdateParameters ()
		{
			UpdateParameter ("stiffnessForce", stiffnessForce, stiffnessCurve);
			UpdateParameter ("dragForce", dragForce, dragCurve);
		}
	
		private void UpdateParameter (string fieldName, float baseValue, AnimationCurve curve)
		{
			// Atualiza um parâmetro específico de cada mola
			var start = curve.keys [0].time;
			var end = curve.keys [curve.length - 1].time;

			// Obtém a propriedade que será atualizada para cada mola
			var prop = springBones [0].GetType ().GetField (fieldName, System.Reflection.BindingFlags.Instance 
				| System.Reflection.BindingFlags.Public);
		
			for (int i = 0; i < springBones.Length; i++) {
				// Atualiza apenas as molas que não estão usando as configurações individuais de força
				if (!springBones [i].isUseEachBoneForceSettings) {
					var scale = curve.Evaluate (start + (end - start) * i / (springBones.Length - 1));
					prop.SetValue (springBones [i], baseValue * scale);
				}
			}
		}
	}
}
