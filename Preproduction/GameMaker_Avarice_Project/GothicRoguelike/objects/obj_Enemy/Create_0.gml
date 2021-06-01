// Damage vars
enemyHealth = 5;		// Hit points
enemyDamage = 10;		// Enemy damage
canBeHit = true;		// Bool for invincibility frames
numIFrames = 20;		// Number of invincibility frames
damageSound = 0;		// Sound to play when damaged

// Loot vars

// Behavior vars
state = "idle"
aggro = false

// Create lists for weaknesses, resistances, and immunities
// (melee, ranged, fire, freezing, explosive, wind, paralysis)
weaknesses = ds_list_create()
resistances = ds_list_create()
immunities = ds_list_create()

// Update health from room completion
scr_UpdateEnemyHealth()

//Presets the chance of item drops
armorChance = 1;
helmetChance = 1;
swordChance = 1;
greatswordChance = 1;

//Creates odds easily.
lootChance = armorChance + helmetChance + swordChance + greatswordChance;

//Gives enemy pathing
path = path_add();