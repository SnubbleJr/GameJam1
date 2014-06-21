using UnityEngine;
using System.Collections;

public class ClonePersonBehaviourScript : MonoBehaviour {
	
	public string personName, character, country;
	public Color color;
	public GameObject PaintableClothes;

	// Use this for initialization
	void Start () {

		PaintableClothes.renderer.material.color = color;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
