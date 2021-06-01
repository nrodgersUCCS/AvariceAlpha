/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 53F1D59D
/// @DnDArgument : "code" "drop = irandom(100);$(13_10)$(13_10)if(drop >= 0 && drop <= 3){$(13_10)	instance_create_layer(x,y,layer, ObjPrototypeLightArmor);$(13_10)}$(13_10)if(drop >= 3 && drop <= 4){$(13_10)	instance_create_layer(x,y,layer, ObjPrototypeHeavyArmor);$(13_10)}$(13_10)if(drop == 5 ){$(13_10)	instance_create_layer(x,y,layer, ObjPrototypeDemonicArmor);$(13_10)}"
drop = irandom(100);

if(drop >= 0 && drop <= 3){
	instance_create_layer(x,y,layer, ObjPrototypeLightArmor);
}
if(drop >= 3 && drop <= 4){
	instance_create_layer(x,y,layer, ObjPrototypeHeavyArmor);
}
if(drop == 5 ){
	instance_create_layer(x,y,layer, ObjPrototypeDemonicArmor);
}