/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 49A48F75
/// @DnDArgument : "code" "//creates explosion when it hits zombie$(13_10)instance_destroy(self);$(13_10)instance_create_depth(x,y,1,objExplosion);"
//creates explosion when it hits zombie
instance_destroy(self);
instance_create_depth(x,y,1,objExplosion);