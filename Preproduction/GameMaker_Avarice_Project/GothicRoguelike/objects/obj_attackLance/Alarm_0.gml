/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 12905513
/// @DnDArgument : "code" "// disable cooldown and reset player's speed$(13_10)global.lanceCooldown = false;$(13_10)if(instance_exists(obj_dummy))$(13_10){$(13_10)	obj_dummy.maxSpeed += 1.3;$(13_10)}$(13_10)instance_destroy();"
// disable cooldown and reset player's speed
global.lanceCooldown = false;
if(instance_exists(obj_dummy))
{
	obj_dummy.maxSpeed += 1.3;
}
instance_destroy();