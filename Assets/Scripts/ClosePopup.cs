using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClosePopup : MonoBehaviour
{
    public GameObject PopUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
     
    }
    public void ButtonClick()
    {
        if (PopUp.activeInHierarchy == true)
            PopUp.SetActive(false);
        else
            PopUp.SetActive(true);
    }
}
