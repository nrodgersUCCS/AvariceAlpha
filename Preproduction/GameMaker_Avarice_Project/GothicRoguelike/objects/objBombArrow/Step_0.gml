/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 321089F8
/// @DnDArgument : "code" "//will despawn arrow after 1 to 1 and a half second$(13_10)despawnTimer -= 1;$(13_10)if(despawnTimer <= 0)$(13_10){$(13_10)	instance_create_depth(x,y,0,objExplosion);$(13_10)	instance_destroy(self);	$(13_10)}"
//will despawn arrow after 1 to 1 and a half second
despawnTimer -= 1;
if(despawnTimer <= 0)
{
	instance_create_depth(x,y,0,objExplosion);
	instance_destroy(self);	
}