using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    class Rifle : WeaponScript
    {
        Vector3 magOriginalPosition;
        [SerializeField] GameObject mag;
        public override bool AddAmmo(int amount)
        {
            if (CurrentAmmoCount >= MaxAmmoCount)
                return false;
            if (CurrentAmmoCount + amount > MaxAmmoCount){
                CurrentAmmoCount = MaxAmmoCount;
                return true;
            }
            CurrentAmmoCount += amount;
            return true;
        }

        public override void Draw()
        {
            gameObject.layer = 9;
            mag.transform.position = magOriginalPosition;
        }

        public override void FillMag()
        {
            if(CurrentMag == 0){
                if(CurrentAmmoCount <= MaxMag){
                    CurrentMag = CurrentAmmoCount;
                    CurrentAmmoCount = 0;
                }
                else{
                    CurrentMag = MaxMag;
                    CurrentAmmoCount -= MaxMag;
                }
            }
            else{
                if(CurrentAmmoCount <= MaxMag - CurrentMag){
                    CurrentMag = CurrentAmmoCount + CurrentMag;
                    CurrentAmmoCount = 0;
                }
                else{
                    CurrentAmmoCount -= MaxMag - CurrentMag;
                    CurrentMag = MaxMag;
                }
            }
            StopReload();
        }

        public override void Shoot()
        {
            TimeSinceFire = 0;
            FireSource.Play();
            CurrentMag--;
            Ray ray = Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "zombie")
                    Debug.Log("Hit");
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

        public override void SetReload()
        {
            ReloadSource.Play();
            Controller.SetBool("isReloading", true);
            IsReloading = true;
        }

        public void Start()
        {
            TimeSinceFire = FireRate;
            CurrentMag = MaxMag;
            CurrentAmmoCount = MaxAmmoCount;
            magOriginalPosition = mag.transform.position;
        }

        public void Update()
        {
            
            TimeSinceFire += Time.deltaTime * 1000;
                if (Input.GetMouseButton(0) && TimeSinceFire >= FireRate && !IsReloading){
                    if(CurrentMag > 0)
                        Shoot();
                }
                if((Input.GetKeyDown(KeyCode.R) || CurrentMag <= 0) && CurrentMag < MaxMag && CurrentAmmoCount > 0 && !IsReloading){
                    SetReload();
                }
                UpdateAmmoVisual();
        }

        public override void UpdateAmmoVisual()
        {
                MagCountText.text = CurrentMag.ToString();
                AmmoCountText.text = "/" + CurrentAmmoCount;
        }
    }
}
