using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    [SerializeField] GameObject[] weapon = new GameObject[3];

    private void Start()
    {
        WeaponScript weaponS = weapon[0].GetComponent<WeaponScript>();
        weaponS.Draw();
        weaponS = weapon[1].GetComponent<WeaponScript>();
        weaponS.Draw();
        weaponS.Stow();
        weaponS = weapon[2].GetComponent<WeaponScript>();
        weaponS.Draw();
        weaponS.Stow();
        weapon[0].SetActive(true);
        weapon[0].layer = 9;
        weapon[1].SetActive(false);
        weapon[1].layer = 10;
        weapon[2].SetActive(false);
        weapon[2].layer = 10;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)){
            WeaponScript weaponS = weapon[0].GetComponent<WeaponScript>();
            weaponS.Draw();
            weaponS = weapon[1].GetComponent<WeaponScript>();
            weaponS.Stow();
            weaponS = weapon[2].GetComponent<WeaponScript>();
            weaponS.Stow();
            weapon[0].SetActive(true);
            weapon[0].layer = 9;
            weapon[1].SetActive(false);
            weapon[1].layer = 10;
            weapon[2].SetActive(false);
            weapon[2].layer = 10;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            WeaponScript weaponS = weapon[1].GetComponent<WeaponScript>();
            weaponS.Draw();
            weaponS = weapon[0].GetComponent<WeaponScript>();
            weaponS.Stow();
            weaponS = weapon[2].GetComponent<WeaponScript>();
            weaponS.Stow();
            weapon[1].SetActive(true);
            weapon[1].layer = 9;
            weapon[0].SetActive(false);
            weapon[0].layer = 10;
            weapon[2].SetActive(false);
            weapon[2].layer = 10;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            WeaponScript weaponS = weapon[2].GetComponent<WeaponScript>();
            weaponS.Draw();
            weaponS = weapon[1].GetComponent<WeaponScript>();
            weaponS.Stow();
            weaponS = weapon[0].GetComponent<WeaponScript>();
            weaponS.Stow();
            weapon[2].SetActive(true);
            weapon[2].layer = 9;
            weapon[1].SetActive(false);
            weapon[1].layer = 10;
            weapon[0].SetActive(false);
            weapon[0].layer = 10;
        }
    }
}
