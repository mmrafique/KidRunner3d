# Kid Runner 3D - Endless Runner Mobile Game

## Abstract

Minijoc arcade mobile estil endless runner on el jugador controla un personatge que llença projectils a elements i aquests es trenquen. El jugador ha de recollir monedes, diamants i powerups mentre esquiva obstacles per aconseguir la puntuació més alta possible.

**Captura del joc:**
![Gameplay Principal](screenshots/gameplay_main.png)

---

## Característiques Principals

### 🎮 Mecàniques del Joc

#### Sistema de Carrils (Lane System)
- El jugador es mou entre 3 carrils (esquerra, centre, dreta)
- Input tàctil per canviar de carril
- Velocitat progressiva que augmenta amb el temps

**Screenshot:**
![Sistema de carrils](screenshots/lanes.png)

#### Pickups i Col·lectables
1. **Monedes** - Sumen punts per desbloquejar personatges
2. **Diamants** - Moneda premium del joc
3. **Cors** - Donen vides extra
4. **PowerUps**:
   - Jetpack - Permet volar temporalment
   - Escut - Protegeix d'un cop
   - Imant - Atrau monedes automàticament
   - Monopatí - Augmenta velocitat

**Screenshots:**
![Monedes i Diamants](screenshots/pickups.png)
![PowerUps](screenshots/powerups.png)

#### Sistema de Vides (Hearts)
- El jugador comença amb 3 vides (cors)
- Xocar amb obstacles treu 1 vida
- Recollir cors afegeix 1 vida
- Game Over quan es queda sense cors

**Screenshot:**
![UI Hearts](screenshots/ui_hearts.png)

---

## 🎨 Implementació Tècnica

### DOTween - Animacions

S'ha utilitzat **DOTween** per crear animacions fluides i professionals en múltiples elements:

#### Animacions de Pickups
**Fitxers:** `Coin.cs`, `Diamond.cs`, `HeartPickup.cs`

```csharp
// Animació de flotació continua
transform.DOMoveY(transform.position.y + 0.5f, 1f)
    .SetEase(Ease.InOutSine)
    .SetLoops(-1, LoopType.Yoyo);

// Animació al recoger
transform.DOScale(Vector3.one * 1.5f, 0.2f).SetEase(Ease.OutBounce);
transform.DORotate(new Vector3(360, 360, 0), 0.2f, RotateMode.FastBeyond360);
```

**Efectes implementats:**
- ✅ Flotació vertical (up/down) amb `DOMoveY`
- ✅ Rotació contínua combinada amb `Update()`
- ✅ Escala explosiva al recoger amb `DOScale`
- ✅ Rotació ràpida al recoger amb `DORotate`

**Screenshots:**
![DOTween Flotació](screenshots/dotween_float.png)
![DOTween Recollir](screenshots/dotween_pickup.png)

#### Animacions de Daño
**Fitxers:** `Obstacle.cs`, `ObstacleMoving.cs`

```csharp
// Shake del jugador al rebre dany
playerTransform.DOShakePosition(0.3f, 0.2f, 10, 90);

// Flash de color vermell
renderer.material.DOColor(Color.red, 0.2f)
    .OnComplete(() => renderer.material.DOColor(Color.white, 0.2f));
```

**Efectes implementats:**
- ✅ Temblor/Shake del personatge amb `DOShakePosition`
- ✅ Flash vermell al material amb `DOColor`
- ✅ Transició suau de tornada al color original

**Screenshots:**
![DOTween Damage Shake](screenshots/dotween_damage.png)
![DOTween Color Flash](screenshots/dotween_flash.png)

---

### 📱 Sensors Mòbils - Vibracions (Haptic Feedback)

S'ha implementat **vibració haptic** com a sensor mòbil per millorar la feedback del jugador.

#### Implementació
**Fitxers:** Tots els scripts de pickups i obstacles

```csharp
// Vibració simple (monedes, cors)
Handheld.Vibrate();

// Vibració doble (diamants, powerups, dany)
Handheld.Vibrate();
System.Threading.Thread.Sleep(50);
Handheld.Vibrate();
```

#### Patrons de Vibració Implementats:

| Acció | Tipus Vibració | Fitxer |
|-------|----------------|--------|
| Recoger moneda | Simple | `Coin.cs` |
| Recoger diamant | Doble | `Diamond.cs` |
| Recoger cor | Simple | `HeartPickup.cs` |
| Xocar amb obstacle | Doble forta | `Obstacle.cs`, `ObstacleMoving.cs` |

**Justificació:**
Les vibracions proporcionen **feedback tàctil instantani** al jugador, millorant la immersió i la resposta del joc sense necessitat de mirar la pantalla.

**Screenshot:**
![Configuració Android](screenshots/android_vibration.png)

---

### 🔊 Sistema d'Audio

Cada interacció del joc té el seu efecte de so corresponent:

#### Sons Implementats
**Fitxer:** `AudioManager.cs`

- ✅ So de recollir moneda
- ✅ So de recollir diamant
- ✅ So de recollir cor
- ✅ So de powerup activat
- ✅ So de xoc/dany
- ✅ So de Game Over
- ✅ Música de fons

```csharp
AudioSource.PlayClipAtPoint(collectSound, transform.position);
```

**Screenshots:**
![AudioManager Inspector](screenshots/audiomanager.png)
![Sons assignats](screenshots/audio_clips.png)

---

### ✨ Sistema de Partícules

Efectes visuals amb partícules en totes les interaccions:

#### Partícules Implementades
**Carpeta:** `Assets/Prefabs/Effects/`

- ✅ Explosió de moneda (groc)
- ✅ Espurnes de diamant (blau brillant)
- ✅ Cors (rosa/vermell)
- ✅ Impacte d'obstacle (fum/pols)
- ✅ Efecte de powerups

