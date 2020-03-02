using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Magazine; //current number of bullets
    public int maxMagazine; // max number of bullets that can be in the magazine

    public int Reserves; //current reserves
    public int maxReserves; //max number of bullets that can be in the reserves

    public int ammoPerShot; //number of bullets used every time the weapon is fired
    public bool isAutomatic; //is the weapon automatic or not?

    public GameObject bulletPrefab; //bullet to instantiate
    public Transform Spawn;

    void Start()
    {
        Magazine = maxMagazine;
        Reserves = maxReserves;
    }

    public bool hasAmmo()
    {
        //is the current number of bullets greater thanor equal to the amount of ammo used per shot
        return Magazine >= ammoPerShot;
    }

    public void Reload()
    {
        if(Reserves >= maxMagazine)
        {
            int required = maxMagazine - Magazine; //determine the required number of bullets  

            Magazine = maxMagazine; //fill magazine
            Reserves -= maxMagazine; //update reserves
        }
        else //not enough in reserves to fully fill the magazine
        {
            Magazine = Reserves; //load whatever number we have
            Reserves = 0;
        }
    }

    //virtual indicates that this method can be extended in a child class
    public virtual void Fire(Vector3 fireFromPosition) 
    {
        Magazine -= ammoPerShot;
        if(Magazine <= 0)
        {
            Reload();
        }
    }
}
