/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 321089F8
/// @DnDArgument : "code" "despawnTimer -= 1;$(13_10)if(despawnTimer <= 0)$(13_10){$(13_10)	instance_destroy(self);	$(13_10)}"
despawnTimer -= 1;
if(despawnTimer <= 0)
{
	instance_destroy(self);	
}