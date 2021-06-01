/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 57A018B6
/// @DnDArgument : "code" "//Closes the NPC window upon accepting or declining a quest$(13_10)if(global.closeWindow == true)$(13_10){$(13_10)	isOpen = false;$(13_10)	//If there is a quest, won't reopen window$(13_10)	if(global.questActive = false)$(13_10)	{$(13_10)		global.closeWindow = false;$(13_10)	}$(13_10)}$(13_10)//Closes the NPC if the player is too far away$(13_10)if(distance_to_object(obj_dummy) > maxDistance){$(13_10)	isOpen = false;$(13_10)}$(13_10)//Makes the QuestOverlay visible if isOpen is true.$(13_10)if(isOpen){$(13_10)	layer_set_visible(layer_id,true);$(13_10)}$(13_10)//Makes the QuestOverlay invisible if isOpen is false.$(13_10)else{$(13_10)	layer_set_visible(layer_id,false);$(13_10)}"
//Closes the NPC window upon accepting or declining a quest
if(global.closeWindow == true)
{
	isOpen = false;
	//If there is a quest, won't reopen window
	if(global.questActive = false)
	{
		global.closeWindow = false;
	}
}
//Closes the NPC if the player is too far away
if(distance_to_object(obj_dummy) > maxDistance){
	isOpen = false;
}
//Makes the QuestOverlay visible if isOpen is true.
if(isOpen){
	layer_set_visible(layer_id,true);
}
//Makes the QuestOverlay invisible if isOpen is false.
else{
	layer_set_visible(layer_id,false);
}