using UnityEngine;
using System.Collections;

public class DescriptionScript : MonoBehaviour {

    public string sceneName;
    public GameObject descriptionfield;
    public string description;
	
	// Update is called once per frame
	public void setDescription() {
        descriptionfield.GetComponent<UIMessage>().messageText = description;
	}
}
