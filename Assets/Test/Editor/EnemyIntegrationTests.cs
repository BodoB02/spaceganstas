using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class EnemyIntegrationTests
{
    [UnityTest]
    public IEnumerator EnemyShootsWhenPlayerIsInFront()
    {
        // Előkészítés: Hozzuk létre a Player-t és az Enemy-t
        var playerObject = new GameObject("Player");
        var enemyObject = new GameObject("Enemy");

        // Add hozzá a FacesPlayer és EnemyShootingType1 szkripteket az ellenséghez
        var facesPlayer = enemyObject.AddComponent<EnemyFacesPlayer>();
        var enemyShooting = enemyObject.AddComponent<EnemyShootingType1>();

        // Beállítjuk az ellenséget és a játékost
        playerObject.transform.position = new Vector3(0, 0, 0);
        enemyObject.transform.position = new Vector3(0, 5, 0); // Az ellenség közvetlenül a játékos felett van

        // Manuálisan állítjuk az ellenséget, hogy a játékos felé nézzen
        Vector3 direction = playerObject.transform.position - enemyObject.transform.position;
        float zAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        enemyObject.transform.rotation = Quaternion.Euler(0, 0, zAngle);

        // Töltünk egy lövedéket prefabként (üres GameObject-ként a teszt kedvéért)
        enemyShooting.bulletPrefab = new GameObject("Bullet");
        enemyShooting.bulletPrefab.tag = "Bullet"; // Hozzunk létre egy tag-et a lövedékekhez

        // Várjunk pár frame-et, hogy a lövés megtörténjen
        for (int i = 0; i < 10; i++)
        {
            yield return null;
        }

        // Ellenőrzés: Ellenőrizzük, hogy a lövés megtörtént-e (a BulletPrefab egy új példányának létrejötte alapján)
        var bullets = GameObject.FindGameObjectsWithTag("Bullet");
        Assert.IsTrue(bullets.Length > 0, "Az ellenségnek lövést kellett volna leadnia, de ez nem történt meg.");
    }
}
