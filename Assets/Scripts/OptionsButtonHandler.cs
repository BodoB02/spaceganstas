using UnityEngine;

public class OptionsButtonHandler : MonoBehaviour
{
    public string shipName; // A gombhoz tartozó űrhajó neve

    public void OnSelectShip()
    {
        if (ShipManager.Instance != null)
        {
            ShipManager.Instance.SelectSpaceShip(shipName);
            Debug.Log($"Selected spaceship: {shipName}");
        }
        else
        {
            Debug.LogError("GameManager instance not found!");
        }
    }
}
