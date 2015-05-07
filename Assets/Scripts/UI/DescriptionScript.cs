using UnityEngine;
using System.Collections;

public class DescriptionScript : MonoBehaviour {

    public GameObject descriptionField;
    public string description;
	
	// Update is called once per frame
	public void setDescription() {
        descriptionField.GetComponent<UIMessage>().messageText = description;
	}
}
