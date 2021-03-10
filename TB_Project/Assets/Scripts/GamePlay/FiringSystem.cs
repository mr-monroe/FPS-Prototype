using UnityEngine;

public class FiringSystem : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;

    private int weaponsAmount => weapons.Length;
    private int currentWeaponIndex;
    private Weapon currentWeapon => weapons[currentWeaponIndex];
    private bool crouchMode = false;
    private float attackModificateur = 1f;

    public void OnShootPressed() 
    {
        if (!GameObject.ReferenceEquals(weapons[currentWeaponIndex], null)) 
        {
            if (crouchMode) { attackModificateur = 2f; }
            else { attackModificateur = 1f; }

            currentWeapon.Fire(attackModificateur);     
        }
    }

    public void EquipNextWeapon() 
    {
        currentWeaponIndex = (weaponsAmount + currentWeaponIndex + 1) % weaponsAmount;
        SwapWeapon();
    }

    public void EquipPreviousWeapon() 
    {
        currentWeaponIndex = (weaponsAmount + currentWeaponIndex - 1) % weaponsAmount;
        SwapWeapon();
    }

    private void SwapWeapon() 
    {
        foreach (Weapon weapon in weapons) 
        {
            weapon.Unequip();
        }
        weapons[currentWeaponIndex].Equip();
    }

    public void CrouchEffect() 
    {
        crouchMode = !crouchMode;
    }
}