**Screenshots:**
![Partícules Moneda](screenshots/particles_coin.png)
![Partícules Diamant](screenshots/particles_diamond.png)
![Partícules Dany](screenshots/particles_damage.png)

---

## 🎯 Interfície d'Usuari (UI)

### UI Adaptable
La interfície s'adapta automàticament a diferents resolucions de pantalla:

- ✅ Canvas amb **Scale with Screen Size**
- ✅ Anchors configurats per adaptar-se
- ✅ Text i botons escalables

**Pantalles implementades:**
1. **Menu Principal**
   - Botó Play
   - Botó Settings
   - Botó Exit

2. **Character Selection**
   - 3 personatges seleccionables
   - Sistema de desbloqueig amb monedes (100 monedes/personatge)
   - Visualització en temps real del personatge

3. **Gameplay UI**
   - Comptador de monedes
   - Comptador de diamants
   - Vides (cors)
   - Distància recorreguda

4. **Game Over Screen**
   - Puntuació final
   - Millor puntuació (High Score)
   - Botó Retry
   - Botó Menu

**Screenshots:**
![Menu Principal](screenshots/ui_menu.png)
![Character Selection](screenshots/ui_character_selection.png)
![Gameplay UI](screenshots/ui_gameplay.png)
![Game Over](screenshots/ui_gameover.png)

---

## 🏗️ Arquitectura del Projecte

### Scripts Principals

#### GameManager.cs
Gestiona l'estat global del joc:
- Sistema de puntuació
- Gestió de vides
- Sistema de monedes i diamants
- Game Over i reinici

#### PlayerController.cs
Controla el personatge:
- Moviment entre carrils
- Salt i slide
- Detecció de col·lisions
- Animacions

#### TileManager.cs
Sistema de generació procedural:
- Spawn de tiles infinites
- Reciclatge de tiles
- Optimització de memòria

#### Spawners
- `DiamondSpawner.cs` - Genera diamants aleatòriament
- `HeartSpawner.cs` - Genera cors
- `JetPackSpawner.cs` - Genera jetpacks
- `MagnetSpawner.cs` - Genera imants
- `ShieldSpawner.cs` - Genera escuts
- `SkateboardSpawner.cs` - Genera monopatins

**Screenshot:**
![Estructura Scripts](screenshots/scripts_folder.png)

---

## 📊 Justificació dels Punts de la Rúbrica

### ✅ Joc Endless Relativament Equilibrat
- Velocitat progressiva que augmenta gradualment
- Spawn aleatori d'obstacles i pickups
- Sistema de dificultad balancejat

### ✅ Interfície Adaptable
- Canvas amb **Scale with Screen Size**
- UI funcional en múltiples resolucions
- Anchors correctament configurats

### ✅ Ús de DOTween en Llocs Localitzats
- Flotació de pickups
- Animacions de recollida
- Efectes de dany (shake, color flash)
- **5+ implementacions diferents de DOTween**

### ✅ Events Sonors a Totes les Interaccions
- So en recollir items
- So en rebre dany
- So de powerups
- Música de fons

### ✅ Efectes de Partícules a Totes les Interaccions
- Partícules al recoger monedes
- Partícules al recoger diamants
- Partícules en xocs
- Efectes de powerups

### ✅ Ús Visible de Sensors (1+ sensor)
- **Vibració haptic** implementada en:
  - Recollir items (vibració simple)
  - Diamants (vibració doble)
  - Dany (vibració forta)

### ✅ Treball General de Cohesió del Projecte
- Assets customitzats (no assets genèrics de Unity)
- Estil visual consistent
- Mecàniques ben integrades

**Screenshot del Build:**
![Build Android](screenshots/android_build.png)

---

## 🚀 Com Executar el Projecte

### Requisits
- Unity 2021.3 o superior
- DOTween (importat des de Asset Store)
- Android SDK (per builds mòbils)

### Instruccions
1. Clona el repositori
2. Obre el projecte amb Unity
3. Importa DOTween desde Window > Asset Store
4. Obre l'escena `MainMenu` a `Assets/Scenes/`
5. Compila per Android/iOS o executa en l'Editor

### Instal·lació DOTween
```
1. Window → Asset Store
2. Buscar "DOTween"
3. Download → Import
4. Setup DOTween (apareix popup automàtic)
```

**Screenshot:**
![DOTween Setup](screenshots/dotween_setup.png)

---

## 📁 Estructura de Carpetes

```
Assets/
├── Scenes/              # Escenes del joc
│   ├── MainMenu
│   ├── CharacterSelection
│   └── GamePlay
├── Scripts/             # Tots els scripts C#
├── Prefabs/             # Prefabs d'obstacles, pickups, etc
│   ├── Coin/
│   ├── Diamond/
│   ├── Heart/
│   ├── Obstacles/
│   └── PowerUps/
├── Models/              # Models 3D
├── Animations/          # Animacions dels personatges
├── Audios/              # Efectes de so i música
├── sprites/             # Sprites UI
└── Prefabs/Effects/     # Sistemes de partícules
```

---

## 🎓 Crèdits

**Desenvolupador:** [El teu nom]
**Projecte:** MiniJoc Arcade Mobile
**Data:** Gener 2026
**Motor:** Unity 2021.3
**Llibreries:** DOTween (Demigiant)

---

## 📝 Notes Addicionals

### Millores Futures
- [ ] Més personatges desblocables
- [ ] Sistema de missions diàries
- [ ] Leaderboards online
- [ ] Més tipus de powerups

### Problemes Coneguts
- Les vibracions només funcionen en dispositius reals (no en l'Editor)
- DOTween requereix importació manual

---

**Enllaç al Repositori:** [GitHub Link aquí]
**Build APK:** [Release Link aquí]
