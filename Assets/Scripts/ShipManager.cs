using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SpaceShip
{
    public string shipName;            // A hajó neve
    public List<Sprite> upgrades;     // A hajóhoz tartozó fejlesztések (sprite-ok)
}

public class ShipManager : MonoBehaviour
{
    public static ShipManager Instance;

    [Header("SpaceShip Configuration")]
    public List<SpaceShip> spaceShips;      // Elérhető hajók listája
    public SpaceShip selectedSpaceShip;    // A jelenleg kiválasztott hajó
    private int currentLevel = 1;          // Az aktuális szint száma

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeSpaceships();
        ResetToBaseVersion();
    }

    // Hajók inicializálása
    public void InitializeSpaceships()
    {
        spaceShips = new List<SpaceShip>
        {
            new SpaceShip
            {
                shipName = "RedShip",
                upgrades = new List<Sprite>
                {
                    Resources.Load<Sprite>("playerShip1_red"),
                    Resources.Load<Sprite>("playerShip2_red"),
                    Resources.Load<Sprite>("playerShip3_red")
                }
            },
            new SpaceShip
            {
                shipName = "BlueShip",
                upgrades = new List<Sprite>
                {
                    Resources.Load<Sprite>("playerShip1_blue"),
                    Resources.Load<Sprite>("playerShip2_blue"),
                    Resources.Load<Sprite>("playerShip3_blue")
                }
            },
            new SpaceShip
            {
                shipName = "OrangeShip",
                upgrades = new List<Sprite>
                {
                    Resources.Load<Sprite>("playerShip1_orange"),
                    Resources.Load<Sprite>("playerShip2_orange"),
                    Resources.Load<Sprite>("playerShip3_orange")
                }
            },
            new SpaceShip
            {
                shipName = "GreenShip",
                upgrades = new List<Sprite>
                {
                    Resources.Load<Sprite>("playerShip1_green"),
                    Resources.Load<Sprite>("playerShip2_green"),
                    Resources.Load<Sprite>("playerShip3_green")
                }
            }
        };
    }
    public void ResetToBaseVersion()
    {
        currentLevel = 1; // Visszaállítjuk az alap szintet
    }
    public SpaceShip GetDefaultSpaceship() //Alapértelmezett űrhajó beállítása
    {
        return spaceShips.Find(ship => ship.shipName == "GreenShip");
    }

    // Hajó kiválasztása a név alapján
    public void SelectSpaceShip(string shipName)
    {
        selectedSpaceShip = spaceShips.Find(ship => ship.shipName == shipName);
        if (selectedSpaceShip == null)
        {
            Debug.LogError($"SpaceShip with name {shipName} not found!");
        }
    }

    // Hajó aktuális fejlesztési szintjének sprite-ja
    public Sprite GetUpgradeSprite()
    {
        if (selectedSpaceShip != null && currentLevel > 0 && currentLevel <= selectedSpaceShip.upgrades.Count)
        {
            return selectedSpaceShip.upgrades[currentLevel - 1];
        }

        return GetDefaultSpaceship().upgrades[currentLevel - 1];
    }

    // Szint előreléptetése
    public void AdvanceLevel()
    {
        if ((currentLevel < selectedSpaceShip.upgrades.Count) || (currentLevel < GetDefaultSpaceship().upgrades.Count))
        {
            currentLevel++;
        }
        else
        {
            Debug.LogWarning("Maximum upgrade level reached!");
        }
    }

    // Játékos sprite frissítése
    public void SetPlayerSprite(GameObject player)
    {
        if (player == null)
        {
            Debug.LogWarning("No Player object provided.");
            return;
        }

        if (selectedSpaceShip == null)
        {
            Debug.LogError("No spaceship selected in ShipManager!");
            return;
        }

        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Player object does not have a SpriteRenderer component!");
            return;
        }

        Sprite newSprite = GetUpgradeSprite();
        if (newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
            Debug.Log($"Player sprite updated to: {newSprite.name}");
        }
    }
}
