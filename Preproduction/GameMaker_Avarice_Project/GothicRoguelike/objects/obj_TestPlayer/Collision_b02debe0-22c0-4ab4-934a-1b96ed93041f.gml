//Checks if the player is invincible and if not, causes damage and begins I-frames.
if(!global.invincible){
	scrDamage();
	self.solid = false;
}