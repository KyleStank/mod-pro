game.Log("Initializing mod...");
game.Log("MOD LOADED.");

file = game.GetModDirectory() .. 'crappymod/Calvin.png'
entity = game.GetPlayerEntity()
entity.SetNewSprite(0, game.LoadNewSprite(file))
