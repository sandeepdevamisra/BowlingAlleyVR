using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject ball;
    int score = 0;
    string temp_score = "";
    GameObject[] pins;
    public Text scoreUI;
    Vector3[] positions;
    int turnCounter=0;
    //int gameNumber=0;
    
    private AudioSource pop;
    int[] score_arr0 = new int[21] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    int[] score_arr1 = new int[10] {0,0,0,0,0,0,0,0,0,0};
    int prev=0, current=0;


    public GameObject menu;

    void Start()
    {


        ball.GetComponent<Rigidbody>().maxAngularVelocity = (float)(10 * ball.GetComponent<Rigidbody>().maxAngularVelocity);
        pop = GetComponent<AudioSource>();
        pins = GameObject.FindGameObjectsWithTag("Pin");
        positions = new Vector3[pins.Length];

        for(int i=0; i<pins.Length; i++)
        {
            positions[i] = pins[i].transform.position;
        }
    }



    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.name == "Plane" || other.gameObject.name == "Cube")
        {

            
            Debug.Log(turnCounter);
            
            CountPinsDown();
            turnCounter++;

            ResetBall();

            if(turnCounter%2==0)
            {

                //score=0;

                ResetPins();

            }

            if(turnCounter == 21)
            {


                string path = Application.persistentDataPath + "/ScoreDatabase.txt";

                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine(score_arr1[9].ToString());
                writer.Close();
                //total_score = 0;
                SceneManager.LoadScene(0);

            }

        }

    }


    void CountPinsDown()
    {
        
        score=0;
        for(int i=0; i<pins.Length; i++)
        {
            if(pins[i].transform.eulerAngles.z > 5 && pins[i].transform.eulerAngles.z < 355 && pins[i].activeSelf)
            {
                score++;
                pop.Play();
                pins[i].SetActive(false);
                pins[i].transform.position = new Vector3(-6.77f, -3.47f, -5f);

            }
        }

        //prev=0;
        //current=0;
        score_arr0[turnCounter] = score;
        if(turnCounter%2 == 0 && turnCounter < 19)
        {

            if(score == 10)
            {
                turnCounter++;
                current=1;
            }

            else
            {
                current=0;
            }

            if(prev==1 || prev==2)
            {
                score_arr1[(turnCounter-2)/2] += score;
            }

            if(turnCounter>0)
            {
                score_arr1[turnCounter/2] = score_arr1[(turnCounter-2)/2] + score;
            }
            else
            {
                score_arr1[turnCounter/2] = score;
            }
        }
        else if(turnCounter%2 == 1 && turnCounter <= 19)
        {

            if(score_arr0[turnCounter-1] + score == 10)
            {
                current = 2;
            }

            if(prev==1)
            {
                score_arr1[(turnCounter-2)/2] += score;
                score_arr1[turnCounter/2] += score;
            }


            score_arr1[turnCounter/2] += score;
            if(turnCounter==19 && current!=1&&current!=2)
            {
                turnCounter++;
            }

        }

        else if(turnCounter==20)
        {
            score_arr1[9] += score;
        }

        if(turnCounter%2==1)
        {
            prev = current;
        }

        for(int i=0; i<19; i+=2)
        {

            if(i==18)
            {
                temp_score += score_arr0[i].ToString() + "-" + score_arr0[i+1].ToString() + "-" + score_arr0[i+2].ToString() + 
                "-" + score_arr1[i/2].ToString() + " ";
            }
            else
            {
                temp_score += score_arr0[i].ToString() + "-" + score_arr0[i+1].ToString() + "-" + score_arr1[i/2].ToString() + " ";
            }
        }


        scoreUI.text = temp_score;

        temp_score = "";


    }

    void ResetPins()
    {

        for(int i=0; i<pins.Length; i++)
        {

            pins[i].SetActive(true);
            pins[i].transform.position = positions[i];
            pins[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            pins[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            pins[i].transform.rotation = Quaternion.identity;

        }
    }

    void ResetBall()
    {

        ball.transform.position = new Vector3(2.42f, 0.77f, 2.61f);
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        ball.transform.rotation = Quaternion.identity;

    }


}
