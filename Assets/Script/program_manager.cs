using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class program_manager : MonoBehaviour
{
    public int pageIndex;
    public GameObject[] Pages;
    public int what_fortune;
    

    public void MovePage(string name)
    {
        Pages[pageIndex].SetActive(false);
        if (name == "btn_back")
        {
            pageIndex--;
        }else if(name=="btn_retry"){
            
            SceneManager.LoadScene(0);
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
