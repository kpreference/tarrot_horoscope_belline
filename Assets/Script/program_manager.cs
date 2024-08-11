using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class program_manager : MonoBehaviour
{
    public int pageIndex;
    public GameObject[] Pages;
    int what_fortune;
    

    public void MovePage(string name)
    {
        Debug.Log(pageIndex);
        Debug.Log(name);
        //pageIndex = 0;
        //Debug.Log(Pages[pageIndex]);
        Pages[pageIndex].SetActive(false);

        if (name == "btn_back")
        {
            if (pageIndex == 3)
            {
                pageIndex -= 2;
            }
            else { pageIndex--; }
        }
        else//여기서 종류 정하면 될듯
        {
            if (name == "btn_couple")
            {
                what_fortune=0;
            }else if (name == "btn_solo")
            {
                what_fortune=1;
            }else if (name == "btn_today")
            {
                what_fortune=2;
            }
            pageIndex++;
        }
        
        Pages[pageIndex].SetActive(true);

    }
    
}
