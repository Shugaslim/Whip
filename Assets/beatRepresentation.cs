using UnityEngine;
using System.Collections;

public class beatRepresentation : MonoBehaviour {

	private GameObject boom;
	private SpriteRenderer bam;
	float bps = (float)67.5/60;



	// Use this for initialization
	void Start () {
		boom = this.gameObject;
		bam = boom.GetComponent<SpriteRenderer>();
		bam.color = Color.white;
		InvokeRepeating("tileColor", (float)1.0, (1/bps));
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void tileColor()
	{
		if(bam.color == Color.white)
			bam.color = Color.red;
		else
			bam.color = Color.white;
	}

	IEnumerator TileChange()
	{
		yield return new WaitForSeconds(1);
		tileColor();
	}
}
