using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersonBehaviour : MonoBehaviour {

	public GameObject man, woman;
	public TextAsset nameFile, charFile, countryFile, countryColourFile;

	string[] names, characteristics, countries;
	List<Color> countryColors;

	// Use this for initialization
	void Start () {
	
		GameObject personManager = GameObject.FindGameObjectWithTag("personManager");

		transform.parent = personManager.transform;

		names = nameFile.text.Split('\n');
		characteristics = charFile.text.Split('!');
		countries = countryFile.text.Split('\n');
		countryColors = ParseColorTable(countryColourFile.text);

		spawnPerson();
	}

	public void spawnPerson()
	{
		
		float rnd = Random.Range(0.0f, 1.0f);
		
		GameObject person;
		
		if (rnd > 0.5f)
		{
			person = Instantiate(man,transform.position,Quaternion.identity) as GameObject;
		}
		else
		{
			person = Instantiate(woman,transform.position,Quaternion.identity) as GameObject;
		}
		
		ClonePersonBehaviourScript personScript = person.GetComponent<ClonePersonBehaviourScript>();
		
		int rnd2 = Mathf.RoundToInt(Random.Range(0, 15));
		
		personScript.personName = names[rnd2];
		
		rnd2 = Mathf.RoundToInt(Random.Range(0, 15));
		
		personScript.character = characteristics[rnd2];
		
		rnd2 = Mathf.RoundToInt(Random.Range(0, 5));
		
		personScript.country = countries[rnd2];
		
		personScript.color = countryColors[rnd2];
	}

	List<Color> ParseColorTable(string aText)
	{
		List<Color> result = new List<Color>();
		
		string[] lines = aText.Split('\n');
		foreach (string L in lines)
		{
			if (L.StartsWith("("))
			{
				// Cut "RGBA(" and split at ")"
				string[] S = L.Substring(1).Split(')');
				
				// Remove all spaces and split the 4 color values
				string[] values = S[0].Replace(" ","").Split(',');

				// Parse the 4 strings into floats and create the color value
				Color col = new Color(float.Parse(values[0]),float.Parse(values[1]),float.Parse(values[2]),0f);

				result.Add(col);
			}
		}
		return result;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
