using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Gun : MonoBehaviour
{
    #region Variables
    [Header ("Gun Properties")]
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float impactForce;
    [SerializeField] private float fireRate;
    [SerializeField] private float reloadTime;
    [SerializeField] private int maxAmo;

    [Header ("References")]
    [Space(20)]
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Animator ammoIconAnimator;

    [Header("AudioSources")]
    [Space(10)]
    [SerializeField] AudioSource shotSound;
    [SerializeField] AudioSource reloadSound;


    private float nextTimeToFire;
    private int currentAmo;
    private bool isReloading;
    private Camera fpsCam;
    private Text ammoCounter;
    #endregion

    void Awake()
    {
        currentAmo = maxAmo;
        fpsCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        ammoCounter = GameObject.FindGameObjectWithTag("AmmoCounter").GetComponent<Text>();
    }

    private void OnEnable()
    {
        isReloading = false;
        gunAnimator.SetBool("Reloading", false);
    }

    void Update()
    {
        if (isReloading) 
            return;

        if (currentAmo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        ShowAmmo();
    }

    private void Shoot() 
    {
        muzzleFlash.Play();
        shotSound.Play();
        currentAmo--;
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            HostileNPC npc = hit.transform.GetComponent<HostileNPC>();
            TrainingDummy td = hit.transform.GetComponent<TrainingDummy>();

            if (npc != null)
                npc.TakeDamage(damage);

            if (td != null)
                td.TakeDamage(damage);

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * impactForce);

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            impactGO.GetComponent<ParticleSystem>().Play();
            Destroy(impactGO, 2f);
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        reloadSound.Play();
        gunAnimator.SetBool("Reloading", true);
        ammoIconAnimator.SetBool("isReloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);

        gunAnimator.SetBool("Reloading", false);
        ammoIconAnimator.SetBool("isReloading", false);

        yield return new WaitForSeconds(0.25f);

        currentAmo = maxAmo;
        isReloading = false;
    }

    private void ShowAmmo()
    {
        ammoCounter.text = "" + currentAmo;
    }
}
