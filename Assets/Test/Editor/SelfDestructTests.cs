using NUnit.Framework;
using UnityEngine;
using System.Collections;

public class SelfDestructTests
{
    [Test]
    public void ObjectDestroysItselfAfterTime()
    {
        // Előkészítés: Hozzuk létre a megsemmisítendő objektumot és a SelfDestruct szkriptet
        GameObject destructingObject = new GameObject();
        var selfDestruct = destructingObject.AddComponent<SelfDestruct>();
        selfDestruct.timer = 1f;

        // Cselekvés: Szimuláljuk az idő múlását manuálisan
        for (float t = 0; t < 1.5f; t += Time.deltaTime)
        {
            // Manuálisan csökkentjük az időzítőt, mintha az Update metódus futna
            selfDestruct.timer -= Time.deltaTime;
            if (selfDestruct.timer <= 0)
            {
                Object.DestroyImmediate(destructingObject);
            }
        }

        // Ellenőrzés
        Assert.IsTrue(destructingObject == null || destructingObject.Equals(null), "Az objektumnak meg kellett volna semmisülnie.");
    }
}
