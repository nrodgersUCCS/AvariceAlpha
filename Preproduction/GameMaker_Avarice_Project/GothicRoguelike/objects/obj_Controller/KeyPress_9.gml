/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 40AEB81F
/// @DnDArgument : "code" "//open and close inventory$(13_10)if(inventoryOpen = false)$(13_10){$(13_10)	inventoryOpen = true;	$(13_10)	instance_deactivate_all(obj_Controller);$(13_10)}$(13_10)else$(13_10){$(13_10)	inventoryOpen = false;$(13_10)	instance_activate_all();$(13_10)}"
//open and close inventory
if(inventoryOpen = false)
{
	inventoryOpen = true;	
	instance_deactivate_all(obj_Controller);
}
else
{
	inventoryOpen = false;
	instance_activate_all();
}