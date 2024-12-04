# Projekt Dokumentáció

## Labirintus

A labirintust manuálisan építettem fel Unity-ben a következő eszközökkel:

- **Map**: A falak a játék elején kerülnek generálásra a **"MapBoundary"** prefab segítségével, amely tárolja a pályát körülvevő vonalat, amin nem lehet áthaladni. A generálásért a **Movement.cs** script felel.

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
  **`Assets/Scripts/Movement.cs`**

### Használt Assetek

#### Textúrák

- A labirintushoz és az objektumokhoz szükséges textúrák az alábbi helyen találhatók:  
  \*\*`Assets/Scenes/PNGs

### Tesztelés

- A játékos navigálhat a labirintusban, és interakcióba léphet:
  - Ellenségekkel (Enemy type 1, type 2, MeteorLord)
  - Tárgyakkal (PowerUps, Meteors, Laser, ShipParts).
- Az összes használt asset dokumentált és elérhető a **Project** fülben.
- A tesztelési környezet: Unity Editor (verzió: [2022.3.45f1 <DX11>]).
