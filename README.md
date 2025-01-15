# Projekt Dokumentáció

## Futtatás

### A játék futtatásához kövesd az alábbi lépéseket:

**Letöltés**

- Látogass el a GitHub release oldalára:GitHub Release Link
- Töltsd le a futtatható fájlt a "Assets" részből (SpaceGangsta_v1.2.zip).
- Csomagold ki a .zip fájlt egy tetszőleges mappába.

**Indítás**

- Nyisd meg a kicsomagolt mappát.
- Indítsd el a játékot a My Project.exe fájlra kattintva (Windows esetén).

## Fordítás Unity-ben

### Ha a projektet Unity-ben szeretnéd futtatni vagy szerkeszteni, kövesd az alábbi lépéseket:

**Előkészítés**

- Töltsd le a teljes projektet a GitHub oldaláról:

  - Kattints a "Code" gombra, és válaszd a "Download ZIP" lehetőséget, vagy klónozd a repository-t a következő paranccsal:
    - git clone https://github.com/BodoB02/spaceganstas.git

- Nyisd meg Unity-ben:
  - Indítsd el a Unity Editor-t.
  - Kattints a "Open Project" gombra.
  - Navigálj a letöltött projekt mappájába, és nyisd meg.

**Fordítás**

- A Build Settings-ben (File > Build Settings) válaszd ki a platformot:
  - Platform: Windows/Mac/WebGL.
  - Scene-ek hozzáadása: Győződj meg róla, hogy az összes jelenet hozzá van adva (pl. Level1.unity, Level2.unity, stb.).
  - Kattints a "Build" gombra, és válassz egy célmappát a build számára.

**Szükséges Unity verzó**
A projekt Unity verzója: 2022.3.47f1 Ha nem rendelkezel ezzel a verzóval, letöltheted a Unity Hub segítségével.

## Labirintus

A labirintust manuálisan építettem fel Unity-ben a következő eszközökkel:

- **Map**: A falak a játék elején kerülnek generálásra a **"MapBoundry"** prefab segítségével, amely tárolja a pályát körülvevő vonalat, amin nem lehet áthaladni. A generálásért a **PlayerMovement.cs** script felel.

- **Prefabok**:
  - **Játékos**:
    - Player
  - **Ellenségek**:
    - EnemyType1
    - EnemyType2
    - Főellenség: **MeteorLord**
  - **Tárgyak**:
    - PowerUps
    - Meteors
    - Laser
    - Items (ShipParts)
  - **Egyéb**:
    - ShipManager
    - GameTimer
    - ObjectSpawner
    - GameManager
    - PauseCanvas
    - Main Camera

### Elérési út

- A labirintus elérhető a következő jelenetben:  
  **`Assets/Scenes/Level1.unity`**
  **`Assets/Scenes/Level2.unity`**
  **`Assets/Scenes/Level3.unity`**
  **`Assets/Scenes/Level4.unity`**
- A labirintus generálásához használt script:  
  **`Assets/Scripts/PlayerMovement.cs`**

### Használt Assetek

#### Textúrák és Hangok

- A labirintushoz és az objektumokhoz szükséges textúrák és Hangok az alábbi helyen találhatók:  
  **`Assets/Pictures`**
  **`Assets/Audio`**

### Tesztelés

- A játékos navigálhat a labirintusban, és interakcióba léphet:
  - Ellenségekkel (Enemy type 1, type 2, MeteorLord)
  - Tárgyakkal (PowerUps, Meteors, Laser, ShipParts).
- Az összes használt asset dokumentált és elérhető a **Project** fülben.
- A tesztelési környezet: Unity Editor (verzió: [2022.3.45f1 <DX11>]).
