extends Panel

var currImageCopy 
var lightTowerScene
var flameTowerScene
var lightningTowerScene
var towerScene
var tempTower

func _on_ready():
	flameTowerScene = load("res://Source/Entities/Towers/flame_tower.tscn")
	lightTowerScene = load("res://Source/Entities/Towers/light_tower.tscn")
	lightningTowerScene = load("res://Source/Entities/Towers/lightning_tower.tscn")
	print(flameTowerScene)
	print(lightTowerScene)
	print(lightningTowerScene)
	
	
func _on_gui_input(event):
	tempTower =  lightTowerScene.instantiate()
	print("Tower scene:", towerScene)

	if event is InputEventMouseButton and event.button_mask == 1:
		currImageCopy = get_child(0).duplicate()
		currImageCopy.global_position = event.global_position
		get_tree().current_scene.add_child(currImageCopy)
	elif event is InputEventMouseMotion and event.button_mask == 1:
		#Dragging
		currImageCopy.global_position = event.global_position

	elif event is InputEventMouseButton and event.button_mask == 0:
		get_tree().current_scene.add_child(tempTower)
		tempTower.global_position = event.global_position
		currImageCopy.queue_free()	
	

#func _on_gui_input(event):
#	print(get_child(0).name)
#	tempTower = flameTowerScene.instantiate()
#	if event is InputEventMouseButton and event.button_mask == 1:
#		currImageCopy = get_child(0).duplicate()
#		currImageCopy.global_position = event.global_position
#		get_tree().current_scene.add_child(currImageCopy)

