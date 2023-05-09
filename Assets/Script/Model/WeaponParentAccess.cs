using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class WeaponParentAccess : MonoBehaviour
{
    [SerializeField]
private GameObject weaponParent;

public void EquipWeapon(EquipableItemSO weaponItemSO, List<ItemParameter> itemState)
{
    AgentWeapon agentWeapon = weaponParent.GetComponentInChildren<AgentWeapon>();
    if (agentWeapon != null)
    {
        agentWeapon.SetWeapon(weaponItemSO, itemState);
    }
}
}
