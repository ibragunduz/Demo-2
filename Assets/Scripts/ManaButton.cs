using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaButton : MonoBehaviour {

	public Transform ManaFilled;
	[SerializeField] private float currentAmount;
	[SerializeField] private float speed;
	
	// Update is called once per frame
	void Update () {
		if (currentAmount < 100){
			currentAmount += speed * Time.deltaTime;

		}
		else{

		}
		ManaFilled.GetComponent<Image>().fillAmount = currentAmount / 100;
	}
}
