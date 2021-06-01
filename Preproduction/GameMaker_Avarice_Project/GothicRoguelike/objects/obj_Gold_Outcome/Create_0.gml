/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 4C81F1B1
/// @DnDArgument : "code" "goldTotal = 0;$(13_10)$(13_10)str = "Blank"$(13_10)$(13_10)//Adds items in stash$(13_10)goldTotal += 20 * global.armorStashCount$(13_10)goldTotal += 15 * global.helmStashCount$(13_10)goldTotal += 10 * global.swordStashCount$(13_10)goldTotal += 10 * global.greatswordStashCount$(13_10)$(13_10)//Adds items player is carrying outside of what's equipped$(13_10)goldTotal += 10 *global.armorCarried$(13_10)goldTotal += 8 * global.greatswordsCarried$(13_10)goldTotal += 5 * global.helmetsCarried$(13_10)goldTotal += 5 * global.swordCarried$(13_10)$(13_10)badLayer = layer_get_id("BadEnding")$(13_10)neutralLayer = layer_get_id("NeutralEnding");$(13_10)goodLayer = layer_get_id("GoodEnding");$(13_10)$(13_10)$(13_10)//Bad Ending Text$(13_10)if(goldTotal <=90){$(13_10)	str = "La Croix's forces were decimated.\n You were hanged for your failure."$(13_10)	layer_set_visible(badLayer, true);$(13_10)}$(13_10)else if(goldTotal<=200){$(13_10)	str = "La Croix's forces won, with heavy\n casualities. You remain\n an outsider."$(13_10)	layer_set_visible(neutralLayer, true);$(13_10)}$(13_10)else{$(13_10)	str = "La Croix's forces won. You were\n accepted into his land with\n your riches."$(13_10)	layer_set_visible(goodLayer, true);$(13_10)}"
goldTotal = 0;

str = "Blank"

//Adds items in stash
goldTotal += 20 * global.armorStashCount
goldTotal += 15 * global.helmStashCount
goldTotal += 10 * global.swordStashCount
goldTotal += 10 * global.greatswordStashCount

//Adds items player is carrying outside of what's equipped
goldTotal += 10 *global.armorCarried
goldTotal += 8 * global.greatswordsCarried
goldTotal += 5 * global.helmetsCarried
goldTotal += 5 * global.swordCarried

badLayer = layer_get_id("BadEnding")
neutralLayer = layer_get_id("NeutralEnding");
goodLayer = layer_get_id("GoodEnding");


//Bad Ending Text
if(goldTotal <=90){
	str = "La Croix's forces were decimated.\n You were hanged for your failure."
	layer_set_visible(badLayer, true);
}
else if(goldTotal<=200){
	str = "La Croix's forces won, with heavy\n casualities. You remain\n an outsider."
	layer_set_visible(neutralLayer, true);
}
else{
	str = "La Croix's forces won. You were\n accepted into his land with\n your riches."
	layer_set_visible(goodLayer, true);
}