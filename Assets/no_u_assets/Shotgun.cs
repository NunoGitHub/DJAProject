using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponScript
{
    Vector3 shellOriginalPostion;
    [SerializeField] GameObject shell;
    Vector3 gripOriginalPosition;
    [SerializeField] GameObject grip;
    public override bool AddAmmo(int amount)
    {
        if (CurrentAmmoCount >= MaxAmmoCount)
            return false;
        if (CurrentAmmoCount + amount > MaxAmmoCount)
        {
            CurrentAmmoCount = MaxAmmoCount;
            return true;
        }
        CurrentAmmoCount += amount;
        return true;
    }

    public override void Draw()
    {
        gameObject.layer = 9;
        shell.transform.position = shellOriginalPostion;
        grip.transform.position = gripOriginalPosition;
    }

    public override void FillMag()
    {
        if (CurrentAmmoCount > 0 && CurrentMag < MaxMag){
            CurrentAmmoCount--;
            CurrentMag++;
        }
        if (CurrentMag == MaxMag)
            StopReload();
    }

    public override void SetReload()
    {
        Controller.SetBool("isReloading", true);
        IsReloading = true;
    }

    public void PlayReloadSound(){
        ReloadSource.Play();
    }

    public override void Shoot()
    {
        TimeSinceFire = 0;
        FireSource.Play();
        CurrentMag--;
        for (int i = 0; i < ProjectileCount; i++)
        {
            float x = Random.Range(0.4f, 0.6f);
            float y = Random.Range(0.4f, 0.6f);
            Ray ray = Camera.ViewportPointToRay(new Vector3(x, y, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {                    
                if (hit.transform.tag == "zombie")
                    zombieHealth.DealDamage("Shotgun", hit.transform.gameObject, Vector3.Distance(ray.origin, hit.transform.position));
            }
        }
    }

    public override void StopReload()
    {
        IsReloading = false;
        Controller.SetBool("isReloading", false);
    }

    public override void Stow()
    {
        gameObject.layer = 10;
        StopReload();
    }

    public override void UpdateAmmoVisual()
    {
            MagCountText.text = CurrentMag.ToString();
            AmmoCountText.text = "/" + CurrentAmmoCount;
    }

    private void Start()
    {
        TimeSinceFire = FireRate;
        CurrentMag = MaxMag;
        CurrentAmmoCount = MaxAmmoCount;
        shellOriginalPostion = shell.transform.position;
        gripOriginalPosition = grip.transform.position;
    }

    public void Update()
    {
        TimeSinceFire += Time.deltaTime * 1000;
            if (Input.GetMouseButton(0) && TimeSinceFire >= FireRate && !IsReloading)
            {
            if (CurrentMag > 0)
            {
                Shoot();
            }
            }
            if ((Input.GetKeyDown(KeyCode.R) || CurrentMag <= 0) && CurrentMag < MaxMag && CurrentAmmoCount > 0 && !IsReloading)
            {
                SetReload();
            }
            UpdateAmmoVisual();
    }
}
