using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class MainMenu : MonoBehaviour
{
    
    
    public Text topScoreValue;
    public GameObject topScoreMenu;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
         Application.Quit(); 
    }

    public void ShowTopScore()
    {
        topScoreMenu.SetActive(true);
        
        string path = Application.persistentDataPath + "/ScoreDatabase.txt";

        string temp = "";

        if(File.Exists(path))
        {
        
            List<int> score_list = new List<int>();

            StreamReader reader = new StreamReader(path);
            
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                int x = Int16.Parse(line);
                //Debug.Log(x);
                score_list.Add(x);
                
            }

            reader.Close();

            score_list.Sort();
            score_list.Reverse();

            if(score_list.Count < 10)
            {
                for(int i=0; i<score_list.Count; i++)
                {
                    temp += (i+1).ToString() + ". " + score_list[i].ToString() + "\t";
                }
            }

            else
            {
                for(int i=0; i<10; i++)
                {
                    temp += (i+1).ToString() + ". " + score_list[i].ToString() + "\t";
                }
            }
        }

        topScoreValue.text = temp;
    

    }

    public void CloseTopScore()
    {
        topScoreMenu.SetActive(false);
    }

}
