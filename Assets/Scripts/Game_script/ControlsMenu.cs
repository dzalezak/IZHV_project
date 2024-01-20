using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public GameObject CMenu, MMenu;
    public void Back(){
        CMenu.SetActive(false);
        MMenu.SetActive(true);
    }
}
