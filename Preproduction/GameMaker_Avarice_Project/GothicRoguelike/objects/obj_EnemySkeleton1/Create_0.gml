// Call parent function
event_inherited()

// Add immunities, resistaces, and weaknesses
ds_list_add(resistances, "fire", "paralysis", "wind");
ds_list_add(weaknesses, "melee", "explosive");

// variables to get enemy to patrol while passive
patrolingLeft = false;

//line of sight variables
facing = 1;
inView  = ds_list_create();