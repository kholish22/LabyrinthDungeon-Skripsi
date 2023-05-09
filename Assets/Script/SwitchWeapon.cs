using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    int totalWeapons = 1;

    public int currentWeaponIndex;

    public GameObject[] sword;
    public GameObject weaponHolder;
    public GameObject currentWeapon;
    // Start is called before the first frame update
    void Start()
    {
        totalWeapons = weaponHolder.transform.childCount;
        sword = new GameObject[totalWeapons];

        for (int i = 0; i < totalWeapons; i++)
        {
            sword[i] = weaponHolder.transform.GetChild(i).gameObject;
            sword[i].SetActive(false);
        }

        sword[0].SetActive(true);
        currentWeapon = sword[0];
        currentWeaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
