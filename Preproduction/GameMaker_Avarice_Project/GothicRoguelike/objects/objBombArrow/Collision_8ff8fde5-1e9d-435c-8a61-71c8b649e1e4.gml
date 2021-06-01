/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 0E6E4CD2
/// @DnDArgument : "code" "//creates explosion when it hits ranged zombie$(13_10)instance_destroy(self);$(13_10)instance_create_depth(x,y,1,objExplosion);"
//creates explosion when it hits ranged zombie
instance_destroy(self);
instance_create_depth(x,y,1,objExplosion);