using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class program_manager : MonoBehaviour
{
    public int pageIndex;
    public GameObject[] Pages;
    
    

    public void MovePage(string name)
    {
        Debug.Log(pageIndex);
        Debug.Log(name);
        //pageIndex = 0;
        //Debug.Log(Pages[pageIndex]);
        Pages[pageIndex].SetActive(false);

        if (name == "btn_back")
        {
            pageIndex--;
        }
        else//여기서 종류 정하면 될듯
        {
            /*if (name == "btn_solo")
            {

            }else if (name == "btn_couple")
            {

            }else if (name == "btn_today")
            {

            }*/
            pageIndex++;
        }
        
        Pages[pageIndex].SetActive(true);

    }
    
}
