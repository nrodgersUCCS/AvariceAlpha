// checks to see if inventory is open and then draws all sprites depending on if the
//list element is populated or not
if(inventoryOpen = true)
{
	draw_sprite(InventoryBackground,0,0,0);
	
	//first row
	if(ds_list_find_value(global.inventoryList,0) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|0],0,8,39, 32, 32)
		
	}
	if(ds_list_find_value(global.inventoryList,1) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|1],0,41,39,32,32);
	}
	if(ds_list_find_value(global.inventoryList,2) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|2],0,74,39,32,32);
	}
	if(ds_list_find_value(global.inventoryList,3) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|3],0,107,39,32,32);
	}
	if(ds_list_find_value(global.inventoryList,4) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|4],0,140,39,32,32);
	}
	if(ds_list_find_value(global.inventoryList,5) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|5],0,173,39,32,32);
	}
	if(ds_list_find_value(global.inventoryList,6) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|6],0,206,39,32,32);
	}
	
	//second row
	if(ds_list_find_value(global.inventoryList,7) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|7],0,8,72,32,32);
	}
	if(ds_list_find_value(global.inventoryList,8) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|8],0,41,72,32,32);
	}
	if(ds_list_find_value(global.inventoryList,9) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|9],0,74,72,32,32);
	}
	if(ds_list_find_value(global.inventoryList,10) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|10],0,107,72,32,32);
	}
	if(ds_list_find_value(global.inventoryList,11) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|11],0,140,72,32,32);
	}
	if(ds_list_find_value(global.inventoryList,12) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|12],0,173,72,32,32);
	}
	if(ds_list_find_value(global.inventoryList,13) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|13],0,206,72,32,32);
	}
	
	//3rd row
	if(ds_list_find_value(global.inventoryList,14) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|14],0,8,105,32,32);
	}
	if(ds_list_find_value(global.inventoryList,15) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|15],0,41,105,32,32);
	}
	if(ds_list_find_value(global.inventoryList,16) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|16],0,74,105,32,32);
	}
	if(ds_list_find_value(global.inventoryList,17) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|17],0,107,105,32,32);
	}
	if(ds_list_find_value(global.inventoryList,18) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|18],0,140,105,32,32);
	}
	if(ds_list_find_value(global.inventoryList,19) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|19],0,173,105,32,32);
	}
	if(ds_list_find_value(global.inventoryList,20) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|20],0,206,105,32,32);
	}
	
	//4th row
	if(ds_list_find_value(global.inventoryList,21) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|21],0,8,138,32,32);
	}
	if(ds_list_find_value(global.inventoryList,22) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|22],0,41,138,32,32);
	}
	if(ds_list_find_value(global.inventoryList,23) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|23],0,74,138,32,32);
	}
	if(ds_list_find_value(global.inventoryList,24) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|24],0,107,138,32,32);
	}
	if(ds_list_find_value(global.inventoryList,25) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|25],0,140,138,32,32);
	}
	if(ds_list_find_value(global.inventoryList,26) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|26],0,173,138,32,32);
	}
	if(ds_list_find_value(global.inventoryList,27) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|27],0,206,138,32,32);
	}
	
	//5th row
	if(ds_list_find_value(global.inventoryList,28) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|28],0,8,171,32,32);
	}
	if(ds_list_find_value(global.inventoryList,29) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|29],0,41,171,32,32);
	}
	if(ds_list_find_value(global.inventoryList,30) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|30],0,74,171,32,32);
	}
	if(ds_list_find_value(global.inventoryList,31) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|31],0,107,171,32,32);
	}
	if(ds_list_find_value(global.inventoryList,32) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|32],0,140,171,32,32);
	}
	if(ds_list_find_value(global.inventoryList,33) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|33],0,173,171,32,32);
	}
	if(ds_list_find_value(global.inventoryList,34) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|34],0,206,171,32,32);
	}
	
	//6th row
	if(ds_list_find_value(global.inventoryList,35) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|35],0,8,204,32,32);
	}
	if(ds_list_find_value(global.inventoryList,36) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|36],0,41,204,32,32);
	}
	if(ds_list_find_value(global.inventoryList,37) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|37],0,74,204,32,32);
	}
	if(ds_list_find_value(global.inventoryList,38) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|38],0,107,204,32,32);
	}
	if(ds_list_find_value(global.inventoryList,39) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|39],0,140,204,32,32);
	}
	if(ds_list_find_value(global.inventoryList,40) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|40],0,173,204,32,32);
	}
	if(ds_list_find_value(global.inventoryList,41) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|41],0,206,204,32,32);
	}
	
	//7th row
	if(ds_list_find_value(global.inventoryList,42) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|42],0,8,237,32,32);
	}
	if(ds_list_find_value(global.inventoryList,43) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|43],0,41,237,32,32);
	}
	if(ds_list_find_value(global.inventoryList,44) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|44],0,74,237,32,32);
	}
	if(ds_list_find_value(global.inventoryList,45) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|45],0,107,237,32,32);
	}
	if(ds_list_find_value(global.inventoryList,46) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|46],0,140,237,32,32);
	}
	if(ds_list_find_value(global.inventoryList,47) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|47],0,173,237,32,32);
	}
	if(ds_list_find_value(global.inventoryList,48) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|48],0,206,237,32,32);
	}
	
	//8th row
	if(ds_list_find_value(global.inventoryList,49) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|49],0,8,270,32,32);
	}
	if(ds_list_find_value(global.inventoryList,50) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|50],0,41,270,32,32);
	}
	if(ds_list_find_value(global.inventoryList,51) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|51],0,74,270,32,32);
	}
	if(ds_list_find_value(global.inventoryList,52) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|52],0,107,270,32,32);
	}
	if(ds_list_find_value(global.inventoryList,53) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|53],0,140,270,32,32);
	}
	if(ds_list_find_value(global.inventoryList,54) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|54],0,173,270,32,32);
	}
	if(ds_list_find_value(global.inventoryList,55) != undefined)
	{
		draw_sprite_stretched(global.inventoryList[|55],0,206,270,32,32);
	}
}