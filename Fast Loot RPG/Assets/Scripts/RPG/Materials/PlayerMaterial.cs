using UnityEngine;

namespace RPG.Materials {
    public class PlayerMaterial : MonoBehaviour {

        private Material material;

        public static readonly int BlurAmount = Shader.PropertyToID("_Blur");
        public static readonly int BlurDir = Shader.PropertyToID("_BlurDir");
        
        private void Awake() {
            material = GetComponent<SpriteRenderer>().material;
            // material = new Material(material);
            // GetComponent<SpriteRenderer>().material = material;
        }
        public void Set(int propID, int value) => material.SetInt(propID, value);
        public void Set(int propID, float value) => material.SetFloat(propID, value);
        public void Set(int propID, Vector4 value) => material.SetVector(propID, value);

    }
}