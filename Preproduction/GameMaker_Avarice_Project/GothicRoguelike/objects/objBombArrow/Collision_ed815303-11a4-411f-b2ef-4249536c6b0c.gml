/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 32D049F7
/// @DnDArgument : "code" "//arrow destroys self when it hits wall and creates an explosion$(13_10)instance_create_depth(x,y,0,objExplosion);$(13_10)instance_destroy(self);"
//arrow destroys self when it hits wall and creates an explosion
instance_create_depth(x,y,0,objExplosion);
instance_destroy(self);