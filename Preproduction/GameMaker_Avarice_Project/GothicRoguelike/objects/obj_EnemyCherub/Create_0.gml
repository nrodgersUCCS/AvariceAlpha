// Call parent event
event_inherited()

// Add immunities, resistances, and weaknesses
ds_list_add(immunities, "fire");
ds_list_add(resistances, "freezing", "wind");
ds_list_add(weaknesses, "explosive", "paralysis");

//helps with the timing of fireballs
canMove = true;
canAttack = true;
canChangeDirection = true;

// For circular movement
theta = 0
attackRadius = 1;
radius = attackRadius * 0.7;
rotationSpeed = 0.5;

// enemy aggro variables
facing = 0;