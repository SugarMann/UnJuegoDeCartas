import random

# --- 1. DATA DEFINITION ---
# Game structure
CARD_DATABASE = [
    # Basic Cards
    {"card_id": "basic_strike_w", "title": "Basic Strike (W)", "ap_cost": 1, "character_class": "Warrior", "effects": [{"effect_type": "DAMAGE", "value": 5}]},
    {"card_id": "basic_defend_w", "title": "Basic Defend (W)", "ap_cost": 1, "character_class": "Warrior", "effects": [{"effect_type": "BLOCK", "value": 5}]},
    {"card_id": "basic_strike_s", "title": "Basic Strike (S)", "ap_cost": 1, "character_class": "Sorceress", "effects": [{"effect_type": "DAMAGE", "value": 5}]},
    {"card_id": "basic_defend_s", "title": "Basic Defend (S)", "ap_cost": 1, "character_class": "Sorceress", "effects": [{"effect_type": "BLOCK", "value": 5}]},
    {"card_id": "basic_strike_r", "title": "Basic Strike (R)", "ap_cost": 1, "character_class": "Rogue", "effects": [{"effect_type": "DAMAGE", "value": 5}]},
    {"card_id": "basic_defend_r", "title": "Basic Defend (R)", "ap_cost": 1, "character_class": "Rogue", "effects": [{"effect_type": "BLOCK", "value": 5}]},

    # Warrior Cards
    {"card_id": "heavy_strike", "title": "Heavy Strike", "ap_cost": 2, "character_class": "Warrior", "effects": [{"effect_type": "DAMAGE", "value": 10}]},
    {"card_id": "iron_shield", "title": "Iron Shield", "ap_cost": 1, "character_class": "Warrior", "effects": [{"effect_type": "BLOCK", "value": 8}]},

    # Sorceress Cards
    {"card_id": "fireball", "title": "Fireball", "ap_cost": 2, "character_class": "Sorceress", "effects": [{"effect_type": "DAMAGE", "value": 8}]},
    {"card_id": "mana_shield", "title": "Mana Shield", "ap_cost": 1, "character_class": "Sorceress", "effects": [{"effect_type": "BLOCK", "value": 6}]},
    
    # Rogue Cards
    {"card_id": "quick_stab", "title": "Quick Stab", "ap_cost": 0, "character_class": "Rogue", "effects": [{"effect_type": "DAMAGE", "value": 3}]},
    {"card_id": "evasion", "title": "Evasion", "ap_cost": 1, "character_class": "Rogue", "effects": [{"effect_type": "BLOCK", "value": 5}]},
]


# --- 2. LOGIC CLASSES AND FUNCTIONS ---

# Company's shared deck
deck = []
hand = []
discard_pile = []

# Heroes' status
heroes = {
    "Warrior": {"hp": 50, "max_hp": 50, "ap": 2, "max_ap": 2, "block": 0},
    "Sorceress": {"hp": 30, "max_hp": 30, "ap": 2, "max_ap": 2, "block": 0},
    "Rogue": {"hp": 35, "max_hp": 35, "ap": 2, "max_ap": 2, "block": 0},
}

# Enemy status
enemy = {"hp": 80, "max_hp": 80, "damage": 15, "block": 0}


def initialize_deck():
    """Creates the initial company deck."""
    global deck, discard_pile, hand
    deck = []
    # Add 4 of each basic card
    for _ in range(4):
        deck.append(next(c for c in CARD_DATABASE if c["card_id"] == "basic_strike_w"))
        deck.append(next(c for c in CARD_DATABASE if c["card_id"] == "basic_defend_w"))
        deck.append(next(c for c in CARD_DATABASE if c["card_id"] == "basic_strike_s"))
        deck.append(next(c for c in CARD_DATABASE if c["card_id"] == "basic_defend_s"))
        deck.append(next(c for c in CARD_DATABASE if c["card_id"] == "basic_strike_r"))
        deck.append(next(c for c in CARD_DATABASE if c["card_id"] == "basic_defend_r"))
    
    # Add 1 of each special card
    deck.append(next(c for c in CARD_DATABASE if c["card_id"] == "heavy_strike"))
    deck.append(next(c for c in CARD_DATABASE if c["card_id"] == "iron_shield"))
    deck.append(next(c for c in CARD_DATABASE if c["card_id"] == "fireball"))
    deck.append(next(c for c in CARD_DATABASE if c["card_id"] == "mana_shield"))
    deck.append(next(c for c in CARD_DATABASE if c["card_id"] == "quick_stab"))
    deck.append(next(c for c in CARD_DATABASE if c["card_id"] == "evasion"))

    discard_pile = []
    hand = []
    random.shuffle(deck)
    print(f"Initial deck with {len(deck)} cards.")

def reshuffle_discard_into_deck():
    """Shuffles the discard pile back into the deck."""
    global deck, discard_pile
    print("... Shuffling discard pile into deck ...")
    deck.extend(discard_pile)
    discard_pile = []
    random.shuffle(deck)

