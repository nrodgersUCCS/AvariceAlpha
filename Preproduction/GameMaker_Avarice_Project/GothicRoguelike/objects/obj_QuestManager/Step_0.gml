//Completes the quest and artificailly breaks out of the loop
if(global.currentCollectedObjects == global.maxObjects)
{
	global.questCompleted = true;
	global.questActive = false;
	layer_set_visible(layer_id, true);
}