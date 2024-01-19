extends PathFollow2D

var speed : float = .2
var path_end : float

# Called when the node enters the scene tree for the first time.
func _ready():
	self.progress_ratio = 0.0
	path_end = .985
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	var direction = -1 if self.progress_ratio > 0.99 else 1
	self.progress_ratio += direction * speed * delta
	
	if self.progress_ratio > path_end:
		self.progress_ratio = 0.0
	pass
