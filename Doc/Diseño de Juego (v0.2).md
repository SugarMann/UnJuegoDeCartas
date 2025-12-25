### 1. El Bucle de Juego (Core Loop) 

El flujo principal de la partida se mantiene, con las siguientes modificaciones clave en las recompensas y eventos:

1. **FASE DE MAPA:** El jugador avanza por un mapa de nodos ramificados.
    
2. **FASE DE NODO (Evento):** El nodo se revela. Puede ser:
    
    - Combate
        
    - Combate de Élite
        
    - **Evento Aleatorio (?) **
        
    - Campamento
        
    - Jefe
        
3. **FASE DE RECOMPENSA:**
    
    - Tras un combate, el jugador **solo** recibe **Experiencia** y **Oro**.
        
    - Las cartas nuevas **no** se obtienen en el botín de combate estándar. Se adquieren en Campamentos (gastando EXP) o Mercaderes (gastando Oro).
        
4. **Vuelta al paso 1.**
    

---

### 2. Los Nodos del Mapa 

- **Combate / Élite:** Sin cambios. Lucha estándar o difícil por más recompensas (EXP y Gold).
    
- **Campamento:**
    
    - El jugador gasta **EXP** para "comprar" mejoras permanentes, como:
        
        - Mejorar una carta (ej. "Ataque Básico" -> "Ataque Básico+").
            
        - Adquirir una nueva habilidad pasiva para un personaje.
            
        - Obtener una carta nueva de una selección de 3.
            
    - También se puede "Descansar" para recuperar HP (esto podría costar 0 EXP, o toda la acción del campamento).
        
- **Evento Aleatorio (?):**
    
    - Este nodo (marcado con una interrogación) activará **una de tres escenas de forma aleatoria**:
        
        1. **Mercader:** Una tienda donde el jugador gasta **Oro (Gold)** para comprar cartas, reliquias o pociones.
            
        2. **Trueque:** Un evento narrativo de decisión (ej. "Un ermitaño te ofrece un pacto: ¿Pierdes 10 HP máx. a cambio de esta poderosa reliquia?").
            
        3. **Emboscada:** Un combate que comienza inmediatamente, y donde **los enemigos atacan primero** (tienen prioridad en el primer turno). (? IMPORTANTE -> balanceado)
            

---

### 3. El Sistema de Combate 

Aquí es donde aplicamos tus cambios más significativos.

- **Sin Intenciones Enemigas:**
    
    - Los enemigos **NO muestran sus intenciones**. El jugador no sabrá si el enemigo va a atacar, defenderse o aplicar un _debuff_.
        
    - Esto aumenta la dificultad y premia la memorización de patrones y la capacidad de reacción defensiva (tener Bloqueo "por si acaso").
        
- **Turno del Jugador: Sistema de Robo:**
    
    1. **Inicio del Turno:**
        
        - Se reinicia la Energía (AP) de cada personaje (ej. 3 AP c/u).
            
        - Se elimina todo el Bloqueo.
            
    2. **Fase de Robo:**
        
        - El jugador roba **6 cartas** de un **único mazo de la compañía** (el mazo total del grupo).
            
        - **Regla de Robo 1:** La mano resultante debe contener **al menos una carta de cada personaje** (ej. al menos 1 del Guerrero, 1 de la Hechicera, 1 del Pícaro).
            
        - **Regla de Robo 2:** Las cartas en la mano **no pueden repetirse** (no puedes tener dos "Golpe Fuerte" en la mano a la vez).
            
    3. **Nota de Balance:** Este sistema de robo es complejo. El algoritmo tendrá que "forzar" la Regla 1 (quizás robar 3 cartas, y si falta la de un personaje, buscarla y añadirla, robando luego 3 más al azar). **Es un punto a revisar y balancear en profundidad.**
        
    4. **Fase de Acciones:**
        
        - El jugador juega cartas desde esta **mano compartida**.
            
        - **Coste de AP:** Una carta de "Guerrero" (definida en el JSON) gasta AP del Guerrero. Una carta de "Hechicera" gasta AP de la Hechicera. Esto mantiene la gestión individual de recursos.
            
- **Turno del Enemigo:**
    
    - Los enemigos ejecutan sus acciones (sin haberlas revelado).
        

---

### 4. Diseño de Cartas 

Según tu requisito, todas las claves y descripciones de los JSON estarán en inglés para facilitar el desarrollo.

#### Estructura JSON de la Carta

JSON

```
{
  "card_id": "warrior_heavy_strike_01",
  "title": "Heavy Strike",
  "ap_cost": 2,
  "type": "Attack",
  "character_class": "Warrior",
  "rarity": "Common",
  "description": "Deal 10 damage.",
  "effects": [
    {
      "effect_type": "DAMAGE",
      "value": 10,
      "target": "SELECTED_ENEMY"
    }
  ],
  "upgrade_id": "warrior_heavy_strike_01_plus"
}
```

#### Conjunto de Cartas Inicial (21 cartas)

**Cartas Básicas (Comunes a todos) (Total: 6)**

- `basic_strike` (1 AP): "Deal 5 damage."
    
- `basic_defend` (1 AP): "Gain 5 Block."
    
- _Versiones Mejoradas (+):_ "Deal 8 damage.", "Gain 8 Block."
    

**Cartas del Guerrero (Total: 5)**

- `heavy_strike` (2 AP): "Deal 10 damage."
    
- `iron_shield` (1 AP): "Gain 8 Block."
    
- `taunt` (1 AP): "Gain 5 Block. All enemies target you next turn."
    
- `shield_bash` (2 AP): "Deal 6 damage. Apply 1 Vulnerable."
    
- `battleship` (3 AP, Power): "Passive. At the start of your turn, gain 4 Block."
    

**Cartas de la Hechicera (Total: 5)**

- `fireball` (2 AP): "Deal 8 damage to one enemy and 4 to all others."
    
- `mana_shield` (1 AP): "Gain 6 Block."
    
- `lightning_bolt` (1 AP): "Deal 7 damage. Apply 1 Burn (2 damage at end of turn)."
    
- `haste` (0 AP): "Draw 1 card."
    
- `intellect` (3 AP, Power): "Passive. Draw 1 additional card at the start of your turn."
    

**Cartas del Pícaro (Total: 5)**

- `quick_stab` (0 AP): "Deal 3 damage."
    
- `evasion` (1 AP): "Gain 5 Block. If you take no damage this turn, draw 1 card."
    
- `poison_dart` (1 AP): "Apply 3 Poison."
    
- `double_strike` (1 AP): "Deal 4 damage, twice."
    
- `adrenaline` (3 AP, Power): "Passive. Gain 1 additional AP at the start of your turn."
    

---

### 5. Enemigos Iniciales (3 Tipos)

El diseño de enemigos no cambia, pero **la forma de jugar contra ellos sí.** Al no mostrar intenciones, el jugador tendrá que aprender sus patrones de ataque.

1. **Bandido (DPS Básico):**
    
    - HP: 25
        
    - Patrón: Ataca (8) -> Ataca (8) -> Defiende (10) -> Repite.
        
2. **Bruto (Tanque):**
    
    - HP: 60
        
    - Patrón: Ataque Fuerte (15) -> Debuff (Aplica "Weak") -> Repite.
        
3. **Ladrón (Rápido):**
    
    - HP: 15
        
    - Patrón: Siempre ataca (5) y aplica "Vulnerable".