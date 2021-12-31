using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    #region Variables
    [Header ("Selected Weapon")]
    [SerializeField] private int selectedWeapon = 0;

    public bool isHolstered { get; private set; }

    private Gun gunScript;
    private GameObject gameObjectCurrentWeapon;
    #endregion

    void Awake()
    {
        SelectWeapon();
    }

    void Update()
    {
        if (!isHolstered)
        { 
            int previousSelectedWeapon = selectedWeapon;
            HandleInput();

            if (previousSelectedWeapon != selectedWeapon)
                SelectWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            HolsterWeapon(selectedWeapon);
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            { 
                weapon.gameObject.SetActive(true);
                gunScript = weapon.gameObject.GetComponent<Gun>();
                gameObjectCurrentWeapon = weapon.gameObject;
            }
                
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    private void HandleInput()
    {
        // Scrollwheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // Up
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // Down
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        // Numberkeys
        if (Input.GetKeyDown(KeyCode.Alpha1))
            selectedWeapon = 0;

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            selectedWeapon = 1;

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
            selectedWeapon = 2;

        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
            selectedWeapon = 3;
    }

    private void HolsterWeapon(int currentWeapon)
    {
        if (isHolstered)
        {
            isHolstered = false;
            gunScript.enabled = true;
            gameObjectCurrentWeapon.SetActive(true);
        }

        else if (!isHolstered)
        {
            isHolstered = true;
            gunScript.enabled = false;
            gameObjectCurrentWeapon.SetActive(false);
        }
    }
}
