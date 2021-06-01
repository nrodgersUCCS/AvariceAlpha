/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 19934343
/// @DnDArgument : "code" "if(chance <= armorChance){$(13_10)	instance_create_layer(x,y,layer, obj_armor);$(13_10)}$(13_10)else if(chance > armorChance && chance <= armorChance + helmetChance){$(13_10)	instance_create_layer(x,y,layer,obj_helmet);$(13_10)}$(13_10)else if(chance > armorChance + helmetChance && chance <= armorChance + helmetChance + swordChance){$(13_10)	instance_create_layer(x,y,layer,obj_sword);$(13_10)}$(13_10)else{$(13_10)	instance_create_layer(x,y,layer,obj_greatsword);$(13_10)}"
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