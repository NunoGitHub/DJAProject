using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets
{
    public abstract class WeaponScript : MonoBehaviour
    {
        [SerializeField] float damage;
        [SerializeField] float fireRate;
        [SerializeField] float accuracy;
        [SerializeField] int projectileCount;
        [SerializeField] int currentAmmoCount;
        [SerializeField] int maxAmmoCount;
        [SerializeField] int currentMag;
        [SerializeField] int maxMag;
        [SerializeField] Camera camera;
        [SerializeField] bool isReloading = false;
        [SerializeField] Animator controller;
        [SerializeField] AudioSource fireSource;
        [SerializeField] AudioSource reloadSource;
        [SerializeField] TextMeshProUGUI ammoCountText;
        [SerializeField] TextMeshProUGUI magCountText;
        float timeSinceFire = 0;
        

        public float Damage { get => damage;}
        public float FireRate { get => fireRate;}
        public float Accuracy { get => accuracy;}
        public int ProjectileCount { get => projectileCount;}
        public int CurrentAmmoCount { get => currentAmmoCount; set => currentAmmoCount = value; }
        public int MaxAmmoCount { get => maxAmmoCount;}
        public int CurrentMag { get => currentMag; set => currentMag = value; }
        public int MaxMag { get => maxMag;}
        public Camera Camera { get => camera;}
        public Animator Controller { get => controller; set => controller = value; }
        public bool IsReloading { get => isReloading; set => isReloading = value; }
        public float TimeSinceFire { get => timeSinceFire; set => timeSinceFire = value; }
        public AudioSource FireSource { get => fireSource; set => fireSource = value; }
        public AudioSource ReloadSource { get => reloadSource; set => reloadSource = value; }
        public TextMeshProUGUI AmmoCountText { get => ammoCountText; set => ammoCountText = value; }
        public TextMeshProUGUI MagCountText { get => magCountText; set => magCountText = value; }

        public abstract void Shoot();
        public abstract void Stow();
        public abstract void Draw();
        public abstract bool AddAmmo(int amount);
        public abstract void StopReload();
        public abstract void FillMag();
        public abstract void SetReload();
        public abstract void UpdateAmmoVisual();

    }
}
