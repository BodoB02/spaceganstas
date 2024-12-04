using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameManagerTests
{
    [Test]
    public void GameManager_CanBeCreated()
    {
        // Előkészítés: Hozzunk létre egy GameObject-et és adjunk hozzá egy GameManager komponenst
        var gameObject = new GameObject();
        var gameManager = gameObject.AddComponent<GameManager>();

        // Ellenőrzés: Ellenőrizzük, hogy a GameManager nem null
        Assert.IsNotNull(gameManager);
    }
}
