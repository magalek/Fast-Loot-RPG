using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour {
    [SerializeField] private RenderTexture renderTexture;
    
    // Start is called before the first frame update
    void Start() {
        GetComponent<Camera>().targetTexture = renderTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
