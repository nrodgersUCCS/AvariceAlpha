/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 7A38E071
/// @DnDArgument : "code" "//Activates quest, closes window, sets quest objects to 0$(13_10)if(global.questCompleted == false)$(13_10){$(13_10)	global.questActive = true;$(13_10)}$(13_10)global.closeWindow = true;$(13_10)global.currentCollectedObjects = 0;"
//Activates quest, closes window, sets quest objects to 0
if(global.questCompleted == false)
{
	global.questActive = true;
}
global.closeWindow = true;
global.currentCollectedObjects = 0;