extends Panel

var currImageCopy 
var towerScene

func _on_ready():
	var towerName = get_child(0).name
	towerScene = load("res://Source/Entities/Towers/"+towerName+".tscn")
	
func _on_gui_input(event):
	if event is InputEventMouseButton and event.button_mask == 1:
		currImageCopy = get_child(0).duplicate()
		currImageCopy.global_position = event.global_position
		get_tree().current_scene.add_child(currImageCopy)
	elif event is InputEventMouseMotion and event.button_mask == 1:
		#Dragging
		currImageCopy.global_position = event.global_position

	elif event is InputEventMouseButton and event.button_mask == 0:
		var tower = towerScene.instantiate()
		get_tree().current_scene.add_child(tower)
		tower.global_position = event.global_position
		currImageCopy.queue_free()
	

#func _on_gui_input(event):
#	print(get_child(0).name)
#	tempTower = flameTowerScene.instantiate()
#	if event is InputEventMouseButton and event.button_mask == 1:
#		currImageCopy = get_child(0).duplicate()
#		currImageCopy.global_position = event.global_position
#		get_tree().current_scene.add_child(currImageCopy)

