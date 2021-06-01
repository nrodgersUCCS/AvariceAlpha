/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 273B10CE
/// @DnDArgument : "code" "//Puts all the items the player is carrying into the stash$(13_10)		global.armorStashCount += global.armorCarried + global.isArmored;$(13_10)		global.isArmored = false;$(13_10)		global.armorCarried = 0;$(13_10)		$(13_10)		global.helmStashCount += global.helmetsCarried + global.hasHelmet;$(13_10)		global.hasHelmet = false;$(13_10)		global.helmetsCarried = 0;$(13_10)		$(13_10)		global.swordStashCount += global.swordCarried + global.hasSword;$(13_10)		global.hasSword = false;$(13_10)		global.swordCarried = 0;$(13_10)		$(13_10)		global.greatswordStashCount += global.greatswordsCarried + global.hasGreatSword;$(13_10)		global.hasGreatSword = false;$(13_10)		global.greatswordsCarried = 0;$(13_10)		$(13_10)		ds_list_clear(global.inventoryList);"
//Puts all the items the player is carrying into the stash
		global.armorStashCount += global.armorCarried + global.isArmored;
		global.isArmored = false;
		global.armorCarried = 0;
		
		global.helmStashCount += global.helmetsCarried + global.hasHelmet;
		global.hasHelmet = false;
		global.helmetsCarried = 0;
		
		global.swordStashCount += global.swordCarried + global.hasSword;
		global.hasSword = false;
		global.swordCarried = 0;
		
		global.greatswordStashCount += global.greatswordsCarried + global.hasGreatSword;
		global.hasGreatSword = false;
		global.greatswordsCarried = 0;
		
		ds_list_clear(global.inventoryList);