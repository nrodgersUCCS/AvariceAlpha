/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 242674C6
/// @DnDArgument : "code" "//Decreases armor inventory when clicked$(13_10)if(buttonType = "armor"){$(13_10)	if(global.isArmored + global.armorCarried >0){$(13_10)		if(global.isArmored){$(13_10)			global.isArmored = false;$(13_10)		}$(13_10)	global.armorStashCount++;$(13_10)	ds_list_delete(global.inventoryList, ds_list_find_index(global.inventoryList,amorSprite))$(13_10)	}$(13_10)}$(13_10)//Decreases helm inventory when clicked$(13_10)else if(buttonType = "helm"){$(13_10)	if(global.hasHelmet + global.helmetsCarried >0){$(13_10)		if(global.hasHelmet){$(13_10)			global.hasHelmet = false;$(13_10)		}$(13_10)	global.helmStashCount++;$(13_10)	ds_list_delete(global.inventoryList,ds_list_find_index(global.inventoryList,spr_Helmet));$(13_10)	}$(13_10)}$(13_10)//Decreases longsword inventory when clicked$(13_10)else if(buttonType = "longsword"){$(13_10)	if(global.hasSword + global.swordCarried >0)$(13_10)	{$(13_10)		if(global.hasSword)$(13_10)		{$(13_10)			global.hasSword = false;$(13_10)		}$(13_10)	global.swordStashCount++;$(13_10)	ds_list_delete(global.inventoryList,ds_list_find_index(global.inventoryList,SwordPickupSprite));$(13_10)			global.arrayCount += 1;$(13_10)	}$(13_10)}$(13_10)//Decreases greatsword inventory when clicked$(13_10)else if(buttonType = "greatsword"){$(13_10)	if(global.hasGreatSword + global.greatswordsCarried >0)$(13_10)	{$(13_10)		if(global.hasGreatSword)$(13_10)		{$(13_10)			global.hasGreatSword = false;$(13_10)		}$(13_10)	global.greatswordStashCount++;$(13_10)	ds_list_delete(global.inventoryList,ds_list_find_index(global.inventoryList,attackingSword));$(13_10)	}$(13_10)}$(13_10)"
//Decreases armor inventory when clicked
if(buttonType = "armor"){
	if(global.isArmored + global.armorCarried >0){
		if(global.isArmored){
			global.isArmored = false;
		}
	global.armorStashCount++;
	ds_list_delete(global.inventoryList, ds_list_find_index(global.inventoryList,amorSprite))
	}
}
//Decreases helm inventory when clicked
else if(buttonType = "helm"){
	if(global.hasHelmet + global.helmetsCarried >0){
		if(global.hasHelmet){
			global.hasHelmet = false;
		}
	global.helmStashCount++;
	ds_list_delete(global.inventoryList,ds_list_find_index(global.inventoryList,spr_Helmet));
	}
}
//Decreases longsword inventory when clicked
else if(buttonType = "longsword"){
	if(global.hasSword + global.swordCarried >0)
	{
		if(global.hasSword)
		{
			global.hasSword = false;
		}
	global.swordStashCount++;
	ds_list_delete(global.inventoryList,ds_list_find_index(global.inventoryList,SwordPickupSprite));
			global.arrayCount += 1;
	}
}
//Decreases greatsword inventory when clicked
else if(buttonType = "greatsword"){
	if(global.hasGreatSword + global.greatswordsCarried >0)
	{
		if(global.hasGreatSword)
		{
			global.hasGreatSword = false;
		}
	global.greatswordStashCount++;
	ds_list_delete(global.inventoryList,ds_list_find_index(global.inventoryList,attackingSword));
	}
}