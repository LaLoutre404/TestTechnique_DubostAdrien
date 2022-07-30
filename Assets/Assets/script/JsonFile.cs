using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;

public class JsonFile : MonoBehaviour
{
    #region Variables
        public string filePath1;
        public string filePath2;
        public string file1Name;
        public string file2Name;
        protected string JsonString1;
        protected string JsonString2;
        public List<JsonContent> CONTENT1; 
        public List<JsonContent> CONTENT2;
    #endregion

    public static JsonFile Instance; 
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this; 
        }
        else
        {
            Destroy(Instance); 
        }
    }

    private void Start()
    {
        // Create List of JsonContent
        CONTENT1 = new List<JsonContent>();
        CONTENT2 = new List<JsonContent>();

        //Set the path of Json file
        filePath1 = Application.streamingAssetsPath + file1Name;
        filePath2 = Application.streamingAssetsPath + file2Name;
        
        //init Reeader
        StreamReader reader = new StreamReader(filePath1);
        StreamReader reader2 = new StreamReader(filePath2);
        
        //Read Json files
        string file1;
        while ((file1 = reader.ReadLine()) != null)
        {

            JsonString1 += file1;
        }
        string file2;
        while ((file2 = reader2.ReadLine()) != null)
        {
            JsonString2 += file2;
        }

        //Deserialize Json files in a List of Json Content
        CONTENT1 = JsonConvert.DeserializeObject<List<JsonContent>>(JsonString1);
        CONTENT2 = JsonConvert.DeserializeObject<List<JsonContent>>(JsonString2);
    }
}
public class JsonContent : MonoBehaviour
{
    public int Id;
    public string Title;
    public string Content;
}
