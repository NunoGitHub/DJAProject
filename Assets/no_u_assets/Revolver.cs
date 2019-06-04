using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class Revolver : WeaponScript
    {
        Quaternion cylinderOriginalRotation;
        Vector3 cylinderOriginalPosition;
        [SerializeField] GameObject cylinder;
        [SerializeField] RevolverController revController;
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
            cylinder.transform.position = cylinderOriginalPosition;
            cylinder.transform.rotation = cylinderOriginalRotation;
        }

        public override void FillMag()
        {
            if (CurrentMag == 0){
                if (CurrentAmmoCount <= MaxMag){
                    CurrentMag = CurrentAmmoCount;
                    CurrentAmmoCount = 0;
                }
                else{
                    CurrentMag = MaxMag;
                    CurrentAmmoCount -= MaxMag;
                }
            }
            else{
                if (CurrentAmmoCount <= MaxMag - CurrentMag){
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
            revController.RotateCyl();
            FireSource.Play();
            CurrentMag--;
            Ray ray = Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "zombie")
                    zombieHealth.DealDamage("Revolver", hit.transform.gameObject, Vector3.Distance(ray.origin, hit.transform.position));
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

        private void Start()
        {
            TimeSinceFire = FireRate;
            CurrentMag = MaxMag;
            CurrentAmmoCount = MaxAmmoCount;
            cylinderOriginalPosition = cylinder.transform.position;
            cylinderOriginalRotation = cylinder.transform.rotation;
        }

        public void Update()
        {
            TimeSinceFire += Time.deltaTime * 1000;
                if (Input.GetMouseButton(0) && TimeSinceFire >= FireRate && !IsReloading){
                    if (CurrentMag > 0)
                        Shoot();
                }
                if ((Input.GetKeyDown(KeyCode.R) || CurrentMag <= 0) && CurrentMag < MaxMag && CurrentAmmoCount > 0 && !IsReloading){
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
