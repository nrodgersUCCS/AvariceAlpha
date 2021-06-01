/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 53F1D59D
/// @DnDArgument : "code" "//Uses the odds from the enemies drop chance to randomly drop an item$(13_10)chance = irandom(lootChance)$(13_10)$(13_10)//Used to drop the proper item.$(13_10)if(chance <= armorChance){$(13_10)	instance_create_layer(x,y,layer, obj_armor);$(13_10)}$(13_10)else if(chance > armorChance && chance <= armorChance + helmetChance){$(13_10)	instance_create_layer(x,y,layer,obj_helmet);$(13_10)}$(13_10)else if(chance > armorChance + helmetChance && chance <= armorChance + helmetChance + swordChance){$(13_10)	instance_create_layer(x,y,layer,obj_sword);$(13_10)}$(13_10)else{$(13_10)	instance_create_layer(x,y,layer,obj_greatsword);$(13_10)}$(13_10)$(13_10)// Clean up for remaining pieces$(13_10)instance_destroy(obj_SkelodevilArm);$(13_10)instance_destroy(obj_SkelodevilLeg);$(13_10)instance_destroy(obj_SkelodevilBody);$(13_10)instance_destroy(obj_SkelodevilHead);"
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

// Clean up for remaining pieces
instance_destroy(obj_SkelodevilArm);
instance_destroy(obj_SkelodevilLeg);
instance_destroy(obj_SkelodevilBody);
instance_destroy(obj_SkelodevilHead);