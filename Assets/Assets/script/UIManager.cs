using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class UIManager : MonoBehaviour
{
    #region Variables
        //GameObject
        public GameObject ScrollViewContent;
        public GameObject ResultPanel;
        public List<GameObject> buttons;
        
        //Button
        public Button TemplateButton;
        public Button DataSet1;
        public Button DataSet2; 
        
        //TextMesh
        public TextMeshProUGUI ContentText;

        //integer
        public int GapBetweenButtons; 
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Add Event to the data set buttons
        DataSet1.onClick.AddListener(delegate { FillScrollView(1); });
        DataSet2.onClick.AddListener(delegate { FillScrollView(2); });
        
        //position the scroll view content to 0
        ScrollViewContent.transform.localPosition= new Vector3(0f, 0f, 0f);
        
        //enabled Result Panel
        ResultPanel.SetActive(false); 
    }

    //Fill scroll view with buttons according to JsonFile
    public void FillScrollView(int index)
    {
        ExitContent(); 

        //Select JsonFile according to the data set button
        List<JsonContent> List = SelectJsonFile(index);

        //According to the list elements, create buttons in the scroll view content
        for (int i = 0; i < List.Count; i++)
        {
            //create button
            Button tmpbutton = Instantiate(TemplateButton, ScrollViewContent.transform);
            buttons.Add(tmpbutton.gameObject);

            //Set Onclick of the button
            tmpbutton.onClick.AddListener(delegate { FindContent(tmpbutton, index); });

            //Set Button Text
            TextMeshProUGUI tmptext = tmpbutton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            tmptext.text = JsonFile.Instance.CONTENT1[i].Title;
            
            //Set position of the button
            RectTransform tmpRect = tmpbutton.GetComponent<RectTransform>();
            tmpRect.anchorMin = new Vector2(0.5f, 1); 
            tmpRect.anchorMax = new Vector2(0.5f, 1); 
            tmpRect.pivot = new Vector2(0.5f, 1);
            tmpRect.position -= new Vector3(0f, tmpRect.sizeDelta.y * i * GapBetweenButtons, 0f);
        }

        //active the pnael of result
        ResultPanel.SetActive(true); 
    }

    //Find Content of selected button
    public void FindContent(Button currentButton, int index)
    {
        //Select JsonFile according to the data set button
        List<JsonContent> List = SelectJsonFile(index); 

        //Select the button
        currentButton.Select();

        //Search content by button title
        for (int i = 0; i < buttons.Count; i++)
        {
            //Get the titles of the compared buttons
            TextMeshProUGUI tmptext = buttons[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI tmptextbutton = currentButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

            //If the titles are the same, we display the content of the right list corresponding to the title
            if(tmptext.text == tmptextbutton.text)
            {
                ContentText.text = List[i].Content; 
            }
        }
    }

    //Close the content panel
    public void ExitContent()
    {
        //We clear the list
        buttons.Clear();
        ScrollViewContent.transform.localPosition = new Vector3(0f, 0f, 0f);
        //We destroy all of the gameObject of the scroll view // reset the content text zone // Set the result panel false
        for (int i = 0; i < ScrollViewContent.transform.childCount; i++)
        {
            Destroy(ScrollViewContent.transform.GetChild(i).gameObject);
            ContentText.text = "";
            ResultPanel.SetActive(false); 
        }
    } 

    //Select Json File according to the selected button
    public List<JsonContent> SelectJsonFile(int index)
    {
        List<JsonContent> List = new List<JsonContent>();
        if (index == 1)
        {
            /*DataSet1.interactable = false;
            DataSet2.interactable = false;*/
            return List = JsonFile.Instance.CONTENT1;
        }
        else if (index == 2)
        {
            /*DataSet2.interactable = false;
            DataSet1.interactable = false;*/
            return List = JsonFile.Instance.CONTENT2;
        }
        else
        {
            return null; 
        }
    }
}


