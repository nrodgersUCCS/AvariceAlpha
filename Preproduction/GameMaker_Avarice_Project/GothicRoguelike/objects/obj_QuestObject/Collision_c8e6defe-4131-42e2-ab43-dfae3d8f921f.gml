/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 4AF5462E
/// @DnDArgument : "code" "//"Colloects" the item if the quest for it is active, $(13_10)//and if the player isn't holding the max$(13_10)if(global.questActive == true && global.currentCollectedObjects < global.maxObjects)$(13_10){$(13_10)	global.currentCollectedObjects++;$(13_10)	instance_destroy();$(13_10)}"
//"Colloects" the item if the quest for it is active, 
//and if the player isn't holding the max
if(global.questActive == true && global.currentCollectedObjects < global.maxObjects)
{
	global.currentCollectedObjects++;
	instance_destroy();
}