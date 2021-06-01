/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 4E96920F
/// @DnDArgument : "code" "//creates explosion when it hits boss$(13_10)instance_destroy(self);$(13_10)instance_create_depth(x,y,1,objExplosion);"
//creates explosion when it hits boss
instance_destroy(self);
instance_create_depth(x,y,1,objExplosion);