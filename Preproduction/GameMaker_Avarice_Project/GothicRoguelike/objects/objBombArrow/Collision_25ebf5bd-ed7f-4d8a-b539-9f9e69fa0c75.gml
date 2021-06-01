/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 5D16AFC7
/// @DnDArgument : "code" "//creates explosion when it hits skeleton$(13_10)instance_destroy(self);$(13_10)instance_create_depth(x,y,1,objExplosion);"
//creates explosion when it hits skeleton
instance_destroy(self);
instance_create_depth(x,y,1,objExplosion);