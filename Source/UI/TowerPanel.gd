extends Panel

var currImageCopy 
var towerScene

func _on_ready():
	var towerType = get_child(0).name
	var towerPath = "res://Source/Entities/Towers/"+towerType+".tscn"
	towerScene = load(towerPath)


func _on_gui_input(event):
	var tempTower = towerScene.instantiate()
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
		



