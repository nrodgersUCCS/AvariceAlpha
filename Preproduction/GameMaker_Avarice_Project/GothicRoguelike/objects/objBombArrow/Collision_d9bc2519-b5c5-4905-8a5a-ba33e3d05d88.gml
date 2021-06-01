/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 4BBB1930
/// @DnDArgument : "code" "//creates explosion when it hits demon$(13_10)instance_destroy(self);$(13_10)instance_create_depth(x,y,1,objExplosion);"
//creates explosion when it hits demon
instance_destroy(self);
instance_create_depth(x,y,1,objExplosion);