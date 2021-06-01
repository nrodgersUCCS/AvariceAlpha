/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 5477C2A0
/// @DnDArgument : "code" "//Increases armor inventory when clicked$(13_10)if(buttonType = "armor"){$(13_10)	if(global.armorStashCount > 0){$(13_10)		global.armorStashCount -= 1;$(13_10)		if(global.isArmored){$(13_10)			global.armorCarried++;$(13_10)			ds_list_add(global.inventoryList,amorSprite);$(13_10)		}$(13_10)		else{$(13_10)			global.isArmored = true;$(13_10)		}$(13_10)	}$(13_10)}$(13_10)//Increases helm inventory when clicked$(13_10)else if(buttonType = "helm"){$(13_10)	if(global.helmStashCount > 0){$(13_10)		global.helmStashCount -= 1;$(13_10)		if(global.hasHelmet){$(13_10)			global.helmetsCarried++;$(13_10)			ds_list_add(global.inventoryList,spr_Helmet);$(13_10)		}$(13_10)		else{$(13_10)			global.hasHelmet = true;$(13_10)		}$(13_10)	}$(13_10)}$(13_10)//Increases longsword inventory when clicked$(13_10)else if(buttonType = "longsword"){$(13_10)	if(global.swordStashCount > 0){$(13_10)		global.swordStashCount -= 1;$(13_10)		if(global.hasSword){$(13_10)			global.swordCarried++;$(13_10)			ds_list_add(global.inventoryList,SwordPickupSprite);$(13_10)		}$(13_10)		else{$(13_10)			global.hasSword = true;$(13_10)		}$(13_10)	}$(13_10)}$(13_10)//Increases greatsword inventory when clicked$(13_10)else if(buttonType = "greatsword"){$(13_10)	if(global.greatswordStashCount > 0){$(13_10)		global.greatswordStashCount -= 1;$(13_10)		if(global.hasGreatSword){$(13_10)			global.greatswordsCarried++;$(13_10)			ds_list_add(global.inventoryList,attackingSword)$(13_10)		}$(13_10)		else{$(13_10)			global.hasGreatSword = true;$(13_10)		}$(13_10)	}$(13_10)}"
//Increases armor inventory when clicked
if(buttonType = "armor"){
	if(global.armorStashCount > 0){
		global.armorStashCount -= 1;
		if(global.isArmored){
			global.armorCarried++;
			ds_list_add(global.inventoryList,amorSprite);
		}
		else{
			global.isArmored = true;
		}
	}
}
//Increases helm inventory when clicked
else if(buttonType = "helm"){
	if(global.helmStashCount > 0){
		global.helmStashCount -= 1;
		if(global.hasHelmet){
			global.helmetsCarried++;
			ds_list_add(global.inventoryList,spr_Helmet);
		}
		else{
			global.hasHelmet = true;
		}
	}
}
//Increases longsword inventory when clicked
else if(buttonType = "longsword"){
	if(global.swordStashCount > 0){
		global.swordStashCount -= 1;
		if(global.hasSword){
			global.swordCarried++;
			ds_list_add(global.inventoryList,SwordPickupSprite);
		}
		else{
			global.hasSword = true;
		}
	}
}
//Increases greatsword inventory when clicked
else if(buttonType = "greatsword"){
	if(global.greatswordStashCount > 0){
		global.greatswordStashCount -= 1;
		if(global.hasGreatSword){
			global.greatswordsCarried++;
			ds_list_add(global.inventoryList,attackingSword)
		}
		else{
			global.hasGreatSword = true;
		}
	}
}