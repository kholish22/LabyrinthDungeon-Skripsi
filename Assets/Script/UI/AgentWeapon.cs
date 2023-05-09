using System.Collections;
using Inventory.Model;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]
    private EquipableItemSO weapon;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;


    public int currentWeaponIndex;
    public GameObject[] weaponParents;
    public GameObject currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        currentWeaponIndex = 0;
        SetCurrentWeapon();
    }

    // Activate the next weapon in the array
    public void ActivateNextWeapon()
    {
        currentWeaponIndex++;
        if (currentWeaponIndex >= weaponParents.Length)
        {
            currentWeaponIndex = 0;
        }
        SetCurrentWeapon();
    }

    // Set the current weapon based on the currentWeaponIndex
    private void SetCurrentWeapon()
    {
        // Deactivate all weapons
        foreach (GameObject weaponParent in weaponParents)
        {
            weaponParent.SetActive(false);
        }
        
        // Activate the current weapon
        GameObject newWeapon = weaponParents[currentWeaponIndex];
        newWeapon.SetActive(true);
        currentWeapon = newWeapon;
    }



    public void SetWeapon(EquipableItemSO weaponItemSO, List<ItemParameter> itemState)
{
    if (weapon != null)
    {
        inventoryData.AddItem(weapon, 1, itemCurrentState);
    }

    this.weapon = weaponItemSO;
    this.itemCurrentState = itemState == null ? new List<ItemParameter>() : new List<ItemParameter>(itemState);

    // Check if the weapon item is a sword gold
    if (weaponItemSO.itemName == "Sword Gold")
    {
        // Deactivate all weapon parents
        foreach (GameObject weaponParent in weaponParents)
        {
            weaponParent.SetActive(false);
        }

        // Activate the sword gold weapon parent
        GameObject swordGoldWeaponParent = weaponParents[1]; // assuming sword gold is at index 1
        swordGoldWeaponParent.SetActive(true);
        currentWeapon = swordGoldWeaponParent;
        currentWeaponIndex = 1;
    }
    else if (weaponItemSO.itemName == "Sword Stone")
    {
        // Deactivate all weapon parents
        foreach (GameObject weaponParent in weaponParents)
        {
            weaponParent.SetActive(false);
        }

        // Activate the sword stone weapon parent
        GameObject swordStoneWeaponParent = weaponParents[0]; // assuming sword stone is at index 0
        swordStoneWeaponParent.SetActive(true);
        currentWeapon = swordStoneWeaponParent;
        currentWeaponIndex = 0;
    }

    ModifyParameters();
}


    private void ModifyParameters()
    {
        foreach (var parameter in parametersToModify)
        {
            if (itemCurrentState.Contains(parameter))
            {
                int index = itemCurrentState.IndexOf(parameter);
                float newValue = itemCurrentState[index].value + parameter.value;
                itemCurrentState[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    value = newValue
                };
            }
        }
    }
}