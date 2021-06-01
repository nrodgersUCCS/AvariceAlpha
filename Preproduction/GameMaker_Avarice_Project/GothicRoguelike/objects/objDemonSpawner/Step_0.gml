/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 313AE887
/// @DnDArgument : "code" "//creates an arry for all spawnpoints in room$(13_10)var i;$(13_10)for (i = 0; i < instance_number(objDemonSpawnPoint); i += 1) $(13_10){$(13_10)	spawnPoint[i] = instance_find(objDemonSpawnPoint,i);$(13_10)}$(13_10)$(13_10)//picks a random spawnpoint from array and spawns demon enemy$(13_10)if(spawntimer > 0)$(13_10){$(13_10)	spawntimer -= 1;	$(13_10)}$(13_10)if(spawntimer <= 0)$(13_10){$(13_10)	selectedPoint = random_range(0,array_length_1d(spawnPoint))$(13_10)	selectedObject = array_get(spawnPoint, selectedPoint)$(13_10)	instance_create_depth(selectedObject.x,selectedObject.y,1,obj_EnemyFire);$(13_10)	spawntimer = 180;$(13_10)}"
//creates an arry for all spawnpoints in room
var i;
for (i = 0; i < instance_number(objDemonSpawnPoint); i += 1) 
{
	spawnPoint[i] = instance_find(objDemonSpawnPoint,i);
}

//picks a random spawnpoint from array and spawns demon enemy
if(spawntimer > 0)
{
	spawntimer -= 1;	
}
if(spawntimer <= 0)
{
	selectedPoint = random_range(0,array_length_1d(spawnPoint))
	selectedObject = array_get(spawnPoint, selectedPoint)
	instance_create_depth(selectedObject.x,selectedObject.y,1,obj_EnemyFire);
	spawntimer = 180;
}