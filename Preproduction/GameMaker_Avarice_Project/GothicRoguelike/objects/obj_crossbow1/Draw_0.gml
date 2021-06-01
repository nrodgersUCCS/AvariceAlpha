/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 0AD1E4C0
/// @DnDArgument : "code" "//Draws instructions if player is close to them. Doesn't draw for the StashOverlay item$(13_10)draw_self()$(13_10)if (distance_to_object(obj_dummy) <= 50 && (layer != 2))$(13_10){$(13_10)	draw_text(x-50,y-16,"Press E to equip");$(13_10)}$(13_10)$(13_10)if(layer = 2 && mouseOver){$(13_10)	draw_text(89,210, str)$(13_10)}"
//Draws instructions if player is close to them. Doesn't draw for the StashOverlay item
draw_self()
if (distance_to_object(obj_dummy) <= 50 && (layer != 2))
{
	draw_text(x-50,y-16,"Press E to equip");
}

if(layer = 2 && mouseOver){
	draw_text(89,210, str)
}