def draw_hand():
    """
    Draws 5 cards, enforcing the 'no duplicates' rule.
    NOTE: The '1 card per hero' rule is complex and has been simplified.
    """
    global hand, deck, discard_pile
    
    # Move cards from hand to discard pile
    discard_pile.extend(hand)
    hand = []
    
    hand_ids = set() # To control for duplicates
    
    while len(hand) < 5:
        # If deck is empty, reshuffle discard pile
        if not deck:
            if not discard_pile:
                print("Error! No more cards to draw.")
                break # Ran out of cards in the entire cycle
            reshuffle_discard_into_deck()
        
        card = deck.pop(0)
        
        # Rule: No duplicate card IDs in hand
        if card["card_id"] not in hand_ids:
            hand.append(card)
            hand_ids.add(card["card_id"])
        else:
            # If it's a duplicate, discard it so it's not lost
            discard_pile.append(card)

def start_player_turn():
    """Resets AP, Block, and draws a new hand."""
    print("\n--- YOUR TURN BEGINS ---")
    for hero_name, stats in heroes.items():
        stats["ap"] = stats["max_ap"]
        stats["block"] = 0 # Block is lost every turn
    
    enemy["block"] = 0
    draw_hand()

def print_game_status():
    """Displays the current combat status."""
    print("-" * 30)
    # Heroes
    for hero_name, stats in heroes.items():
        print(f"🦸 {hero_name}: {stats['hp']}/{stats['max_hp']} HP | {stats['ap']} AP | {stats['block']} Block")
    
    # Enemy
    print(f"👹 Enemy: {enemy['hp']}/{enemy['max_hp']} HP | {enemy['block']} Block")
    print("-" * 30)
    
    # Hand
    print("Your Hand:")
    if not hand:
        print("  (No cards in hand)")
    for i, card in enumerate(hand):
        print(f"  [{i+1}] {card['title']} (Cost: {card['ap_cost']} AP from {card['character_class']}) - {card['effects'][0]['effect_type']} {card['effects'][0]['value']}")
    print("  [0] End Turn")

def execute_card_effect(card):
    """Executes the card's effect."""
    # This is the "heart" of your logic.
    # In C# (Unity), this would call a "CardEffectManager"
    
    effect = card["effects"][0] # Simplified to 1 effect per card
    
    if effect["effect_type"] == "DAMAGE":
        damage = effect["value"]
        # Damage hits block first
        if enemy["block"] >= damage:
            enemy["block"] -= damage
            print(f"💥 {card['title']} deals {damage} damage. The enemy blocks it.")
        else:
            damage_to_hp = damage - enemy["block"]
            enemy["block"] = 0
            enemy["hp"] -= damage_to_hp
            print(f"💥 {card['title']} deals {damage_to_hp} damage to the enemy.")

    elif effect["effect_type"] == "BLOCK":
        hero_name = card["character_class"]
        heroes[hero_name]["block"] += effect["value"]
        print(f"🛡️ {card['title']} gives {effect['value']} Block to {hero_name}.")

def player_turn():
    """Main player action loop."""
    global hand
    
    while True:
        print_game_status()
        
        try:
            choice = int(input("Choose a card (1-5) or 0 to end: "))
        except ValueError:
            print("Invalid input.")
            continue
            
        if choice == 0:
            print("You end your turn.")
            break
        
        if 1 <= choice <= len(hand):
            selected_card = hand[choice - 1]
            hero_name = selected_card["character_class"]
            hero_stats = heroes[hero_name]
            
            # Check AP
            if hero_stats["ap"] >= selected_card["ap_cost"]:
                # Pay AP cost
                hero_stats["ap"] -= selected_card["ap_cost"]
                
                # Execute effect
                execute_card_effect(selected_card)
                
                # Move card from hand to discard
                discard_pile.append(hand.pop(choice - 1))
            else:
                print(f"Insufficient AP! {hero_name} does not have {selected_card['ap_cost']} AP.")
        else:
            print("That card does not exist.")

def enemy_turn():
    """Simple enemy turn logic."""
    print("\n--- ENEMY TURN BEGINS ---")
    
    # Super simple AI: Always attacks the Warrior if alive, otherwise the Rogue
    target_hero_name = "Warrior"
    if heroes[target_hero_name]["hp"] <= 0:
        target_hero_name = "Rogue"
    if heroes[target_hero_name]["hp"] <= 0:
        target_hero_name = "Sorceress"
        
    target_hero = heroes[target_hero_name]
    damage = enemy["damage"]
    
    # Damage hits block first
    if target_hero["block"] >= damage:
        target_hero["block"] -= damage
        print(f"👹 The enemy attacks {target_hero_name} for {damage} damage. Blocked!")
    else:
        damage_to_hp = damage - target_hero["block"]
        target_hero["block"] = 0
        target_hero["hp"] -= damage_to_hp
        print(f"👹 The enemy attacks {target_hero_name} for {damage_to_hp} damage.")

def check_game_over():
    """Checks if the game has ended."""
    if enemy["hp"] <= 0:
        print("\n🎉 VICTORY!! You defeated the enemy. 🎉")
        return True
    
    if all(stats["hp"] <= 0 for stats in heroes.values()):
        print("\n💀 DEFEAT. All your heroes have fallen. 💀")
        return True
        
    return False

# --- 3. MAIN GAME LOOP ---

def main():
    print("--- Welcome to the 'A Card Game' DEMO! ---")
    initialize_deck()
    
    turn = 0
    while True:
        turn += 1
        print(f"\n================= ROUND {turn} =================")
        start_player_turn()
        player_turn()
        
        if check_game_over():
            break
            
        enemy_turn()
        
        if check_game_over():
            break

if __name__ == "__main__":
    main()