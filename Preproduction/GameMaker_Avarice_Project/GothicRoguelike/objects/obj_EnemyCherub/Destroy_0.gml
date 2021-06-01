//Uses the odds from the enemies drop chance to randomly drop an item
chance = irandom(lootChance)

//Used to drop the proper item.
if(chance <= armorChance){
	instance_create_layer(x,y,layer, obj_armor);
}
else if(chance > armorChance && chance <= armorChance + helmetChance){
	instance_create_layer(x,y,layer,obj_helmet);
}
else if(chance > armorChance + helmetChance && chance <= armorChance + helmetChance + swordChance){
	instance_create_layer(x,y,layer,obj_sword);
}
else{
	instance_create_layer(x,y,layer,obj_greatsword);
}