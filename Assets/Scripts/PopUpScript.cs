using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpScript : MonoBehaviour
{
    [SerializeField] DataLinker dataLinker;
    [SerializeField] GameObject doubleJumpUnlocked;
    [SerializeField] GameObject dashUnlocked;


    public void ShowDoubleJumpPopUp()
    {
        Instantiate(doubleJumpUnlocked, gameObject.transform);
    }

    public void ShowDashPopUp()
    {
        Instantiate(dashUnlocked, gameObject.transform);
    }
}
